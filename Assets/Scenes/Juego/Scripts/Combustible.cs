using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combustible : MonoBehaviour
{
    public BoxCollider2D gridArea;

    private void Start()
    {
        RandomPosition();
    }
    private void RandomPosition()
    {
        Bounds limites = this.gridArea.bounds;
        float x = Random.Range(limites.min.x, limites.max.x);
        float y = Random.Range(limites.min.y, limites.max.y);
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            RandomPosition();

        }
        else if (collision.gameObject.tag == "Wall")
        {
            RandomPosition();
        }

        else if (collision.gameObject.tag == "Area")
        {
            RandomPosition();
        }

        else if (collision.gameObject.tag == "Area (1)")
        {
            RandomPosition();
        }
    }
}




