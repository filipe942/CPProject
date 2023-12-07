using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;

public class HabitItem : MonoBehaviour
{
    public Text titleText;
    public Text descriptionText;
    //public TMPro.TextMeshProUGUI endDateText;
    public string typeText;
    public string difficultyText;

    public Button trivialButton;
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;
    public Button positiveButton;
    public Button negativeButton;
    private Button selectedButton;

    [System.Serializable]
    public class Habit
    {
        public string title;
        public string description;
        //public string endDate;
        public string type;
        public string difficulty;
    }

    private List<Habit> habitList = new List<Habit>(); // Declare the list to store Habit items

    void Start()
    {
        // Load existing Habit list or create a new one
        LoadOrCreateHabitList();

        PrintLoadedHabitList();
    }

    private void LoadOrCreateHabitList()
    {
        string filePath = GetHabitListFilePath();

        if (File.Exists(filePath))
        {
            // If the file exists, read the binary data from the file
            Debug.Log("Loaded existing Habit list");
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fileStream = File.Open(filePath, FileMode.Open))
                {
                    habitList = (List<Habit>)formatter.Deserialize(fileStream);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error loading Habit list: {e.Message}");
            }
        }
        else
        {
            Debug.Log("Created new Habit list");
            habitList = new List<Habit>();
            SaveHabitList();
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

    public void SelectPositive()
    {
        typeText = "Positive";
        UpdateButtonSelection2(positiveButton);
    }

    public void SelectNegative()
    {
        typeText = "Negative";
        UpdateButtonSelection2(negativeButton);
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

    public void SetHabitData()
    {
        Debug.Log("SetHabitData");

        Habit newHabit = new Habit
        {
            title = titleText.text,
            description = descriptionText.text,
            //endDate = endDateText.text,
            type = typeText,
            difficulty = difficultyText
        };

        // Add the new Habit to the list
        habitList.Add(newHabit);

        // Save the updated list to a file using binary serialization
        SaveHabitList();

        PrintSavedHabitList();

        // Load the next scene
        LoadNextScene();
    }
    
    private void PrintSavedHabitList()
    {
        Debug.Log("Saved Habit List:");

        foreach (Habit habit in habitList)
        {
            Debug.Log($"Title: {habit.title}, Description: {habit.description}, Type: {habit.type}, Difficulty: {habit.difficulty}");
        }
    }

    private void PrintLoadedHabitList()
    {
        Debug.Log("Loaded Habit List:");

        foreach (Habit habit in habitList)
        {
            Debug.Log($"Title: {habit.title}, Description: {habit.description}, Type: {habit.type}, Difficulty: {habit.difficulty}");
        }
    }

    private void SaveHabitList()
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string filePath = GetHabitListFilePath();

            // Write the binary data to the file
            using (FileStream fileStream = File.Create(filePath))
            {
                formatter.Serialize(fileStream, habitList);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error saving Habit list: {e.Message}");
        }
    }

    private void LoadNextScene()
    {
        // Load the next scene (you can customize the scene name)
        SceneManager.LoadScene("TaskMenuHabits");
    }

    private string GetHabitListFilePath()
    {
        // Choose a path for your file (you can customize the path)
        string fileName = "HabitList.dat";
        string path = Path.Combine(Application.persistentDataPath, fileName);
        return path;
    }
}
