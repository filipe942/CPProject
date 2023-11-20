using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonKIT
{
    public class AIDarkMage : AICombat
    {
        AIController aiController;

        GameObject player;
        [Header("Prefabs")]
        public GameObject rangeWeapon; //Prefab Throwing Weapons
        public GameObject superRangedWeapon;

        [Header("Parametrs")]
        float timeBtwShots; //time between shots
        int attackCounter = 0; // Counter for tracking number of attacks
        
        private void Start()
        {
            aiStats = GetComponent<AIStats>();
            aiStats.enemyHP= new DoubleFloat(80f, 80f); //HP do mago é definido aqui
            player = GameObject.FindGameObjectWithTag("Player");
            aiController = GetComponent<AIController>();
        }

        private void Update()
        {
            CheckAttackRadius();
        }

        void CheckAttackRadius()
        {
            if (Vector2.Distance(transform.position, player.transform.position) < aiController.radiusAttack) //If a player is in radiusAttack
            {
                AttackByRate(); //Attack
            }
        }
        //Method of attack
        public override void RangeAttack(GameObject rangeWeapon, Transform target)
        {
            base.RangeAttack(rangeWeapon, target);
        }

        public void SuperRangedAttack(GameObject superRangeWeapon, Transform target){
            base.RangeAttack(superRangeWeapon, target);
        }

        //AttackByRate method
        void AttackByRate()
        {
            if (timeBtwShots <= 0)
            {
                if (attackCounter == 9) // Check if it's the tenth attack
                {
                    SuperRangedAttack(superRangedWeapon, player.transform); // Trigger super attack
                    attackCounter = 0; // Reset attack counter
                }
                else
                {
                    RangeAttack(rangeWeapon, player.transform); // Perform regular ranged attack
                    attackCounter++; // Increment attack counter
                }

                // A velocidade dos ataques do mago é definido aqui.
                timeBtwShots = 1 - (aiStats.attackSpeed * 0f); // Set time to start again. 
            }
            else
            {
                timeBtwShots -= Time.deltaTime;//Time minus 1 sec
            }

        }
    }

}
