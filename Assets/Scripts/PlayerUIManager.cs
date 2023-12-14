using UnityEngine;
using UnityEngine.UI;

namespace DungeonKIT
{
    public class PlayerUIManager : MonoBehaviour
    {
        [SerializeField] Text hpText; // Reference to the UI Text element displaying HP
        [SerializeField] Text strText;
        [SerializeField] Text aglText;
        [SerializeField] Text defText;
        [SerializeField] Text pointsText;
        [SerializeField] Text PlayerLvlPage;
        [SerializeField] Text PlayerLvlSideBar;
        [SerializeField] Text DungeonLVL;
        [SerializeField] Text DungeonLVLBut;

        private PlayerStats playerStatsReference;

        void Start()
        {
            playerStatsReference = PlayerStats.Instance;

            // Update UI based on PlayerStats
            UpdateUI();
        }

        private void Update()
        {
            UpdateUI();
        }


        // Update the UI based on PlayerStats values
        public void UpdateUI()
        {
            if(PlayerLvlPage) { 
            PlayerLvlPage.text = playerStatsReference.Level.ToString();
            }
            if(PlayerLvlSideBar)
            {
                PlayerLvlSideBar.text = playerStatsReference.Level.ToString();
            }

           if(hpText)
            {
                hpText.text = playerStatsReference.HP.max.ToString();
            }
            
            if(strText)
            {
                float damagePercentage = Mathf.RoundToInt(Mathf.InverseLerp(1, 100, playerStatsReference.Damage) * 100f);
                strText.text = Mathf.Clamp(damagePercentage, 1, 100).ToString();
            }

            if (aglText)
            {
                float agilityPercentage = Mathf.RoundToInt(Mathf.InverseLerp(0.8f, 1.5f, playerStatsReference.Agility) * 100f);
                aglText.text = Mathf.Clamp(agilityPercentage, 1, 100).ToString();
            }

            if (defText)
            {
                float armorPercentage = Mathf.RoundToInt(Mathf.InverseLerp(0.4f, 0.85f, playerStatsReference.Armor) * 100f);
                defText.text = Mathf.Clamp(armorPercentage, 1, 100).ToString();
            }

            if (pointsText) 
            {
                pointsText.text = playerStatsReference.Points.ToString();
            }
            if (DungeonLVL)
            {
                DungeonLVL.text = playerStatsReference.DungeonLevel.ToString();
            }
            if (DungeonLVLBut)
            {
                DungeonLVLBut.text = "Play LVL " + playerStatsReference.DungeonLevel.ToString();
            }
            
                
            // Update other UI elements as needed
        }

        public float Convert(float maximum , float current)
        {
            return (current * 100) / maximum;
        }

        public void AddStr()
        {
            if (playerStatsReference.Points != 0 && playerStatsReference.Damage <100)
            { 
                if(playerStatsReference.Damage < 100)
                {
                    playerStatsReference.Damage += 1;
                    //playerStatsReference.Points -= 1;
                    SaveManager.Save();
                    UpdateUI();
                } 
            }  
        }

        public void AddAgl()
        {
            if (playerStatsReference.Points != 0 && playerStatsReference.Agility < 1.5)
            {   
                if (playerStatsReference.Agility < 1.5)
                {
                    playerStatsReference.Agility += 0.007f;
                    playerStatsReference.Points -= 1;
                    SaveManager.Save();
                    UpdateUI();
                }
                if (playerStatsReference.Agility >= 1.5)
                {
                    playerStatsReference.Agility = 1.5f;
                    //playerStatsReference.Points -= 1;
                    SaveManager.Save();
                    UpdateUI();
                } 
            }
        }

        public void AddDef()
        {
            if (playerStatsReference.Points != 0 && playerStatsReference.Armor < 1)
            {
                if (playerStatsReference.Armor < 0.85)
                {
                    playerStatsReference.Armor += 0.0045f;
                    playerStatsReference.Points -= 1;
                    SaveManager.Save();
                    UpdateUI();
                }
                if (playerStatsReference.Armor >= 1)
                {
                    playerStatsReference.Armor = 1f;
                    //playerStatsReference.Points -= 1;
                    SaveManager.Save();
                    UpdateUI();
                }
            }
        }

        public void RemoveStr()
        {
            if (playerStatsReference.Damage > 0)
            {
                if(playerStatsReference.Damage > 1)
                {
                    playerStatsReference.Damage -= 1;
                    playerStatsReference.Points += 1;
                    SaveManager.Save();
                    UpdateUI();
                }
            }
        }

        public void RemoveAgl()
        {
            if (playerStatsReference.Agility > 0.8f)
            {
                if (playerStatsReference.Agility > 0.8)
                {
                    playerStatsReference.Agility -= 0.007f;
                    playerStatsReference.Points += 1;
                    SaveManager.Save();
                    UpdateUI();
                }
                //if (playerStatsReference.Agility <= 0.8)
                //{
                //    playerStatsReference.Agility = 0.8f;
                //    playerStatsReference.Points += 1;
                //    SaveManager.Save();
                //    UpdateUI();
                //}
            }
        }

        public void RemoveDef()
        {
            if (playerStatsReference.Armor > 0.4)
            {
                if (playerStatsReference.Armor > 0.4)
                {
                    playerStatsReference.Armor -= 0.0045f;
                    playerStatsReference.Points += 1;
                    SaveManager.Save();
                    UpdateUI();
                }
                //if (playerStatsReference.Armor <= 0)
                //{
                //    playerStatsReference.Armor = 0f;
                //    playerStatsReference.Points += 1;
                //    SaveManager.Save();
                //    UpdateUI();
                //}
            }
        }
    }
}
