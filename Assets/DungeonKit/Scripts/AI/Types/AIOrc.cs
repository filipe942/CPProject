using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonKIT
{
    public class AIOrc : AICombat
    {
        GameObject player;

        private float timeBtwAttacks; // Time between orc attacks
        public float startTimeBtnAttacks = 1.0f; // Initial time between orc attacks

        private void Start()
        {
            aiStats = GetComponent<AIStats>();
            aiStats.enemyHP = new DoubleFloat(100f + 1.5f * PlayerStats.GetInstance().DungeonLevel, 100f + 1.5f * PlayerStats.GetInstance().DungeonLevel); // HP do orc é definido aqui
            aiStats.attackDamage = 5f + (1.5f * PlayerStats.GetInstance().DungeonLevel);
            timeBtwAttacks = startTimeBtnAttacks; // Initialize the time between attacks
            aiStats.attackSpeed= 0.45f + (0.01f*PlayerStats.GetInstance().DungeonLevel);
        }

        // If player stays in trigger
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player") // If it's the player
            {
                AttackByRate(); // Call the attack method with a rate
            }
        }

        // Method to handle attacks based on a rate
        void AttackByRate()
        {
            if(aiStats.attackSpeed>1.90f){
                aiStats.attackSpeed=1.90f;
            }

            if (timeBtwAttacks <= 0)
            {
                MeleeAttack(player, aiStats.attackDamage); // Call your MeleeAttack method with appropriate parameters

                // Velocidade dos ataques do orc é definido aqui
                timeBtwAttacks = 1-(aiStats.attackSpeed*0.50f); // Set time to start again
            }
            else
            {
                timeBtwAttacks -= Time.deltaTime; // Decrease the time between attacks
            }
        }

        // Method of attack
        public override void MeleeAttack(GameObject target, float attackDamage) 
        {
            // Set up here
            // ...

            // Call the base method
            base.MeleeAttack(target, attackDamage);
        }
    }
}
