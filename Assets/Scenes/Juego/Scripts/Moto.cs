using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class Moto : MonoBehaviour
{
    public Vector2 direccion = Vector2.up; // Dirección inicial de la moto
    public List<Transform> estela = new List<Transform>(); // Lista de partes de la estela
    public Pila<string> poderes = new Pila<string>(); // Pila de poderes
    public Cola<string> items = new Cola<string>(); // Cola de items
    public Transform estelaPrefab; // Prefab de la estela
    public int tamanoInicial = 4; // Tamaño inicial de la moto
    public Grid2 grid;
    public Nodo2 nodoActual;
    public int nodosRecorridos = 0;
    public float velocidad = 5.0f;
    public int combustibleTotal;
    

    // Start is called before the first frame update
    public void Start()
    {
        nodoActual = FindObjectOfType<Grid2>().primerNodo; // Encuentra el nodo inicial
        combustibleTotal= 100;
        Reset();

    }

    // Update is called once per frame
    public virtual void Update()  
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            direccion = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direccion = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direccion = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direccion = Vector2.left;
        }

        if (items.Count() > 0) 
        {
            AplicarItem();
        }

        if(poderes.Count() > 0)
        {
            AplicarPoder();
        }
    }

    public void FixedUpdate()
    {
        for (int i = estela.Count - 1; i > 0; i--)
        {
            estela[i].position = estela[i - 1].position;
        }

        this.transform.position = new Vector3(

            Mathf.Round(this.transform.position.x) + direccion.x,
            Mathf.Round(this.transform.position.y) + direccion.y,
            0.0f);

        ContarNodosRecorridos();
        ReducirCombustible();
    }

    private void ReducirCombustible()
    {
        if(nodosRecorridos >= 5)
        {
            combustibleTotal--;
            nodosRecorridos = 0;
        }

        if (combustibleTotal <= 0)
        {
            Reset();
            //Insertar GameOver
        }
    }

    public void AplicarItem()
    {
        string item = items.Dequeue();

        switch (item)
        {
            case "Combustible":
                combustibleTotal += Random.Range(1, 90);
                break;
            case "Growth":
                Crecer();
                break;
            case "Bomb":
                //Insertar código para activar bomba
                break;
        }
    }

    public void AplicarPoder()
    {
        string poder = poderes.Pop();

        switch (poder)
        {
            case "Velocity":
                velocidad = 10.0f;
                break;
            case "Shield":
                //Insertar código para activar escudo
                break;
        }
    }

    public void ContarNodosRecorridos()
    {
        nodosRecorridos++;
        if (nodosRecorridos >= 5)
        {
            nodosRecorridos = 0;
            Crecer();
        }
    }

    public void Crecer()
    {
        Transform estelaParte = Instantiate(this.estelaPrefab);
        estelaParte.position = estela[estela.Count - 1].position;
        estela.Add(estelaParte);
    }

    public void Reset()
    {
        for(int i = 1; i < estela.Count; i++)
        {
            Destroy(estela[i].gameObject);
        }

        estela.Clear();
        estela.Add(this.transform);

        for (int i=1; i < this.tamanoInicial; i++)
        {
            estela.Add(Instantiate(this.estelaPrefab));
        }

        this.transform.position = Vector3.zero;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Combustible")
        {
            items.Enqueue("Combustible",1);
        }

        else if (collision.gameObject.tag == "Growth")
        {
            items.Enqueue("Growth",2);
        }

        else if (collision.gameObject.tag == "Bomb")
        {
            items.Enqueue("Bomb",2);
        }

        else if (collision.gameObject.tag == "Wall")
        {
            Reset();
        }

        else if(collision.gameObject.tag == "Velocity")
        {
            poderes.Push("Velocity");
        }
        else if(collision.gameObject.tag == "Shield")
        {
            poderes.Push("Shield");
        }

        else if (collision.gameObject.tag == "Moto")
        {
            Reset();
        }

        else if (collision.gameObject.tag == "Bot")
        {
            Reset();
        }


    }

}