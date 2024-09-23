using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pila<T>
{
    private ListaEnlazada<T> lista = new ListaEnlazada<T>();

    public void Push(T data)
    {
        lista.AddFirst(data);
    }

    public T Pop()
    {
        if (lista.Count() == 0)
        {
            throw new InvalidOperationException("Vacio");
        }
        return lista.RemoveFirst();
    }

    public T Peek()
    {
        if (lista.Count() == 0)
        {
            throw new InvalidOperationException("Vacio");
        }
        return lista.GetAt(0);
    }

    public T PeekAt(int index)
    {
        if (index < 0 || index >= lista.Count())
        {
            throw new IndexOutOfRangeException("Indice fuera de rango");
        }
        return lista.GetAt(index);
    }

    public int Count()
    {
        return lista.Count();
    }

    public void Clear()
    {
        lista.Clear();
    }

}
