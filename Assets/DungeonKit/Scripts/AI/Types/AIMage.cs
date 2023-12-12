using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonKIT
{
    public class AIMage : AICombat
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
            aiStats.enemyHP= new DoubleFloat(50f, 50f); //HP do mago é definido aqui
            player = GameObject.FindGameObjectWithTag("Player");
            aiController = GetComponent<AIController>();
            aiStats.attackSpeed= 0.10f + (0.01f*PlayerStats.GetInstance().DungeonLevel);
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
            if(aiStats.attackSpeed>1f){
                aiStats.attackSpeed=1f;
            }

            if (timeBtwShots <= 0)
            {
                RangeAttack(rangeWeapon, player.transform); //Spawn weapon
                
                //A velocidade dos ataques do mago é definido aqui.
                timeBtwShots = 1-(aiStats.attackSpeed*0.50f);//Set time to start again. 
            }
            else
            {
                timeBtwShots -= Time.deltaTime;//Time minus 1 sec
            }

        }
    }

}
