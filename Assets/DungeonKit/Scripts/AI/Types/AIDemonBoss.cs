using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonKIT
{
    public class AIDemonBoss : AICombat
    {
        AIController aiController;

        GameObject player;
        [Header("Prefabs")]
        public GameObject rangeWeapon; //Prefab Throwing Weapons

        [Header("Parametrs")]
        float timeBtwShots; //time between shots
        int attackCounter = 0; // Counter for tracking number of attacks
        
        private void Start()
        {
            aiStats = GetComponent<AIStats>();
            aiStats.enemyHP= new DoubleFloat(1200f + 2f * PlayerStats.GetInstance().DungeonLevel, 1200f + 2f * PlayerStats.GetInstance().DungeonLevel); //HP do mago é definido aqui
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
        public override void RangeAttackMultipleDirection(GameObject rangeWeapon, Transform target)
        {
            base.RangeAttackMultipleDirection(rangeWeapon, target);
        }

        //AttackByRate method
        void AttackByRate()
        {
            if (timeBtwShots <= 0)
            {
                RangeAttackMultipleDirection(rangeWeapon, player.transform); //Spawn weapon
                
                //A velocidade dos ataques do mago é definido aqui.
                timeBtwShots = 1-(aiStats.attackSpeed*0f);//Set time to start again. 
            }
            else
            {
                timeBtwShots -= Time.deltaTime;//Time minus 1 sec
            }

        }
    }

}
