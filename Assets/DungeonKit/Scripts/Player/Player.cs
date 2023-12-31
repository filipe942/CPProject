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
        [SerializeField] private DoubleFloat _HP;
        
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

        [SerializeField] private int _dungeonLevel;
        public int DungeonLevel
        {
            get { return _dungeonLevel; }
            set { _dungeonLevel = value; }
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;

                // Check if there is a save
                if (SaveManager.HasSave())
                {
                    SaveManager.Load(); // Load the saved values from PlayerPrefs
                }
                else
                {
                    print("aqui");
                    // No save found, use default values
                    HP = new DoubleFloat(50f, 50f);
                    Armor = 0.4f;
                    Agility = 0.8f;
                    Damage = 1;
                    Points = 0;
                    Level = 1;
                    ExperiencePoints = 0;
                    DungeonLevel = 1;

                    SaveManager.FirstSave();
                }
            }
        }
    }
}