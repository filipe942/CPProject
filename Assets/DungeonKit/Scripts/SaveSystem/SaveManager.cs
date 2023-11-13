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
            PlayerPrefs.SetInt("Saved_Bottles", PlayerStats.Instance.Bottles);

            PlayerPrefs.SetString("Saved_Level", SceneManager.GetActiveScene().name);
        }

        public static void Load()
        {
            PlayerStats.Instance.HP = new DoubleFloat(PlayerPrefs.GetFloat("Saved_HP"), PlayerPrefs.GetFloat("Saved_HP"));
            PlayerStats.Instance.Damage= PlayerPrefs.GetFloat("Saved_Damage");
            PlayerStats.Instance.Armor= PlayerPrefs.GetFloat("Saved_Armor");
            PlayerStats.Instance.Agility= PlayerPrefs.GetFloat("Saved_Agility");
            PlayerStats.Instance.Money = PlayerPrefs.GetInt("Saved_Money");
            PlayerStats.Instance.Bottles = PlayerPrefs.GetInt("Saved_Bottles");
        }


    }
}