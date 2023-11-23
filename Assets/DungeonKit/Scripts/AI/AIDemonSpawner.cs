using UnityEngine;

public class AIDemonSpawner : MonoBehaviour
{
    public GameObject[] AIPrefabs;
    public float minSpawnInterval = 40f;
    public float maxSpawnInterval = 45f;

    private bool canSpawn = true;

    void Start()
    {
        // Immediate spawn
        SpawnDemon();

        // Schedule the next spawn after a random delay
        Invoke("SpawnDemon", Random.Range(minSpawnInterval, maxSpawnInterval));
    }

    public void SpawnDemon()
    {
        if (canSpawn)
        {
            // Randomly choose an enemy prefab from the array
            int randomIndex = Random.Range(0, AIPrefabs.Length);
            GameObject selectedAIPrefab = AIPrefabs[randomIndex];

            // Check if the spawner is within a collider
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f); // Change the radius accordingly
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject == gameObject)
                {
                    canSpawn = false; // Prevent spawning if the spawner is within a collider
                    break;
                }
            }

            if (canSpawn)
            {
                // Spawn the selected enemy prefab at the spawner's position
                Instantiate(selectedAIPrefab, transform.position, Quaternion.identity);
            }
        }

        // Schedule the next spawn after the defined interval
        Invoke("SpawnDemon", Random.Range(minSpawnInterval, maxSpawnInterval));
    }
}
