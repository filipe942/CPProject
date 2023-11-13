using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonKIT
{
    public class HelmetOfStrength_Item : Item
    {

        public void OnPickedUp()
        {
            PlayerStats playerStats = PlayerStats.Instance;

            playerStats.HP = new DoubleFloat(5f,5f);
            UIManager.Instance.UpdateUI();
        }

        public override void OnTriggerEnter2D(Collider2D collision)
        {
            onPickedUp += OnPickedUp;
            base.OnTriggerEnter2D(collision);
        }

    }
}
