using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node<T> 
{
    public T Data;
    
    public Node<T> Next;

    public Node(T data)
    {
        Data = data;
        Next = null;
        
    }
}


public class ListaEnlazada<T>
{
    
    public Node<T> head;
    public int count;

    
    

    public ListaEnlazada()
    {
        head = null;
        count = 0;
    }

    // Agregar un elemento al inicio de la lista
    public void AddFirst(T data)
    {
        Node<T> newNode = new Node<T>(data);
        newNode.Next = head;
        head = newNode;
        count++;
    }

    // Agregar un elemento al final de la lista
    public void AddLast(T data)
    {
        Node<T> newNode = new Node<T>(data);
        if (head == null)
        {
            head = newNode;
        }
        else
        {
            Node<T> current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
        count++;
    }

    // Eliminar el primer elemento de la lista
    public T RemoveFirst()
    {
        if (head == null)
        {
            throw new InvalidOperationException("Vacio");
        }

        T data = head.Data;
        head = head.Next;
        count--;
        return data;
    }

    // Eliminar el último elemento de la lista
    public T RemoveLast()
    {
        if (head == null)
        {
            throw new InvalidOperationException("Vacio");
        }

        if (head.Next == null)
        {
            T data = head.Data;
            head = null;
            count--;
            return data;
        }

        Node<T> current = head;
        while (current.Next.Next != null)
        {
            current = current.Next;
        }

        T lastData = current.Next.Data;
        current.Next = null;
        count--;
        return lastData;
    }

    // Buscar un elemento en la lista
    public bool Contains(T data)
    {
        Node<T> current = head;
        while (current != null)
        {
            if (current.Data.Equals(data))
            {
                return true;
            }
            current = current.Next;
        }
        return false;
    }

    // Obtener el tamaño de la lista
    public int Count()
    {
        return count;
    }

    public T GetAt(int index)
    {
        if (index < 0 || index >= count)
        {
            throw new ArgumentOutOfRangeException("Índice fuera de rango.");
        }

        Node<T> current = head;
        int currentIndex = 0;

        while (current != null)
        {
            if (currentIndex == index)
            {
                return current.Data;
            }

            current = current.Next;
            currentIndex++;
        }

        // Si llegamos aquí, algo salió mal
        throw new InvalidOperationException("Elemento no encontrado.");
    }

    // Limpiar la lista
    public void Clear()
    {
        head = null;
        count = 0;
    }
    
}

