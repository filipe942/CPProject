using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonKIT;

public class AIDemon : AICombat
{
    GameObject player;
    AIDemonSpawner spawner; // Reference to the spawner

    private float timeBtwAttacks; // Time between orc attacks
    public float startTimeBtnAttacks = 1.0f; // Initial time between orc attacks
    

    private void Start()
    {
        aiStats = GetComponent<AIStats>();
        aiStats.enemyHP = new DoubleFloat(100f, 100f); // HP do orc é definido aqui
        aiStats.attackDamage = 5f;
        timeBtwAttacks = startTimeBtnAttacks; // Initialize the time between attacks

        // Find the spawner in the scene
        spawner = FindObjectOfType<AIDemonSpawner>();
        if (spawner == null)
        {
            Debug.LogError("AISpawner not found in the scene!");
        }
    }

    // If player stays in trigger
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") // If it's the player
        {
            // Call the attack method with a rate
            Attack();
        }
    }

    // Method to handle attacks based on a rate
    private void Attack()
    {
        if (timeBtwAttacks <= 0)
        {
            // Perform the attack actions here
            
            // If you want to spawn enemies when attacking, call the spawner
            if (spawner != null)
            {
                spawner.SpawnDemon();
            }

            timeBtwAttacks = startTimeBtnAttacks; // Reset the attack timer
        }
        else
        {
            timeBtwAttacks -= Time.deltaTime;
        }
    }
}
