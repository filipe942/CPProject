using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DungeonKIT
{
    public class Player : MonoBehaviour
    {
        public static Player Instance; //Singleton

        //PlayerStats playerStats = PlayerStats.Instance;

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

        [SerializeField] private int _money;
        public int Money
        {
            get { return _money; }
            set { _money = value; }
        }

        [SerializeField] private int _points;
        public int Points
        {
            get { return _points; }
            set { _points = value; }
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            HP = new DoubleFloat(500f, 500f);
            Armor = 0.5f;
            Agility = 0.2f;
            Damage = 3f;
            Points = 0;

            if (SaveManager.HasSave())
            {
                print("sda");
                // Load saved stats
                SaveManager.Load();
            }
            else
            {
                print("else");
                // No save found, create a new PlayerStats instance
                GameObject playerStatsObject = new GameObject("PlayerStats");
                PlayerStats playerStatsInstance = playerStatsObject.AddComponent<PlayerStats>();

                // Set the instance as the newly created PlayerStats
                PlayerStats.Instance = playerStatsInstance;

                // Mark the PlayerStats GameObject as not to be destroyed on scene changes
                DontDestroyOnLoad(playerStatsObject);

                // Initialize PlayerStats from the current player
                PlayerStats.Instance.InitializeFromPlayer(this);
            }
        }
    }
}