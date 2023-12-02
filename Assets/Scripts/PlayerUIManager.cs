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

        private PlayerStats playerStatsReference;

        void Start()
        {
            playerStatsReference = PlayerStats.Instance;

            // Update UI based on PlayerStats
            UpdateUI();
        }


        // Update the UI based on PlayerStats values
        public void UpdateUI()
        {
            hpText.text = playerStatsReference.HP.max.ToString();
            strText.text = playerStatsReference.Damage.ToString();
            aglText.text = playerStatsReference.Agility.ToString();
            defText.text = playerStatsReference.Armor.ToString();
            moneyText.text = playerStatsReference.Money.ToString();
                
            // Update other UI elements as needed
        }

        public void AddStr()
        {
            if(playerStatsReference.Damage < 100)
            {
                playerStatsReference.Damage += 1;
                SaveManager.Save();
                UpdateUI();
            }   
        }

        public void AddAgl()
        {
            if (playerStatsReference.Agility < 100)
            {
                playerStatsReference.Agility += 1;
                SaveManager.Save();
                UpdateUI();
            }
        }

        public void AddDef()
        {
            if (playerStatsReference.Armor < 100)
            {
                playerStatsReference.Armor += 1;
                SaveManager.Save();
                UpdateUI();
            }
        }

        public void RemoveStr()
        {
            if(playerStatsReference.Damage > 1)
            {
                playerStatsReference.Damage -= 1;
                SaveManager.Save();
                UpdateUI();
            }
        }

        public void RemoveAgl()
        {
            if (playerStatsReference.Agility > 1)
            {
                playerStatsReference.Agility -= 1;
                SaveManager.Save();
                UpdateUI();
            }
        }

        public void RemoveDef()
        {
            if (playerStatsReference.Armor > 1)
            {
                playerStatsReference.Armor -= 1;
                SaveManager.Save();
                UpdateUI();
            }
        }

    }
}
