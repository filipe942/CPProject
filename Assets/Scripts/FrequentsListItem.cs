using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DungeonKIT;

public class FrequentsListItem : MonoBehaviour
{
    public Text titleText;
    public Text descriptionText;
    public Text difficultyText;
    public Text frequencyText;

    private List<FrequentsItem.Frequents> frequentsList;

    private const float SecondsInADay = 86400f;
    private const float SecondsInAWeek = 604800f; 
    private const float SecondsInAMonth = 2592000f;

    private const string FirstSelectionKeyPrefix = "FirstSelection_";

    public void SetFrequentsItemData(string title, string description, string difficulty, string frequency)
    {
        titleText.text = title;
        descriptionText.text = description;
        difficultyText.text = frequency;
        frequencyText.text = difficulty;
    }

    public void DeleteFrequents()
    {
        List<FrequentsItem.Frequents> frequentsList = GetFrequentsList();

        int index = frequentsList.FindIndex(frequents =>
            frequents.title == titleText.text &&
            frequents.description == descriptionText.text &&
            frequents.difficulty == difficultyText.text &&
            frequents.frequency == frequencyText.text
        );

        if (index != -1)
        {
            frequentsList.RemoveAt(index);

            SaveFrequentsList(frequentsList);
        }

        Destroy(gameObject);
    }

    public void DeleteFrequentsItem()
    {
        if (CanSelectItem())
        {
            List<FrequentsItem.Frequents> frequentsList = GetFrequentsList();

            int index = frequentsList.FindIndex(frequents =>
                frequents.title == titleText.text &&
                frequents.description == descriptionText.text &&
                frequents.difficulty == difficultyText.text &&
                frequents.frequency == frequencyText.text
            );

            if (index != -1)
            {
                UpdateLastSelectionTimestamp(titleText.text);
            }
        }
        else
        {
            Debug.Log("Cannot select item. Selection limit reached.");
        }
    }

    public void DeleteFrequentsItemWithPoints()
    {
        if (CanSelectItem())
        {
            List<FrequentsItem.Frequents> frequentsList = GetFrequentsList();

            int index = frequentsList.FindIndex(frequents =>
                frequents.title == titleText.text &&
                frequents.description == descriptionText.text &&
                frequents.difficulty == difficultyText.text &&
                frequents.frequency == frequencyText.text
            );

            if (index != -1)
            {
                int xpPoints = CalculateXpPoints(difficultyText.text);
                Debug.Log($"Gained {xpPoints} XP points for completing task with difficulty: {difficultyText.text}");
                PlayerStats.Instance.GainXP(xpPoints);
                UpdateLastSelectionTimestamp(titleText.text);
            }
        }
        else
        {
            Debug.Log("Cannot select item. Selection limit reached.");
        }
    }

    private bool CanSelectItem()
    {
        float lastSelectionTimestamp = PlayerPrefs.GetFloat(GetLastSelectionKey(titleText.text), 0f);
        float currentTime = Time.time;

        float selectionLimit = 0f;

        switch (frequencyText.text)
        {
            case "Daily":
                selectionLimit = SecondsInADay;
                break;
            case "Weekly":
                selectionLimit = SecondsInAWeek;
                break;
            case "Monthly":
                selectionLimit = SecondsInAMonth;
                break;
            default:
                Debug.LogError($"Unsupported frequency: {frequencyText.text}");
                return false;
        }

        if (!PlayerPrefs.HasKey(GetFirstSelectionKey(titleText.text)))
        {
            PlayerPrefs.SetInt(GetFirstSelectionKey(titleText.text), 1);
            PlayerPrefs.Save();
            return true;
        }

        return currentTime > lastSelectionTimestamp + selectionLimit;
    }

    private string GetFirstSelectionKey(string title)
    {
        return $"{FirstSelectionKeyPrefix}{title.Replace(" ", "")}";
    }

    private void UpdateLastSelectionTimestamp(string title)
    {
        PlayerPrefs.SetFloat(GetLastSelectionKey(title), Time.time);
        PlayerPrefs.Save();
    }

    private string GetLastSelectionKey(string title)
    {
        return $"LastFrequentsSelection_{title.Replace(" ", "")}";
    }

    private int CalculateXpPoints(string difficulty)
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



    private List<FrequentsItem.Frequents> GetFrequentsList()
    {
        if (frequentsList == null)
        {
            frequentsList = LoadFrequentsList();
        }
        return frequentsList;
    }

    private void SaveFrequentsList(List<FrequentsItem.Frequents> updatedList)
    {
        frequentsList = updatedList;

        string filePath = GetFrequentsListFilePath();
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
            Debug.LogError($"Error saving Frequents list: {e.Message}");
        }
    }

    private List<FrequentsItem.Frequents> LoadFrequentsList()
    {
        // Load the list from the .dat file
        string filePath = GetFrequentsListFilePath();

        if (File.Exists(filePath))
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fileStream = File.Open(filePath, FileMode.Open))
                {
                    return (List<FrequentsItem.Frequents>)formatter.Deserialize(fileStream);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error loading Frequents list: {e.Message}");
            }
        }
        return new List<FrequentsItem.Frequents>();
    }

    private string GetFrequentsListFilePath()
    {
        string fileName = "FrequentsList.dat";
        return Path.Combine(Application.persistentDataPath, fileName);
    }
}
