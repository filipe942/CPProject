using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonKIT
{
    public class PlayerStats : MonoBehaviour
    {
        public static PlayerStats Instance; //Singleton
        [HideInInspector] public AudioSource audioSource;
        [HideInInspector] public DamageEffect damageEffect; //Damage effect
        
        Player player;

        [Header("Variables")]
        [SerializeField] private DoubleFloat _HP = new DoubleFloat(3f, 3f);
        public DoubleFloat HP
        {
            get { return _HP; }
            set { _HP = value; }
        }

        [SerializeField] private float _armor;
        public float Armor
        {
            get { return _armor; }
            set { _armor = value; }
        }

        [SerializeField] private float _agility;
        public float Agility
        {
            get { return _agility; }
            set { _agility = value; }
        }

        [SerializeField] private float _damage;
        public float Damage
        {
            get { return _damage; }
            set { _damage = value; }
        }

        [SerializeField] private int _points;
        public int Points
        {
            get { return _points; }
            set { _points = value; }
        }

        [SerializeField] private Dictionary<int, bool> _doorKeys = new Dictionary<int, bool>();
        public Dictionary<int, bool> DoorKeys
        {
            get { return _doorKeys; }
            set { _doorKeys = value; }
        }

        [SerializeField] private int _dungeonLevel;
        public int DungeonLevel
        {
            get { return _dungeonLevel; }
            set { _dungeonLevel = value; }
        }

        [SerializeField] private int _level;
        public int Level
        {
            get { return _level; }
            set { _level = value; }
        }

        [SerializeField] private float _experiencePoints;
        public float ExperiencePoints
        {
            get { return _experiencePoints; }
            set { _experiencePoints = value; }
        }

        [Header("Parameters")]
        public float timeToDamage; //Time for pause between AI damage
        bool isDamaged;

        [Header("Graphics")]
        public SpriteRenderer playerSprite; //Player sprite

        [Header("Leveling")]
        public float XPPerLevel = 50f;

        private void Awake()
        {
            //Singleton
            if (PlayerStats.Instance != null && PlayerStats.Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;

                /*
                HP = new DoubleFloat(100f, 100f);

                Armor = 0f;
                Agility = 1 + (0.1f / 4);
                Damage = 0.1f;
                */

                // Get reference to the Player class
                player = Player.Instance;

                // Initialize from the Player class
                InitializeFromPlayer(player);
                SaveManager.Load(); 
                if (ScenesManager.Instance.continueGame)
                    SaveManager.Load();
                
                SaveManager.Save();
            }
        }
        public static PlayerStats GetInstance()
        {
            if (Instance == null)
            {
                // You might want to add some error handling here
                Debug.LogError("PlayerStats is not initialized!");
            }

            return Instance;
        }

        public void InitializeFromPlayer(Player player)
            {
                HP = player.HP;
                Armor = player.Armor;
                Agility = player.Agility;
                Damage = player.Damage;
                Points = player.Points;
                Level = player.Level;
                ExperiencePoints = player.ExperiencePoints;
                DungeonLevel = player.DungeonLevel;
            }

        //Taking damage method
        public void TakingDamage(float damageIntake)
        {
            if (!isDamaged) // if player isn't damaged
            {
                isDamaged = true; //block damage
                StartCoroutine(timeDamage()); //set timer to next damage

                HP.current -= damageIntake*(1- Armor); //HP - damageIntake*TheAmountOfArmor

                UIManager.Instance.UpdateUI(); //Update UI
                StartCoroutine(damageEffect.Damage(playerSprite)); //Damage effect

                AudioManager.Instance.Play(audioSource, AudioManager.Instance.playerDamage, false); //play damage sound

                if (HP.current <= 0) //If hp < 0
                {
                    Death(); //Lose 
                }
            }
        }
        public void GainXP(int xp)
        {
            ExperiencePoints += xp;
            if(ExperiencePoints < 0)
            {
                ExperiencePoints = 0;
            }
            SaveManager.Save();
            // Check for level up
            while (ExperiencePoints >= CalculateXPToNextLevel())
            {
                ExperiencePoints -= CalculateXPToNextLevel();
                LevelUp();
            }

        }

        private void LevelUp()
        {
            Level++;
            Points++;
            SaveManager.Save();
        }

        private float CalculateXPToNextLevel()
        {
            return XPPerLevel * Level;
        }
        //Death method
        void Death()
        {
            GameManager.Instance.GameOver(); //Game over in gamemanager
            Destroy(gameObject); //Destroy this GameObject
        }

        IEnumerator timeDamage()
        {
            yield return new WaitForSeconds(timeToDamage); //Wait timeToDamage
            isDamaged = false; //can damage again
        }
    }
}