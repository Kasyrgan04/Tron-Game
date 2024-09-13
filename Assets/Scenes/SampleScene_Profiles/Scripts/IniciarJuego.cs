using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IniciarJuego : MonoBehaviour
{
    public void Iniciar()
    {
        SceneManager.LoadScene("Juego");
    }
}
