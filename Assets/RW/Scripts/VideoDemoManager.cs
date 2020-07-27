using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using agora_gaming_rtc;
using System;

#if (UNITY_2018_3_OR_NEWER)
using UnityEngine.Android;
#endif

public class VideoDemoManager : MonoBehaviour
{
    [Header("Agora Settings")]
    //The name of the channel the user should join
    public string channelName = "defaultChannel";

    //The AppID of the Agora Project, from the Dashboard
    // Get your own App ID at https://dashboard.agora.io/
    public string appId;

    [Header("Scene References")]
    public Button joinButton;
    public TextMeshProUGUI buttonText;
    public PlayerVideo playerOne;
    public PlayerVideo playerTwo;
    public PlayerVideo playerTree;

    //The Agora chat engine
    private IRtcEngine mRtcEngine = null;
    private uint myId;

    private void Start()
    {

        #if (UNITY_2018_3_OR_NEWER)
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            Permission.RequestUserPermission(Permission.Camera);
        }
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
        #endif


        mRtcEngine = IRtcEngine.GetEngine(appId);
       
        //Add the listener to the join button to allow the player to join the channel.
        joinButton.onClick.AddListener(JoinChannel);

    }

    private void JoinChannel()
    {
        Debug.LogFormat("Joining Channel...");

        mRtcEngine.EnableVideo();
        //mRtcEngine->EnableVideo();
        mRtcEngine.EnableVideoObserver();

        //Add our callbacks to handle Agora events
        mRtcEngine.OnJoinChannelSuccess += OnJoinChannelSuccess;
        mRtcEngine.OnUserJoined += OnUserJoined;
        mRtcEngine.OnUserOffline += OnUserLeave;
        mRtcEngine.OnLeaveChannel += OnLeaveChannel;

        joinButton.onClick.RemoveListener(JoinChannel);
        joinButton.onClick.AddListener(LeaveChannel);
        buttonText.text = "En vivo";

        if (string.IsNullOrEmpty(channelName))
        {
            return;
        }

        mRtcEngine.JoinChannel(channelName, "extra", 0);
    }

    //When you join the channel...
    private void OnJoinChannelSuccess(string channelName, uint uid, int elapsed)
    {
        Debug.Log("Joined with uid " + uid);
        myId = uid;
        //Set to zero to show local input
        playerOne.Set(0);

    }

    //When a remote user joins the channel
    private void OnUserJoined(uint uid, int elapsed)
    {
        string userJoinedMessage = string.Format("onUserJoined callback uid {0} {1}", uid, elapsed);

        Debug.Log(userJoinedMessage);

        playerOne.Set(uid);
        playerOne.gameObject.SetActive(true);

        playerTwo.Set(uid);
        playerTwo.gameObject.SetActive(true);

        playerTree.Set(uid);
        playerTree.gameObject.SetActive(true);

    }


    void Update()
    {
        /*
        if (mRtcEngine != null)
        {
            mRtcEngine.Poll();
        }
        */
    }

    //Handles the other player leaving
    private void OnUserLeave(uint uid, USER_OFFLINE_REASON reason)
    {
        playerTwo.Clear();
        playerTree.Clear();
    }

    //Handles leaving the channel when the button is pressed
    private void LeaveChannel()
    {
        Debug.LogFormat("Leaving Channel");

        joinButton.onClick.RemoveListener(LeaveChannel);
        joinButton.onClick.AddListener(JoinChannel);
        buttonText.text = "Join";

        mRtcEngine.LeaveChannel();
        playerOne.Clear();
        playerTwo.Clear();
        playerTree.Clear();

        mRtcEngine.DisableVideoObserver();

        mRtcEngine.OnJoinChannelSuccess -= OnJoinChannelSuccess;
        mRtcEngine.OnUserJoined -= OnUserJoined;
        mRtcEngine.OnUserOffline -= OnUserLeave;
        mRtcEngine.OnLeaveChannel -= OnLeaveChannel;
    }

    //When someone leaves the channel...
    private void OnLeaveChannel(RtcStats stats)
    {
        playerTwo.Clear();
        playerTree.Clear();
    }
}
