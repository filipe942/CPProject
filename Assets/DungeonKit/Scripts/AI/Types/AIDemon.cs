using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonKIT;

public class AIDemon : AICombat
{
    GameObject player;
    AIDemonSpawner spawner; // Reference to the spawner

    private float timeBtwAttacks; // Time between orc attacks
    public float startTimeBtnAttacks = 10.0f; // Initial time between orc attacks

    public bool attackedOnce;

    private void Start()
    {
        aiStats = GetComponent<AIStats>();
        aiStats.enemyHP = new DoubleFloat(200f, 200f); // HP do orc é definido aqui
        aiStats.attackDamage = PlayerStats.GetInstance().HP.max + (2f * PlayerStats.GetInstance().DungeonLevel);
        timeBtwAttacks = startTimeBtnAttacks; // Initialize the time between attacks
        aiStats.attackSpeed= -0.50f;
        attackedOnce=false;
        

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
            if(attackedOnce==false){
                MeleeAttack(player, aiStats.attackDamage); // Call your MeleeAttack method with appropriate parameters
                attackedOnce=true;
            }
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
    public override void MeleeAttack(GameObject target, float attackDamage) 
        {
            // Set up here
            // ...

            // Call the base method
            base.MeleeAttack(target, attackDamage);
        }
}
