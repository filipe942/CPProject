using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class HabitListItem : MonoBehaviour
{
    public Text titleText;
    public Text descriptionText;
    public Text difficultyText;
    public Text typeText;

    private List<HabitItem.Habit> habitList;

    public void SetHabitItemData(string title, string description, string difficulty, string type)
    {
        titleText.text = title;
        descriptionText.text = description;
        difficultyText.text = difficulty;
        typeText.text = type;
    }

    public void DeleteHabitItem()
    {
        List<HabitItem.Habit> habitList = GetHabitList();

        int index = habitList.FindIndex(habit =>
            habit.title == titleText.text &&
            habit.description == descriptionText.text &&
            habit.difficulty == difficultyText.text &&
            habit.type == typeText.text
        );

        if (index != -1)
        {
            habitList.RemoveAt(index);

            SaveHabitList(habitList);
        }

        Destroy(gameObject);
    }

    public void DeleteHabitItemWithPoints()
    {
        List<HabitItem.Habit> habitList = GetHabitList();
        print(habitList.Count);

        int index = habitList.FindIndex(habit =>
            habit.title == titleText.text &&
            habit.description == descriptionText.text &&
            habit.difficulty == difficultyText.text &&
            habit.type == typeText.text
        );

        if (index != -1)
        {
            int xpPoints = CalculateXpPoints(difficultyText.text, typeText.text);
            Debug.Log($"Gained {xpPoints} XP points for completing task with difficulty: {difficultyText.text}");
            habitList.RemoveAt(index);
            SaveHabitList(habitList);
        }

        Destroy(gameObject);
    }

    private int CalculateXpPoints(string difficulty, string type)
    {
        if (type == "Positive")
        {
            switch (difficulty)
            {
                case "Trivial":
                    return 5;
                case "Easy":
                    return 10;
                case "Medium":
                    return 20;
                case "Hard":
                    return 30;
                default:
                    return 0;
            }
        }
        else if (type == "Negative")
        {
           switch (difficulty)
            {
                case "Trivial":
                    return -5;
                case "Easy":
                    return -10;
                case "Medium":
                    return -20;
                case "Hard":
                    return -30;
                default:
                    return 0;
            } 
        }
        else
        {
            // Handle other cases for 'type'
            return 0; // or another appropriate default value
        }
    }

    private List<HabitItem.Habit> GetHabitList()
    {
        if (habitList == null)
        {
            habitList = LoadHabitList();
        }
        return habitList;
    }

    private void SaveHabitList(List<HabitItem.Habit> updatedList)
    {
        habitList = updatedList;

        string filePath = GetHabitListFilePath();
        try
        {
            using (FileStream fileStream = File.Create(filePath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, updatedList);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error saving Habit list: {e.Message}");
        }
    }

    private List<HabitItem.Habit> LoadHabitList()
    {
        // Load the list from the .dat file
        string filePath = GetHabitListFilePath();

        if (File.Exists(filePath))
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fileStream = File.Open(filePath, FileMode.Open))
                {
                    return (List<HabitItem.Habit>)formatter.Deserialize(fileStream);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error loading Habit list: {e.Message}");
            }
        }
        return new List<HabitItem.Habit>();
    }

    private string GetHabitListFilePath()
    {
        string fileName = "HabitList.dat";
        return Path.Combine(Application.persistentDataPath, fileName);
    }
}

