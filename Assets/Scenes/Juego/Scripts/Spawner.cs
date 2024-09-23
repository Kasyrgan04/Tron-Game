using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotSpawner : MonoBehaviour
{
    public GameObject[] botPrefabs;  // Array de prefabs de bots
    public Vector3[] posicionesIniciales; // Array con las coordenadas donde aparecer�n los bots

    void Start()
    {
        // Verificar que la cantidad de prefabs y posiciones coincidan
        if (botPrefabs.Length != posicionesIniciales.Length)
        {
            Debug.LogError("La cantidad de prefabs no coincide con la cantidad de posiciones.");
            return;
        }

        // Instanciar cada prefab de bot en su posici�n correspondiente
        for (int i = 0; i < botPrefabs.Length; i++)
        {
            Instantiate(botPrefabs[i], posicionesIniciales[i], Quaternion.identity);
        }
    }
}

