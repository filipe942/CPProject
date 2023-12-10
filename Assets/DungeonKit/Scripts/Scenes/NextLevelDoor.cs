

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonKIT
{
    public class NextLevelDoor : MonoBehaviour
    {
        new CircleCollider2D collider2D;

        [Header("Components")]
        SpriteRenderer spriteRenderer;
        public Sprite lockedSprite, openedSprite;
        public bool lockedDoor; //Door status

        bool inTrigger;
        InteractionTrigger interactionTrigger;

        private void Start()
        {
            interactionTrigger = GetComponent<InteractionTrigger>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            collider2D = GetComponent<CircleCollider2D>();

            CheckLockStatus(); //Check door status
        }

        private void Update()
        {
            //if player in trigger
            if (interactionTrigger.inTrigger)
            {
                //if player press Interaction button
                if (InputManager.Interaction)
                {
                    InputManager.Interaction = false; //unpress button
                    if (!lockedDoor) //if door unlocked
                    {
                        GoToNextLevel(); //go to next level
                    }
                }
            }
        }

        //Check door status method
        public void CheckLockStatus()
        {
             if (PlayerStats.Instance != null && PlayerStats.Instance.audioSource != null)
            {
                if (lockedDoor) // if door locked
                {
                    spriteRenderer.sprite = lockedSprite; // sprite locked door
                    collider2D.enabled = false; // trigger disabled
                }
                else
                {
                    spriteRenderer.sprite = openedSprite; // sprite unloced door
                    collider2D.enabled = true; // trigger enabled
                }
            }
            else
            {
                Debug.LogWarning("PlayerStats or AudioSource not assigned!");
                // Handle the case where the PlayerStats or AudioSource is not assigned
                // You might want to take alternative actions or log an error here
            }
        }
        //Next level method para testes, o debaixo e o final
        void GoToNextLevel()
        {
            
            int randomLevelID;
            // Check if the level is a multiple of 10
            if (PlayerStats.GetInstance().DungeonLevel % 2 == 0 && PlayerStats.GetInstance().DungeonLevel != 0)
            {
                int bossLevel = UnityEngine.Random.Range(1, 3); // Randomly select 1 or 2 for the boss level
                PlayerStats.GetInstance().DungeonLevel++;
                SaveManager.Save();
                ScenesManager.Instance.LoadLoadingScene("Lvl_Boss_" + bossLevel); // Load boss level

            }
            else
            {
                randomLevelID = UnityEngine.Random.Range(0, 4);
                PlayerStats.GetInstance().DungeonLevel++;
                SaveManager.Save();
                ScenesManager.Instance.LoadLoadingScene("Lvl_" + randomLevelID); // Load next level
            }
        }

        /*
        void GoToNextLevel()
        {
            int numberOfLevels = 4; // Number of available levels
            int currentLevelID = ScenesManager.Instance.levelID;
            int randomLevelID = currentLevelID;


            while (randomLevelID == currentLevelID) // Ensure the new level is different from the current one
            {
                randomLevelID = (int)Random.Range(0, numberOfLevels);
            }

            ScenesManager.Instance.LoadLoadingScene("Lvl_" + randomLevelID);
        }*/



    }
}
