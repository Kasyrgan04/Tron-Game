using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Bot : Moto
{

    public int intervaloCambioDirección = 1; // Intervalo de tiempo para cambiar de dirección
    private float temp = 0.0f;

    public override void Start()
    {
        nodoActual = FindObjectOfType<Grid2>().primerNodo; // Encuentra el nodo inicial


        combustibleTotal = 100;


        renderMoto = GetComponent<Renderer>();
        colorMoto = renderMoto.material.color;
        Reset();
    }

    public override void Update()
    {
        temp += Time.deltaTime;

        if (temp >= intervaloCambioDirección)
        {
            temp = 0.0f;
            int direccionAleatoria = Random.Range(0, 4);

            if (direccionAleatoria == 0)
            {
                direccion = Vector2.up;
                base.Rotacion(0);
            }
            else if (direccionAleatoria == 1)
            {
                direccion = Vector2.down;
                base.Rotacion(180);
            }
            else if (direccionAleatoria == 2)
            {
                direccion = Vector2.right;
                base.Rotacion(-90);
            }
            else if (direccionAleatoria == 3)
            {
                direccion = Vector2.left;
                base.Rotacion(90);
            }
        }

        if (items.Count() > 0)
        {
            base.DelayItem();
        }
        if (poderes.Count() > 0)
        {
            base.DelayPoder();
        }
        base.ContarNodosRecorridos();
    }
}
