using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LoadTodos : MonoBehaviour
{
    private List<ToDoItem.ToDo> todoList = new List<ToDoItem.ToDo>();

    // Start is called before the first frame update
    void Start()
    {
        LoadAllTodos();
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
                    todoList = (List<ToDoItem.ToDo>)formatter.Deserialize(fileStream);

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
}
