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
            PlayerPrefs.SetFloat("Saved_XP", PlayerStats.Instance.ExperiencePoints);
            PlayerPrefs.SetInt("Saved_PlayerLevel", PlayerStats.Instance.Level);
            PlayerPrefs.SetInt("Saved_Points", PlayerStats.Instance.Points);
            PlayerPrefs.SetInt("Saved_DungeonLevel", PlayerStats.Instance.DungeonLevel);
            //PlayerPrefs.SetInt("Saved_Money", PlayerStats.Instance.Money);

            PlayerPrefs.SetString("Saved_Level", SceneManager.GetActiveScene().name);

            PlayerPrefs.Save();
        }

        public static bool HasSave()
        {
            // Check if there is a save by looking at a specific key or file
            return PlayerPrefs.GetInt("Saved_PlayerLevel") > 1;
        }

        public static void Load()
        {
            if (HasSave())
            {
                Player.Instance.HP = new DoubleFloat(PlayerPrefs.GetFloat("Saved_HP"), PlayerPrefs.GetFloat("Saved_HP"));
                Player.Instance.Damage = PlayerPrefs.GetFloat("Saved_Damage");
                Player.Instance.Armor = PlayerPrefs.GetFloat("Saved_Armor");
                Player.Instance.Agility = PlayerPrefs.GetFloat("Saved_Agility");
                Player.Instance.ExperiencePoints = PlayerPrefs.GetFloat("Saved_XP");
                Player.Instance.Level = PlayerPrefs.GetInt("Saved_PlayerLevel");
                Player.Instance.Points = PlayerPrefs.GetInt("Saved_Points");
                Player.Instance.DungeonLevel = PlayerPrefs.GetInt("Saved_DungeonLevel");
                //PlayerStats.Instance.Money = PlayerPrefs.GetInt("Saved_Money");
            }
        }


    }
}