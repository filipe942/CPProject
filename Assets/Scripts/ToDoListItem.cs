using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DungeonKIT;

public class ToDoListItem : MonoBehaviour
{
    public Text titleText;
    public Text descriptionText;
    public Text difficultyText;

    private List<ToDoItem.ToDo> todoList;

    public void SetToDoItemData(string title, string description, string difficulty)
    {
        titleText.text = title;
        descriptionText.text = description;
        difficultyText.text = difficulty;
    }

    public void DeleteToDoItem()
    {
        List<ToDoItem.ToDo> todoList = GetToDoList();

        int index = todoList.FindIndex(todo =>
            todo.title == titleText.text &&
            todo.description == descriptionText.text &&
            todo.difficulty == difficultyText.text
        );

        if (index != -1)
        {
            todoList.RemoveAt(index);

            SaveToDoList(todoList);
        }

        Destroy(gameObject);
    }

    public void DeleteToDoItemWithPoints()
    {
        List<ToDoItem.ToDo> todoList = GetToDoList();
        print(todoList.Count);

        int index = todoList.FindIndex(todo =>
            todo.title == titleText.text &&
            todo.description == descriptionText.text &&
            todo.difficulty == difficultyText.text
        );

        if (index != -1)
        {
            int xpPoints = CalculateXpPoints(difficultyText.text);
            Debug.Log($"Gained {xpPoints} XP points for completing task with difficulty: {difficultyText.text}");
            PlayerStats.Instance.GainXP(xpPoints);
            todoList.RemoveAt(index);
            SaveToDoList(todoList);
        }

        Destroy(gameObject);
    }

    private int CalculateXpPoints(string difficulty)
    {
        switch (difficulty)
        {
            case "Trivial":
                return 10;
            case "Easy":
                return 15;
            case "Medium":
                return 25;
            case "Hard":
                return 35;
            default:
                return 0;
        }
    }

    private List<ToDoItem.ToDo> GetToDoList()
    {
        if (todoList == null)
        {
            todoList = LoadToDoList();
        }
        return todoList;
    }

    private void SaveToDoList(List<ToDoItem.ToDo> updatedList)
    {
        todoList = updatedList;

        string filePath = GetToDoListFilePath();
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
            Debug.LogError($"Error saving ToDo list: {e.Message}");
        }
    }

    private List<ToDoItem.ToDo> LoadToDoList()
    {
        // Load the list from the .dat file
        string filePath = GetToDoListFilePath();

        if (File.Exists(filePath))
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fileStream = File.Open(filePath, FileMode.Open))
                {
                    return (List<ToDoItem.ToDo>)formatter.Deserialize(fileStream);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error loading ToDo list: {e.Message}");
            }
        }
        return new List<ToDoItem.ToDo>();
    }

    private string GetToDoListFilePath()
    {
        string fileName = "ToDoList.dat";
        return Path.Combine(Application.persistentDataPath, fileName);
    }
}
