using UnityEngine;
using System.Collections;

namespace DungeonKIT
{
    public class EnemyManager : MonoBehaviour
    {
        GameObject[] enemies; // Declare the array
        GameObject[] boss;

        void Start()
        {
            UpdateEnemyCount();
        }

        void UpdateEnemyCount()
        {
            // Find all game objects of a specific prefab
            enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Replace with your enemy tag
            boss= GameObject.FindGameObjectsWithTag("Boss");
            int enemyCount = enemies.Length;
            int bossCount=boss.Length;
            Debug.Log("Total Enemies: " + enemyCount +". Total bosses: "+bossCount);

            if (enemyCount == 0)
            {
                UIManager.Instance.LevelWon(); 
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
