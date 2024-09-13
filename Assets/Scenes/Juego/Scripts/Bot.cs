using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bot : Moto // Add MonoBehaviour inheritance  
{
    public int intervaloCambioDirección = 1; // Intervalo de tiempo para cambiar de dirección
    private float temp = 0.0f;

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
            }
            else if (direccionAleatoria == 1)
            {
                direccion = Vector2.down;
            }
            else if (direccionAleatoria == 2)
            {
                direccion = Vector2.right;
            }
            else if (direccionAleatoria == 3)
            {
                direccion = Vector2.left;
            }
        }

        if (items.Count() > 0)
        {
            base.AplicarItem();
        }
        if (poderes.Count() > 0)
        {
            base.AplicarPoder();
        }
    }











}

