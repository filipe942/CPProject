﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonKIT
{
    public class Door : MonoBehaviour
    {
        InteractionTrigger trigger;
        SpriteRenderer spriteRenderer;
        Collider2D[] doorColliders;

        bool isLocked = true;

        [Header("Settings")]
        public int needKeyID; //Need key id
        public Sprite doorUnlockedSprite;

        private void Start()
        {
            trigger = GetComponent<InteractionTrigger>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            doorColliders = GetComponents<Collider2D>();
        }

        private void Update()
        {
            if (trigger.inTrigger)
            {
                //if player press Interaction button
                if (InputManager.Interaction)
                {
                    InputManager.Interaction = false; //unpress button

                    if (PlayerStats.Instance.DoorKeys.ContainsKey(needKeyID)) //null check
                    {
                        if (PlayerStats.Instance.DoorKeys[needKeyID] == true) //Key ownership check
                        {
                            OpenDoor();
                        }
                    }
                }
            }
        }

        void OpenDoor()
        {
            isLocked = false; //Unlock door
            PlayerStats.Instance.DoorKeys.Remove(needKeyID); //remove key from player

            AudioManager.Instance.Play(PlayerStats.Instance.audioSource, AudioManager.Instance.openDoor, false); //play opendoor sound

            spriteRenderer.sprite = doorUnlockedSprite; //Change door sprite to unlock

            for (int i = 0; i < doorColliders.Length; i++) //Disable colliders
            {
                doorColliders[i].enabled = false;
            }

            UIManager.Instance.UpdateUI(); //Update UI
        }

    }
}