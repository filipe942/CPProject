using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
public class DynamicList : MonoBehaviour
{
    public GameObject listItemPrefab;
    public Transform listItemHolder;
    public int numOfListItems;

    [SerializeField] Text ToDoCount;

    private void Start()
    {
        
        LoadAllTodos();

        // Get the loaded ToDo list
        List<ToDoItem.ToDo> todoList = GetLoadedToDoList();

        todoList = FilterExpiredToDos(todoList);

        // Iterate over the length of the list
        for (int i = 0; i < todoList.Count; i++)
        {
            // Instantiate the listItemPrefab for each ToDo item
            GameObject listItem = Instantiate(listItemPrefab, listItemHolder);

            // Get the ToDoListItem component from the instantiated prefab
            ToDoListItem listItemScript = listItem.GetComponent<ToDoListItem>();

            // Set the data for the ToDoListItem based on the ToDo item in the list
            listItemScript.SetToDoItemData(todoList[i].title, todoList[i].description, todoList[i].difficulty);
        }

        ToDoCount.text = todoList.Count.ToString();
    }

    void Update()
    {        
        List<ToDoItem.ToDo> todoList = GetLoadedToDoList();
        todoList = FilterExpiredToDos(todoList);
        ToDoCount.text = todoList.Count.ToString();
    }

    private List<ToDoItem.ToDo> FilterExpiredToDos(List<ToDoItem.ToDo> todoList)
    {
        DateTime currentDate = DateTime.Now.Date;

        // Use RemoveAll to filter and remove expired ToDo items in a single step
        todoList.RemoveAll(todo =>
        {
            if (DateTime.TryParseExact(todo.endDate.Replace("\u200B", "").Trim(), "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime parsedDate))
            {
                return parsedDate < currentDate;
            }
            else
            {
                // Handle invalid date format
                Debug.LogError($"Invalid date format for ToDo item: {todo.endDate}");
                return true; // Remove items with invalid date format
            }
        });

        // Save the updated ToDo list
        SaveToDoList(todoList);

        return todoList;
    }


    private void SaveToDoList(List<ToDoItem.ToDo> todoList)
    {
        string filePath = GetToDoListFilePath();

        try
        {
            // Serialize and save the ToDo list
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fileStream = File.Create(filePath))
            {
                formatter.Serialize(fileStream, todoList);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error saving ToDo list: {e.Message}");
        }
    }

    private void LoadAllTodos()
    {
        string filePath = GetToDoListFilePath();

        if (File.Exists(filePath))
        {
            try
            {
                // If the file exists, read the binary data from the file
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fileStream = File.Open(filePath, FileMode.Open))
                {
                    // Deserialize the binary data into the list
                    List<ToDoItem.ToDo> todoList = (List<ToDoItem.ToDo>)formatter.Deserialize(fileStream);

                    // Print the loaded todoList
                    PrintLoadedToDoList(todoList);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error loading ToDo list: {e.Message}");
            }
        }
        else
        {
            print("No ToDo items found.");
        }
    }

    private void PrintLoadedToDoList(List<ToDoItem.ToDo> todoList)
    {
        print("Loaded ToDo List:");

        foreach (ToDoItem.ToDo todo in todoList)
        {
            print($"Title: {todo.title}, Description: {todo.description}, End Date: {todo.endDate}, Difficulty: {todo.difficulty}");
        }
    }

    private string GetToDoListFilePath()
    {
        // Choose a path for your file (you can customize the path)
        string fileName = "ToDoList.dat";
        string path = Path.Combine(Application.persistentDataPath, fileName);
        return path;
    }

    private List<ToDoItem.ToDo> GetLoadedToDoList()
    {
        string filePath = GetToDoListFilePath();

        if (File.Exists(filePath))
        {
            try
            {
                // If the file exists, read the binary data from the file
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fileStream = File.Open(filePath, FileMode.Open))
                {
                    // Deserialize the binary data into the list
                    List<ToDoItem.ToDo> todoList = (List<ToDoItem.ToDo>)formatter.Deserialize(fileStream);

                    // Return the loaded todoList
                    return todoList;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error loading ToDo list: {e.Message}");
            }
        }
        else
        {
            print("No ToDo items found.");
        }

        // If there was an error or no ToDo items found, return an empty list
        return new List<ToDoItem.ToDo>();
    }
}
