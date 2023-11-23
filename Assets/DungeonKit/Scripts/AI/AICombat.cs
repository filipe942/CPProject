using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonKIT
{
    public class AICombat : MonoBehaviour
    {
        [HideInInspector] public AIStats aiStats;

        private void Start()
        {
            aiStats = GetComponent<AIStats>();
        }

        //Virtual method for Range Attack, reconfigured in child classes
        public virtual void RangeAttack(GameObject rangeWeapon, Transform target)
        {
            GameObject rangeShot = Instantiate(rangeWeapon); //Creates a weapon
            rangeShot.transform.position = transform.position; //Moves a weapon to its position

            //Calculate the angle and position where to shoot
            Vector2 dir = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y); 

            rangeShot.transform.up = dir;
        }

        public virtual void RangeAttackMultipleDirection(GameObject rangeWeapon, Transform target)
        {
            for (int i = 0; i < 8; i++)
            {
                GameObject rangeShot = Instantiate(rangeWeapon); //Creates a weapon
                rangeShot.transform.position = transform.position; //Moves a weapon to its position

                // Calculate the angle for each direction
                float angle = i * 45f;

                // Convert angle to a direction vector
                Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

                rangeShot.transform.up = dir;
            }
        }

        //Virtual method for Melee Attack, reconfigured in child classes
        public virtual void MeleeAttack(GameObject target, float attackDamage)
        {
            PlayerStats playerStats = PlayerStats.Instance;

            playerStats.TakingDamage(attackDamage);

        }
        protected virtual float CalculateDamage()
        {
            return 10f; // Default damage value
        }
    }
}