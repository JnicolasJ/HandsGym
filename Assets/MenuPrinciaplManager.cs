using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrinciaplManager : MonoBehaviour
{
    public void ShowPerfil()
    {
        SceneManager.LoadScene("Perfil");

    }

    public void ShowLaucher()
    {
        SceneManager.LoadScene("Launcher");
    }

    public void Salir()
    {
        Application.Quit();
    }

}
