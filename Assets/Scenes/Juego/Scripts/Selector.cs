using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour
{
    public List<Sprite> poderesSprite = new List<Sprite>();
    public List<Image> poderesCanva = new List<Image>();
    public Pila<string> poderes = new Pila<string>();

    private void Start()
    {
        ActualizarSelector();

    }

    public void ActualizarSelector()
    {
        for (int i = 0; i < poderes.Count(); i++)
        {
            string poder = poderes.PeekAt(i);
            poderesCanva[i].sprite = ObtenerSpritePoder(poder);
        }
    }

    void CambiarOrden()
    {
        if (poderes.Count() > 1)
        {
            string Primerpoder = poderes.Pop();
            poderes.Push(Primerpoder);
            ActualizarSelector();
        }
    }

    public Sprite ObtenerSpritePoder(string poder)
    {
        switch (poder)
        {
            case "Velocity":
                return poderesSprite[0];
            case "Agua":
                return poderesSprite[1];
            default:
                return null;
        }
    }
}

