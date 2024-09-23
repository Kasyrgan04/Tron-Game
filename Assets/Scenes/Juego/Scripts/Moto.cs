
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;


public class Moto : MonoBehaviour
{
    //Manejo de la moto
    public Vector2 direccion = Vector2.up; // Dirección inicial de la moto
    public List<Transform> estela = new List<Transform>(); // Lista de partes de la estela
    public Grid2 grid;
    public Nodo2 nodoActual;
    public int nodosRecorridos = 0;
    public int combustibleTotal;
    public Transform estelaPrefab; // Prefab de la estela
    public int tamanoInicial = 4; // Tamaño inicial de la moto
    //Poderes e items
    public Pila<string> poderes = new Pila<string>(); // Pila de poderes
    public string poderActual;
    public bool shield = false;
    public Color colorMoto;
    public Color colorEscudo = Color.cyan;
    public Renderer renderMoto;
    public float tEscudo = 5.0f;
    public float tempEscudo = 0.0f;
    public Cola<string> items = new Cola<string>(); // Cola de items
    [SerializeField] Slider barraCombustible;
    public string itemActual;






    // Start is called before the first frame update
    public virtual void Start()
    {
        nodoActual = FindObjectOfType<Grid2>().primerNodo; // Encuentra el nodo inicial
        

        combustibleTotal = 100;
        barraCombustible.maxValue = 100;
        barraCombustible.value = combustibleTotal;

        renderMoto = GetComponent<Renderer>();
        colorMoto = renderMoto.material.color;
        Reset();

    }

    // Update is called once per frame
    public virtual void Update()  
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            direccion = Vector2.up;
            Rotacion(0);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direccion = Vector2.down;
            Rotacion(180);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direccion = Vector2.right;
            Rotacion(-90);

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direccion = Vector2.left;
            Rotacion(90);

        }
        else if(poderes.Count()>0)
        {
            DelayPoder();
        }

        if(shield == true)
        {
            tempEscudo -= Time.deltaTime;
            Debug.Log("Escudo: " + tempEscudo);
            if (tempEscudo <= 0)
            {
                shield = false;
                renderMoto.material.color = colorMoto;
            }
        }

        if (items.Count() > 0) 
        {
            DelayItem();
        }


        if (combustibleTotal <= 0)
        {
            Reset();
            
        }

        ContarNodosRecorridos();
        
    }

    public virtual void FixedUpdate()
    {
        for (int i = estela.Count - 1; i > 0; i--)
        {
            estela[i].position = estela[i - 1].position;

        }

        this.transform.position = new Vector3(

            Mathf.Round(this.transform.position.x) + direccion.x,
            Mathf.Round(this.transform.position.y) + direccion.y ,
            0.0f);
        


    }

    public void ReducirCombustible()
    {
        combustibleTotal--;
        
        barraCombustible.value = combustibleTotal;

    }

    public void DelayItem()
    {
        string item = items.Dequeue();

        Invoke("AplicarItem", 1f);
        itemActual = item;  // Guardar el ítem actual
    }

    public void AplicarItem()
    {
        switch (itemActual)
        {
            case "Combustible":
                combustibleTotal += Random.Range(10, 50);
                break;
            case "Growth":
                Crecer();
                break;
            case "Bomb":
                Reset();
                break;
        }
        Debug.Log("Item aplicado: " + itemActual);
    }

    public void DelayPoder()
    {
        string poder = poderes.Pop();

        Invoke("AplicarPoder", 10f);
        poderActual = poder;  
    }

    public void AplicarPoder()
    {


        if (poderActual == "Velocity")
        {
            return;
        }
        else if (poderActual == "Shield")
        {
            Escudo(10.0f);
        }
       
    }

    public void ContarNodosRecorridos()
    {
        nodosRecorridos++;
        if (nodosRecorridos >= 5)
        {
            nodosRecorridos = 0;
            Crecer();
            ReducirCombustible();
        }
    }

    public void Crecer()
    {
        Transform estelaParte = Instantiate(this.estelaPrefab);
        estelaParte.position = estela[estela.Count - 1].position;
        estela.Add(estelaParte);
    }

    public void Rotacion(float rotacion)
    {
        
        this.transform.rotation = Quaternion.Euler(0, 0, rotacion);

        
        foreach (Transform parteEstela in estela)
        {
            parteEstela.rotation = Quaternion.Euler(0, 0, rotacion);
        }
    }

    public void Escudo(float duracion)
    {
        shield = true;
        tEscudo = duracion;
        tempEscudo = duracion;
        renderMoto.material.color = colorEscudo;
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
        combustibleTotal = 100;
        poderes.Clear();
        items.Clear();
        shield = false;
        tempEscudo = 0.0f;
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

        else if (collision.gameObject.tag == "Bomb" && !shield)
        {
            items.Enqueue("Bomb",2);
        }

        else if(collision.gameObject.tag == "Velocity")
        {
            poderes.Push("Velocity");
        }

        else if(collision.gameObject.tag == "Shield")
        {
            poderes.Push("Shield");
        }

        else if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bot" || collision.gameObject.tag == "Estela" && !shield)
        {
            Reset();
        }
        else if(collision.gameObject.tag == "Wall")
        {
            Reset();
        }

    }

}