using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonKIT
{
    public class AIStats : MonoBehaviour
    {
        //Cached components
        AIController aiController;
        AICanvas aICanvas;
        SpriteRenderer aiSprite;
        AudioSource audioSource;
        public EnemyManager enemyManager;

        //Event
        public delegate void DeathAction(); // AI Death Event
        public event DeathAction onDeath;

        [HideInInspector] public DamageEffect damageEffect; //Visual damage effect

        [Header("Settings")]
        public DoubleFloat enemyHP = new DoubleFloat(100, 100); //DoubleFloat(currentHP,maxHP)

        public float attackDamage;
        public float attackSpeed;

        private void Start()
        {
            aiSprite = GetComponentInChildren<SpriteRenderer>();
            aICanvas = GetComponentInChildren<AICanvas>();
            aiController = GetComponent<AIController>();
            audioSource = GetComponent<AudioSource>();
            enemyManager = FindObjectOfType<EnemyManager>();
            if (enemyManager == null)
            {
                Debug.LogError("EnemyManager not found!");
            }
        }

        //Сaused by taking damage
        public void TakingDamage(float damage)
        {
            aiController.isAttacked = true; //sends AI that he was attacked

            enemyHP.current -= damage; //damage
            aICanvas.UpdateUI(); //Update AI ui (hp bar)

            AudioManager.Instance.Play(audioSource, AudioManager.Instance.aiDamage, false); //play damage sound

            StartCoroutine(damageEffect.Damage(aiSprite)); //Start damage effect

            if (enemyHP.current <= 0) //if HP < 0 Death
            {
                Death();
            }
        }

        void Death()
        {
            if (onDeath != null)
                onDeath(); // Death event

            Destroy(gameObject);
            enemyManager.EnemyDestroyed();
        }


    }
}
