using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo2
{
    public Vector2 posicion;
    public Nodo2 arriba;
    public Nodo2 abajo;
    public Nodo2 izquierda;
    public Nodo2 derecha;

    public Nodo2(Vector2 posicion)
    {
        this.posicion = posicion;
    }
}

public class Grid2 : MonoBehaviour
{
    public Nodo2 primerNodo;
    public int columnas;
    public int filas;
    public GameObject spritePrefab;

    private void Start()
    {
        CrearGrid();
        Renderizar();
    }

    private void CrearGrid()
    {
        // Calcula el desplazamiento para centrar el grid en la pantalla
        float desplazamientoX = (columnas - 1) / 2.0f;
        float desplazamientoY = (filas - 1) / 2.0f;

        // Crea el primer nodo centrado en la pantalla
        primerNodo = new Nodo2(new Vector2(-desplazamientoX, -desplazamientoY));
        Nodo2 nodoFila = primerNodo; // Primer nodo de la fila actual

        // Crea el resto de las filas y columnas
        for (int i = 0; i < filas; i++)
        {
            Nodo2 nodoActual = nodoFila;

            for (int j = 1; j < columnas; j++)
            {
                // Crea un nuevo nodo a la derecha del nodo actual
                Nodo2 nuevoNodo = new Nodo2(new Vector2(nodoActual.posicion.x + 1, nodoActual.posicion.y));
                nodoActual.derecha = nuevoNodo;
                nuevoNodo.izquierda = nodoActual;
                nodoActual = nuevoNodo;
            }

            // Avanza a la siguiente fila
            if (i < filas - 1)
            {
                Nodo2 nuevoNodoAbajo = new Nodo2(new Vector2(primerNodo.posicion.x, nodoFila.posicion.y + 1));
                nodoFila.abajo = nuevoNodoAbajo;
                nuevoNodoAbajo.arriba = nodoFila;
                nodoFila = nuevoNodoAbajo;
            }
        }

        // Conecta las filas verticalmente
        Nodo2 nodoFilaArriba = primerNodo;
        while (nodoFilaArriba.abajo != null)
        {
            Nodo2 nodoActualArriba = nodoFilaArriba;
            Nodo2 nodoActualAbajo = nodoFilaArriba.abajo;

            while (nodoActualArriba.derecha != null)
            {
                nodoActualArriba.derecha.abajo = nodoActualAbajo.derecha;
                nodoActualAbajo.derecha.arriba = nodoActualArriba.derecha;

                nodoActualArriba = nodoActualArriba.derecha;
                nodoActualAbajo = nodoActualAbajo.derecha;
            }

            nodoFilaArriba = nodoFilaArriba.abajo;
        }
    }

    void Renderizar()
    {
        Nodo2 nodoActual = primerNodo;
        Nodo2 nodoFila = primerNodo;

        while (nodoFila != null)
        {
            while (nodoActual != null)
            {
                // Renderiza el nodo
                Instantiate(spritePrefab, nodoActual.posicion, Quaternion.identity);
                nodoActual = nodoActual.derecha;
            }

            nodoFila = nodoFila.abajo;
            nodoActual = nodoFila;
        }
    }
}

