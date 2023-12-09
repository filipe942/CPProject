using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;

public class FrequentsItem : MonoBehaviour
{
    public Text titleText;
    public Text descriptionText;
    //public TMPro.TextMeshProUGUI endDateText;
    public string frequencyText;
    public string difficultyText;

    public Button trivialButton;
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;
    public Button dailyButton;
    public Button weeklyButton;
    public Button monthlyButton;
    private Button selectedButton;
    private Button selectedButton2;

    [System.Serializable]
    public class Frequents
    {
        public string title;
        public string description;
        public string frequency;
        public string difficulty;
    }

    private List<Frequents> frequentsList = new List<Frequents>(); // Declare the list to store Frequents items

    void Start()
    {
        // Load existing Frequents list or create a new one
        LoadOrCreateFrequentsList();

        PrintLoadedFrequentsList();
    }

    private void LoadOrCreateFrequentsList()
    {
        string filePath = GetFrequentsListFilePath();

        if (File.Exists(filePath))
        {
            // If the file exists, read the binary data from the file
            Debug.Log("Loaded existing Frequents list");
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fileStream = File.Open(filePath, FileMode.Open))
                {
                    frequentsList = (List<Frequents>)formatter.Deserialize(fileStream);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error loading Frequents list: {e.Message}");
            }
        }
        else
        {
            Debug.Log("Created new Frequents list");
            frequentsList = new List<Frequents>();
            SaveFrequentsList();
        }
    }

    public void SelectTrivial()
    {
        difficultyText = "Trivial";
        UpdateButtonSelection(trivialButton);
    }

    public void SelectEasy()
    {
        difficultyText = "Easy";
        UpdateButtonSelection(easyButton);
    }

    public void SelectMedium()
    {
        difficultyText = "Medium";
        UpdateButtonSelection(mediumButton);
    }

    public void SelectHard()
    {
        difficultyText = "Hard";
        UpdateButtonSelection(hardButton);
    }

     public void SelectDaily()
    {
        frequencyText = "Daily";
        UpdateButtonSelection2(dailyButton);
    }

     public void SelectWeekly()
    {
        frequencyText = "Weekly";
        UpdateButtonSelection2(weeklyButton);
    }

     public void SelectMonthly()
    {
        frequencyText = "Monthly";
        UpdateButtonSelection2(monthlyButton);
    }

    // Helper method to update the button selection
    private void UpdateButtonSelection(Button newSelection)
    {
        // Unhighlight the previously selected button
        if (selectedButton != null)
        {
            ColorBlock previousColors = selectedButton.colors;
            previousColors.normalColor = Color.white;
            selectedButton.colors = previousColors;
            LayoutRebuilder.ForceRebuildLayoutImmediate(selectedButton.transform as RectTransform);
        }

        // Highlight the newly selected button
        selectedButton = newSelection;
        ColorBlock newColors = selectedButton.colors;
        newColors.normalColor = Color.red;
        selectedButton.colors = newColors;

        Canvas.ForceUpdateCanvases();
    }

    private void UpdateButtonSelection2(Button newSelection)
    {
        // Unhighlight the previously selected button
        if (selectedButton2 != null)
        {
            ColorBlock previousColors = selectedButton2.colors;
            previousColors.normalColor = Color.white;
            selectedButton2.colors = previousColors;
            LayoutRebuilder.ForceRebuildLayoutImmediate(selectedButton2.transform as RectTransform);
        }

        // Highlight the newly selected button
        selectedButton2 = newSelection;
        ColorBlock newColors = selectedButton2.colors;
        newColors.normalColor = Color.red;
        selectedButton2.colors = newColors;

        Canvas.ForceUpdateCanvases();
    }

    public void SetFrequentsData()
    {
        Debug.Log("SetFrequentsData");

        Frequents newFrequents = new Frequents
        {
            title = titleText.text,
            description = descriptionText.text,
            //endDate = endDateText.text,
            frequency = frequencyText,
            difficulty = difficultyText
        };

        // Add the new Frequents to the list
        frequentsList.Add(newFrequents);

        // Save the updated list to a file using binary serialization
        SaveFrequentsList();

        PrintSavedFrequentsList();

        // Load the next scene
        LoadNextScene();
    }

    private void PrintSavedFrequentsList()
    {
        Debug.Log("Saved Frequents List:");

        foreach (Frequents frequents in frequentsList)
        {
            Debug.Log($"Title: {frequents.title}, Description: {frequents.description}, Frequency: {frequents.frequency}, Difficulty: {frequents.difficulty}");
        }
    }

    private void PrintLoadedFrequentsList()
    {
        Debug.Log("Loaded Frequents List:");

        foreach (Frequents frequents in frequentsList)
        {
            Debug.Log($"Title: {frequents.title}, Description: {frequents.description}, Frequency: {frequents.frequency}, Difficulty: {frequents.difficulty}");
        }
    }

    private void SaveFrequentsList()
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string filePath = GetFrequentsListFilePath();

            // Write the binary data to the file
            using (FileStream fileStream = File.Create(filePath))
            {
                formatter.Serialize(fileStream, frequentsList);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error saving Frequents list: {e.Message}");
        }
    }

    private void LoadNextScene()
    {
        // Load the next scene (you can customize the scene name)
        SceneManager.LoadScene("TaskMenuFrequents");
    }

    private string GetFrequentsListFilePath()
    {
        // Choose a path for your file (you can customize the path)
        string fileName = "FrequentsList.dat";
        string path = Path.Combine(Application.persistentDataPath, fileName);
        return path;
    }
}