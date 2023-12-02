using UnityEngine;
using UnityEngine.SceneManagement;

namespace DungeonKIT
{
    public class SaveManager : MonoBehaviour
    {
        public static void Save()
        {
            PlayerPrefs.SetFloat("Saved_HP", PlayerStats.Instance.HP.max);
            PlayerPrefs.SetFloat("Saved_Damage",PlayerStats.Instance.Damage);
            PlayerPrefs.SetFloat("Saved_Armor", PlayerStats.Instance.Armor);
            PlayerPrefs.SetFloat("Saved_Agility",PlayerStats.Instance.Agility);            

            PlayerPrefs.SetInt("Saved_Money", PlayerStats.Instance.Money);

            PlayerPrefs.SetString("Saved_Level", SceneManager.GetActiveScene().name);

            PlayerPrefs.Save();
        }

        public static bool HasSave()
        {
            // Check if there is a save by looking at a specific key or file
            return PlayerPrefs.HasKey("Saved_HP"); // You can customize this check based on your save implementation
        }

        public static void Load()
        {
            if (HasSave())
            {
                PlayerStats.Instance.HP = new DoubleFloat(PlayerPrefs.GetFloat("Saved_HP"), PlayerPrefs.GetFloat("Saved_HP"));
                PlayerStats.Instance.Damage = PlayerPrefs.GetFloat("Saved_Damage");
                PlayerStats.Instance.Armor = PlayerPrefs.GetFloat("Saved_Armor");
                PlayerStats.Instance.Agility = PlayerPrefs.GetFloat("Saved_Agility");
                PlayerStats.Instance.Money = PlayerPrefs.GetInt("Saved_Money");
            }
        }


    }
}