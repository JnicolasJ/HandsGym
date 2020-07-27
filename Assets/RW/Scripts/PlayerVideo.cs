using agora_gaming_rtc;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerVideo : MonoBehaviour
{
    public VideoSurface videoSurface;
    public void Set(uint uid)
    {

        videoSurface.gameObject.SetActive(true);
        videoSurface.SetEnable(true);
        videoSurface.SetForUser(uid);

    }


    public void Clear()
    {
        videoSurface.SetEnable(false);
    }

}
