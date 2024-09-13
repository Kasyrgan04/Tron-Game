using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodoCola<T>
{
    public T Data;           
    public int Prioridad;    // Prioridad del nodo
    public NodoCola<T> Next; 

    public NodoCola(T data, int prioridad)
    {
        Data = data;
        Prioridad = prioridad;
        Next = null;
    }
}


public class Cola<T>
{
    private NodoCola<T> head;
    public int count;

    public Cola()
    {
        head = null;
        count = 0;
    }

    
    public void Enqueue(T data, int prioridad)
    {
        NodoCola<T> nuevoNodo = new NodoCola<T>(data, prioridad);

        if (head == null || head.Prioridad > prioridad)
        {
            // Insertar al inicio si la cola está vacía o el nodo tiene mayor prioridad
            nuevoNodo.Next = head;
            head = nuevoNodo;
            
        }
        else
        {
            // Insertar en la posición correcta según la prioridad
            NodoCola<T> current = head;
            while (current.Next != null && current.Next.Prioridad <= prioridad)
            {
                current = current.Next;
            }
            nuevoNodo.Next = current.Next;
            current.Next = nuevoNodo;
            
        }
        count++;
    }

    
    public T Dequeue()
    {
        if (head == null)
        {
            throw new InvalidOperationException("La cola está vacía.");
        }

        T data = head.Data;
        head = head.Next;
        count--;
        return data;
    }

    
    public bool IsEmpty()
    {
        return head == null;
    }

    public int Count()
    {
        return count;
    }

    public T Peek()
    {
        if (head == null)
        {
            throw new InvalidOperationException("La cola está vacía.");
        }

        return head.Data;
    }
}

