using UnityEngine;
using UnityEngine.SceneManagement;

namespace DungeonKIT
{
    public class SaveManager : MonoBehaviour
    {

        public static void FirstSave()
        {
            PlayerPrefs.SetFloat("Saved_HP", Player.Instance.HP.max);
            PlayerPrefs.SetFloat("Saved_Damage", Player.Instance.Damage);
            PlayerPrefs.SetFloat("Saved_Armor", Player.Instance.Armor);
            PlayerPrefs.SetFloat("Saved_Agility", Player.Instance.Agility);
            PlayerPrefs.SetFloat("Saved_XP", Player.Instance.ExperiencePoints);
            PlayerPrefs.SetInt("Saved_PlayerLevel", Player.Instance.Level);
            PlayerPrefs.SetInt("Saved_Points", Player.Instance.Points);
            PlayerPrefs.SetInt("Saved_DungeonLevel", Player.Instance.DungeonLevel);

            PlayerPrefs.Save();
        }

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

            PlayerPrefs.Save();
        }
        public static void SaveDungeonLVL(int Lvl)
        {
            PlayerPrefs.SetInt("Saved_DungeonLevel", Lvl);
            PlayerPrefs.Save();
        }
        public static void SaveDungeonFloor(string LvlName)
        {
            PlayerPrefs.SetString("Saved_Level", LvlName);
            PlayerPrefs.Save();
        }

        public static bool HasSave()
        {
            // Check if there is a save by looking at a specific key or file
            return PlayerPrefs.GetInt("Saved_PlayerLevel") > 0;
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
        private void OnApplicationQuit()
        {
            Save();
        }


    }
}