using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonKIT
{
    public class PlayerRangeWeapon : RangeWeapon
    {

        public override void OnTriggerEnter2D(Collider2D collider)
        {
            base.OnTriggerEnter2D(collider);

            if (collider.gameObject.tag == "Enemy" || collider.gameObject.tag=="Boss") //if contact with enemy
            {
                AIStats enemy = collider.gameObject.GetComponent<AIStats>(); //get aistats component
                Damage(enemy); //damage enemy
            }
        }

        //Damage method
        void Damage(AIStats enemy)
        {
            enemy.TakingDamage(damageRange.RandomFloat()+PlayerStats.Instance.Damage); //Random damage between damageRange.min and max
            Destroying(); 
        }
    }
}