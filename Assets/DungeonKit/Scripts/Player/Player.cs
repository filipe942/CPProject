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

            HP = new DoubleFloat(200f, 200f);
            Armor = 0.5f;
            Agility = 1 + (0.1f / 4);
            Damage = 0.5f;

            PlayerStats.Instance.InitializeFromPlayer(this);
        }

    }
}