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
        [SerializeField] Text moneyText;
        [SerializeField] Text pointsText;
        [SerializeField] Text moneyWon;

        private PlayerStats playerStatsReference;

        void Start()
        {
            playerStatsReference = PlayerStats.Instance;

            if (moneyText != null){
                AddMoney();
            }

            // Update UI based on PlayerStats
            UpdateUI();
        }


        // Update the UI based on PlayerStats values
        public void UpdateUI()
        {
            hpText.text = playerStatsReference.HP.max.ToString();
            strText.text = playerStatsReference.Damage.ToString();
            
            float agility = (playerStatsReference.Agility *100f) - 100f;
            aglText.text = agility.ToString();

            float armor = playerStatsReference.Armor*100f;
            defText.text = armor.ToString();

            moneyText.text = playerStatsReference.Money.ToString();
            pointsText.text = playerStatsReference.Points.ToString();
                
            // Update other UI elements as needed
        }

        public void AddStr()
        {
            if (playerStatsReference.Points != 0 && playerStatsReference.Damage <100)
            { 
                if(playerStatsReference.Damage < 100)
                {
                    playerStatsReference.Damage += 1;
                    playerStatsReference.Points -= 1;
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
                    playerStatsReference.Agility += 0.01f;
                    playerStatsReference.Points -= 1;
                    SaveManager.Save();
                    UpdateUI();
                }
                if (playerStatsReference.Agility >= 1.5)
                {
                    playerStatsReference.Agility = 1.5f;
                    playerStatsReference.Points -= 1;
                    SaveManager.Save();
                    UpdateUI();
                } 
            }
        }

        public void AddDef()
        {
            if (playerStatsReference.Points != 0 && playerStatsReference.Armor < 1)
            {
                if (playerStatsReference.Armor < 1)
                {
                    playerStatsReference.Armor += 0.01f;
                    playerStatsReference.Points -= 1;
                    SaveManager.Save();
                    UpdateUI();
                }
                if (playerStatsReference.Armor >= 1)
                {
                    playerStatsReference.Armor = 1f;
                    playerStatsReference.Points -= 1;
                    SaveManager.Save();
                    UpdateUI();
                }
            }
        }

        public void RemoveStr()
        {
            if (playerStatsReference.Points != 0 && playerStatsReference.Damage > 0)
            {
                if(playerStatsReference.Damage > 1)
                {
                    playerStatsReference.Damage -= 1;
                    playerStatsReference.Points -= 1;
                    SaveManager.Save();
                    UpdateUI();
                }
            }
        }

        public void RemoveAgl()
        {
            if (playerStatsReference.Points != 0 && playerStatsReference.Agility > 1f)
            {
                if (playerStatsReference.Agility > 1)
                {
                    playerStatsReference.Agility -= 0.01f;
                    playerStatsReference.Points -= 1;
                    SaveManager.Save();
                    UpdateUI();
                }
                if (playerStatsReference.Agility <= 1)
                {
                    playerStatsReference.Agility = 1f;
                    playerStatsReference.Points -= 1;
                    SaveManager.Save();
                    UpdateUI();
                }
            }
        }

        public void RemoveDef()
        {
            if (playerStatsReference.Points != 0 && playerStatsReference.Armor > 0)
            {
                if (playerStatsReference.Armor > 0)
                {
                    playerStatsReference.Armor -= 0.01f;
                    playerStatsReference.Points -= 1;
                    SaveManager.Save();
                    UpdateUI();
                }
                if (playerStatsReference.Armor <= 0)
                {
                    playerStatsReference.Armor = 0f;
                    playerStatsReference.Points -= 1;
                    SaveManager.Save();
                    UpdateUI();
                }
            }
        }

        public void AddMoney()
        {
            int money = int.Parse(moneyWon.text);
            playerStatsReference.Money += money;
        }

        public void RemoveMoney()
        {
            int money = int.Parse(moneyWon.text);
            playerStatsReference.Money -= money;
        }
    
    }
}
