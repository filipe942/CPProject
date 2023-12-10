using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonKIT
{
    public class AIMageRangedWeapon : RangeWeapon
    {


        public override void OnTriggerEnter2D(Collider2D collider)
        {
            base.OnTriggerEnter2D(collider);

            if (collider.gameObject.tag == "Player") //if contact with player
            {
                Damage(PlayerStats.Instance, 10f + (2f * PlayerStats.GetInstance().DungeonLevel)); //Player damaged
            }
        }


        //Damage method
        void Damage(PlayerStats player, float attackDamage)
        {
            player.TakingDamage(attackDamage); //Player hp - 1 
            Destroying(); //Destroyng gameobject
        }

    }
}