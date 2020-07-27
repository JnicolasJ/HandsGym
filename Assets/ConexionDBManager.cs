using System;
using System.Collections;
using UnityEngine;

//encargado de enviar y recibir informacion del jugador
public class ConexionDBManager : MonoBehaviour
{
   public void CrearUser(string user, string email, string pass, string codigo, Action<Response> response)
    {
        StartCoroutine(CO_CreateUser(user, email, pass, codigo, response));
    }

    private IEnumerator CO_CreateUser(string user, string email, string pass,string codigo, Action<Response> response)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", user);
        form.AddField("email", email);
        form.AddField("password", pass);
        form.AddField("codigo", codigo);

        WWW w = new WWW("http://localhost/Game/crearUser.php", form);
        
        //esperamos la respuesta
        yield return w;

        //w.text tiene la respuesta del servidor
        response(JsonUtility.FromJson<Response>(w. text));

        /*debug
        if (w.error != null)
        {
            //m_tx.text = "404 not found!";
            Debug.Log("<color=red>" + w.text + "</color>");//error
        }
        else
        {
            if (w.isDone)
            {
                if (w.text.Contains("error"))
                {
                   // errorMessages.text = "invalid username or password!";
                    Debug.Log("<color=red>" + w.text + "</color>");//error
                }
                else
                {
                    //open welcom panel
                    //welcomePanel.SetActive(true);
                    //user.text = username.text;
                    Debug.Log("<color=green>" + w.text + "</color>");//user exist
                }
            }
        }
        */
    }

    public void CheckUser(string user, string pass, Action<Response> response)
    {
        StartCoroutine(CO_CheckUser(user, pass, response));
    }

    private IEnumerator CO_CheckUser(string user, string pass, Action<Response> response)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", user);
        form.AddField("password", pass);
        WWW w = new WWW("http://localhost/Game/checkUser.php", form);
        yield return w;
        Debug.Log(w.text);
        response(JsonUtility.FromJson<Response>(w.text));
    }


    public void UpdateAvatar(string user, string avatar)
    {
        StartCoroutine(CO_UpdateAvatar(user, avatar));
    }

    private IEnumerator CO_UpdateAvatar(string user, string avatar)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", user);
        form.AddField("avatar", avatar);

        WWW w = new WWW("http://localhost/Game/updateAvatar.php", form);

        //esperamos la respuesta
        yield return w;
        //w.text tiene la respuesta del servidor
        Debug.Log(w.text);
       
    }

    public void UpdateSesiones(string user)
    {
        StartCoroutine(CO_UpdateSesiones(user));
    }
    private IEnumerator CO_UpdateSesiones(string user)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", user);
        form.AddField("sesion", JugadorSingleton.Instance.m_sesiones + 1);
        WWW w = new WWW("http://localhost/Game/updateSesion.php", form);
        yield return w;
        Debug.Log(w.text);
    }

    public void UpdatePuntaje(string user)
    {
        StartCoroutine(CO_Puntaje(user));
    }
    private IEnumerator CO_Puntaje(string user)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", user);
        form.AddField("puntaje", JugadorSingleton.Instance.m_puntaje + 1);
        WWW w = new WWW("http://localhost/Game/updatePuntaje.php", form);
        yield return w;
        Debug.Log(w.text);
    }

    public void UpdatePenalizacion(string user)
    {
        StartCoroutine(CO_Penalizacion(user));
    }

    private IEnumerator CO_Penalizacion(string user)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", user);
        form.AddField("penalizacion", JugadorSingleton.Instance.m_penalizaciones + 1);

        WWW w = new WWW("http://localhost/Game/updatePenalizacion.php", form);

        //esperamos la respuesta
        yield return w;
        //w.text tiene la respuesta del servidor
        Debug.Log(w.text);
    }
}

//serializable para que pueda ser transformada a jason
[Serializable]
public class Response
{
    public bool done = false;
    public string mensaje = "";
    public string db_usuario = "";
    public string db_email = "";
    public string db_avatar = "";
    public int db_puntaje = 0;
    public int db_penalizacion = 0;
    public int db_ranking = 0;
    public int db_sessiones = 0;
    public double db_x = 0;
    public double db_y = 0;
    public double db_z = 0;
    public double db_cod_instructor = 0;
    

}
