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

        [Header("Parametrs")]
        float timeBtwShots; //time between shots
        
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
            //Set up here

            //
            base.RangeAttack(rangeWeapon, target);
        }

        //AttackByRate method
        void AttackByRate()
        {
            if (timeBtwShots <= 0)
            {
                RangeAttack(rangeWeapon, player.transform); //Spawn weapon
                
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