using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorSingleton : Singleton<JugadorSingleton>
{
    public string m_usuario = "";
    public string m_email = "";
    public int m_ranking = 0;
    public int m_puntaje = 0;
    public int m_penalizaciones = 0;
    public int m_sesiones = 0;
    public string m_avatar = "";
    public double m_x;
    public double m_y;
    public double m_z;
    public double m_cod_instructor;

    public static JugadorSingleton Instance
    {
        get
        {
            return ((JugadorSingleton)mInstance);
        }
        set
        {
            mInstance = value;
        }
    }

    protected JugadorSingleton() { }
}
