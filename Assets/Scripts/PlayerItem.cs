/*using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using DungeonKIT; 
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerItem : MonoBehaviour
{
    public string StrText;
    public string DefText;
    public string AglText;
    public string PointsText;

    public Button StrUpButton;
    public Button StrDownButton;
    public Button DefUpButton;
    public Button DefDownButton;
    public Button AglUpButton;
    public Button AglDownButton;

    private PlayerStats playerStatsReference;

    [System.Serializable]
    public class Players
    {
        public string str;
        public string agl;
        public string def;
        public string point;
    }

    private List<Players> playerList = new List<Players>(); // Declare the list to store player items

    void Start()
    {
        // Load existing Player list or create a new one
        LoadPlayer();

        PrintLoadedPlayer();
    }

    private void LoadPlayer()
    {
        string filePath = GetPlayerFilePath();

        if (File.Exists(filePath))
        {
            // If the file exists, read the binary data from the file
            Debug.Log("Loaded existing Player");
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fileStream = File.Open(filePath, FileMode.Open))
                {
                    playerList = (List<Players>)formatter.Deserialize(fileStream);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error loading Player: {e.Message}");
            }
        }
        else
        {
            Debug.Log("Created new Player");
            playerList = new List<Players>();
            SavePlayer();
        }
    }

    public void SelectStr()
    {
        playerStatsReference = PlayerStats.Instance;
        StrText = playerStatsReference.Damage.ToString();
        PointsText = playerStatsReference.Points.ToString();
    }

    public void SelectDef()
    {
        playerStatsReference = PlayerStats.Instance;
        DefText = playerStatsReference.Armor.ToString();
        PointsText = playerStatsReference.Points.ToString();
    }

    public void SelectAgl()
    {
        playerStatsReference = PlayerStats.Instance;
        AglText = playerStatsReference.Agility.ToString();
        PointsText = playerStatsReference.Points.ToString();
    }

    public void SetPlayerData()
    {
        Debug.Log("SetPlayerData");

        playerStatsReference.Damage = float.Parse(StrText);
        playerStatsReference.Armor = float.Parse(DefText);
        playerStatsReference.Agility = float.Parse(AglText);
        playerStatsReference.Points = int.Parse(PointsText);

        // Save the updated list to a file using binary serialization
        SavePlayer();

        PrintSavedPlayer();

        // Load the next scene
        //LoadNextScene();
    }

    private void PrintSavedPlayer()
    {
        Debug.Log("Saved Player:");

        Debug.Log($"Damage/Str: {playerStatsReference.Damage}, Armor/Def: {playerStatsReference.Armor}, Agility/Agl: {playerStatsReference.Agility}, Points: {playerStatsReference.Points}");
    }

    private void PrintLoadedPlayer()
    {
        Debug.Log("Loaded Player:");

        Debug.Log($"Damage/Str: {playerStatsReference.Damage}, Armor/Def: {playerStatsReference.Armor}, Agility/Agl: {playerStatsReference.Agility}, Points: {playerStatsReference.Points}");
    }

    private void SavePlayer()
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string filePath = GetPlayerFilePath();

            // Write the binary data to the file
            using (FileStream fileStream = File.Create(filePath))
            {
                formatter.Serialize(fileStream, playerList);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error saving Player: {e.Message}");
        }
    }

    private string GetPlayerFilePath()
    {
        // Choose a path for your file (you can customize the path)
        string fileName = "Player.dat";
        string path = Path.Combine(Application.persistentDataPath, fileName);
        return path;
    }
}
*/


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;


namespace DungeonKIT
{
    public class PlayerItem : MonoBehaviour
    {
        
        public string StrText;
        public string DefText;
        public string AglText;
        public string PointsText;

        public Button StrUpButton;
        public Button StrDownButton;
        public Button DefUpButton;
        public Button DefDownButton;
        public Button AglUpButton;
        public Button AglDownButton;

        private PlayerStats playerStatsReference;

        //[System.Serializable]
        /*public class Players
        {
            public string str;
            public string agl;
            public string def;
            public string point;
        }*/

        //private List<Players> playerList = new List<Players>(); // Declare the list to store player items

        void Start()
        {
            // Load existing Frequents list or create a new one
            LoadPlayer();

            PrintLoadedPlayer();
        }

        private void LoadPlayer()
        {
            string filePath = GetPlayerFilePath();

            if (File.Exists(filePath))
            {
                // If the file exists, read the binary data from the file
                Debug.Log("Loaded existing Player");
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    using (FileStream fileStream = File.Open(filePath, FileMode.Open))
                    {
                        playerStatsReference = (PlayerStats)formatter.Deserialize(fileStream);
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"Error loading Player: {e.Message}");
                }
            }
            else
            {
                Debug.Log("Created new Player");
                //playerList = new List<Players>();
                SavePlayer();
            }
        }

        public void SelectStr()
        {
            playerStatsReference = PlayerStats.Instance;
            StrText = playerStatsReference.Damage.ToString();
            PointsText = playerStatsReference.Points.ToString();
        }

        public void SelectDef()
        {
            playerStatsReference = PlayerStats.Instance;
            DefText = playerStatsReference.Armor.ToString();
            PointsText = playerStatsReference.Points.ToString();
        }


        public void SelectAgl()
        {
            playerStatsReference = PlayerStats.Instance;
            AglText = playerStatsReference.Agility.ToString();
            PointsText = playerStatsReference.Points.ToString();
        }

        public void SetPlayerData()
        {
            Debug.Log("SetPlayerData");

            playerStatsReference.Damage = float.Parse(StrText);
            playerStatsReference.Armor = float.Parse(DefText);
            playerStatsReference.Agility = float.Parse(AglText);
            playerStatsReference.Points = int.Parse(PointsText);
            

            // Save the updated list to a file using binary serialization
            SavePlayer();

            PrintSavedPlayer();

            // Load the next scene
            //LoadNextScene();
        }

        private void PrintSavedPlayer()
        {
            Debug.Log("Saved Player:");

            Debug.Log($"Damage/Str: {playerStatsReference.Damage}, Armor/Def: {playerStatsReference.Armor}, Agility/Agl: {playerStatsReference.Agility}, Points: {playerStatsReference.Points}"); 
        }

        private void PrintLoadedPlayer()
        {
            Debug.Log("Loaded Player:");

            Debug.Log($"Damage/Str: {playerStatsReference.Damage}, Armor/Def: {playerStatsReference.Armor}, Agility/Agl: {playerStatsReference.Agility}, Points: {playerStatsReference.Points}"); 
        }

        private void SavePlayer()
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                string filePath = GetPlayerFilePath();

                // Write the binary data to the file
                using (FileStream fileStream = File.Create(filePath))
                {
                    playerStatsReference = (PlayerStats)formatter.Deserialize(fileStream);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error saving Player: {e.Message}");
            }
        }

        /*private void LoadNextScene()
        {
            // Load the next scene (you can customize the scene name)
            SceneManager.LoadScene("TaskMenuFrequents");
        }*/

        private string GetPlayerFilePath()
        {
            // Choose a path for your file (you can customize the path)
            string fileName = "Player.dat";
            string path = Path.Combine(Application.persistentDataPath, fileName);
            return path;
        }
}
}