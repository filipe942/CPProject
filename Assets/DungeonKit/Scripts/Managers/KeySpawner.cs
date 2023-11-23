using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonKIT
{
    public class KeySpawner : MonoBehaviour
    {
        public GameObject prefabToSpawn; // Assign the prefab in the Inspector
        
        void Start()
        {
            SpawnPrefabAtRandom();
        }

        void SpawnPrefabAtRandom()
        {
            GameObject[] spawners = GameObject.FindGameObjectsWithTag("KeySpawner");

            if (spawners.Length > 0 && prefabToSpawn != null)
            {
                int randomIndex = UnityEngine.Random.Range(0, spawners.Length);
                GameObject randomSpawner = spawners[randomIndex];

                Instantiate(prefabToSpawn, randomSpawner.transform.position, Quaternion.identity);
                // You may want to add additional logic to position the prefab properly
            }
            else
            {
                Debug.LogError("No key spawners found or prefab not assigned.");
            }
        }
    }
}
