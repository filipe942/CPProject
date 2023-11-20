using UnityEngine;
using System.Collections;

namespace DungeonKIT
{
    public class EnemyManager : MonoBehaviour
    {
        GameObject[] enemies; // Declare the array

        void Start()
        {
            UpdateEnemyCount();
        }

        void UpdateEnemyCount()
        {
            // Find all game objects of a specific prefab
            enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Replace with your enemy tag
            int enemyCount = enemies.Length;
            Debug.Log("Total Enemies: " + enemyCount);

            if (enemyCount == 0)
            {
                Debug.Log("Level complete!");
                GameManager.Instance.LevelComplete();
            }

        }

        // Call this method whenever an enemy is destroyed
        public void EnemyDestroyed()
        {
            // Update the enemy count after a short delay to ensure object destruction
            StartCoroutine(DelayedUpdate());
        }

        IEnumerator DelayedUpdate()
        {
            yield return new WaitForSeconds(0.1f); // Adjust this delay as needed

            UpdateEnemyCount(); // Update the enemy count after a delay
        }
    }
}
