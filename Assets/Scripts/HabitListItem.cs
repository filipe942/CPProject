using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DungeonKIT;

public class HabitListItem : MonoBehaviour
{
    public Text titleText;
    public Text descriptionText;
    public Text difficultyText;
    public Text typeText;

    private const float SecondsInADay = 86400f;

    private List<HabitItem.Habit> habitList;

    public void SetHabitItemData(string title, string description, string difficulty, string type)
    {
        titleText.text = title;
        descriptionText.text = description;
        difficultyText.text = type;
        typeText.text = difficulty;
    }

    public void DeleteHabit()
    {
        List<HabitItem.Habit> habitList = GetHabitList();


        int index = habitList.FindIndex(habit =>
        {
            return habit.title == titleText.text &&
                   habit.description == descriptionText.text &&
                   habit.difficulty == difficultyText.text &&
                   habit.type == typeText.text;
        });

        if (index != -1)
        {
            habitList.RemoveAt(index);
            SaveHabitList(habitList);
            Destroy(gameObject);
        }
        else
        {
            print("Habit not found");
        }
    }

    public void DeleteHabitItem()
    {
        if (CanExecuteAction())
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
                int xpPoints = CalculateXpPoints(difficultyText.text, typeText.text, false);
                Debug.Log($"Gained {xpPoints} XP points for completing task with difficulty: {difficultyText.text}");
                PlayerStats.Instance.GainXP(xpPoints);
                UpdateLastExecutionTimestamp();
            }
        }
        else
        {
            Debug.Log("Cannot execute action. Daily limit reached.");
        }
    }

    public void DeleteHabitItemWithPoints()
    {
        if (CanExecuteAction())
        {
            print("entrou");
            List<HabitItem.Habit> habitList = GetHabitList();

            int index = habitList.FindIndex(habit =>
                habit.title == titleText.text &&
                habit.description == descriptionText.text &&
                habit.difficulty == difficultyText.text &&
                habit.type == typeText.text
            );

            if (index != -1)
            {
                int xpPoints = CalculateXpPoints(difficultyText.text, typeText.text, true);
                Debug.Log($"Gained {xpPoints} XP points for completing task with difficulty: {difficultyText.text}");
                PlayerStats.Instance.GainXP(xpPoints);
                UpdateLastExecutionTimestamp();
            }
        }
        else
        {
            Debug.Log("Cannot execute action. Daily limit reached.");
        }
    }

    private bool CanExecuteAction()
    {
        string firstExecutionKey = GetFirstExecutionKey();
        print(firstExecutionKey);
        if (!PlayerPrefs.HasKey(firstExecutionKey))
        {
            // First execution, allow and set the flag
            PlayerPrefs.SetInt(firstExecutionKey, 1);
            PlayerPrefs.Save();
            return true;
        }

        string lastExecutionKey = GetLastExecutionKey();
        float lastExecutionTimestamp = PlayerPrefs.GetFloat(lastExecutionKey, 0f);
        float currentTime = Time.time;

        return currentTime > lastExecutionTimestamp + SecondsInADay;
    }

    private void UpdateLastExecutionTimestamp()
    {
        string lastExecutionKey = GetLastExecutionKey();
        PlayerPrefs.SetFloat(lastExecutionKey, Time.time);
        PlayerPrefs.Save();
    }
    private string GetFirstExecutionKey()
    {
        return $"FirstHabitExecution_{titleText.text.Replace(" ", "")}";
    }
    private string GetLastExecutionKey()
    {
        return "LastHabitExecution_" + titleText.text.Replace(" ", ""); 
    }

    private int CalculateXpPoints(string difficulty, string type, bool did)
    {
        int basePoints = 0;

        switch (difficulty)
        {
            case "Trivial":
                basePoints = 5;
                break;
            case "Easy":
                basePoints = 10;
                break;
            case "Medium":
                basePoints = 20;
                break;
            case "Hard":
                basePoints = 30;
                break;
                // Handle other cases for 'difficulty'
        }

        if (type == "Negative")
        {
            basePoints = -basePoints;
        }

        return did ? basePoints : -basePoints;
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

