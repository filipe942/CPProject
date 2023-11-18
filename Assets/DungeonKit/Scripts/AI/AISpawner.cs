using UnityEngine;

public class AISpawner : MonoBehaviour
{
    public GameObject[] AIPrefabs;

    void Start()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        // Randomly choose an enemy prefab from the array
        int randomIndex = Random.Range(0, AIPrefabs.Length);
        GameObject selectedAIPrefab = AIPrefabs[randomIndex];

        // Spawn the selected enemy prefab at the spawner's position
        Instantiate(selectedAIPrefab, transform.position, Quaternion.identity);
    }
}
