using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;

public class ToDoItem : MonoBehaviour
{
    public Text titleText;
    public Text descriptionText;
    public TMPro.TextMeshProUGUI endDateText;
    public string difficultyText;

    public Button trivialButton;
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;
    private Button selectedButton;

    [System.Serializable]
    public class ToDo
    {
        public string title;
        public string description;
        public string endDate;
        public string difficulty;
    }

    private List<ToDo> todoList = new List<ToDo>(); // Declare the list to store ToDo items

    void Start()
    {
        // Load existing ToDo list or create a new one
        LoadOrCreateToDoList();

        PrintLoadedToDoList();
    }

    private void LoadOrCreateToDoList()
    {
        string filePath = GetToDoListFilePath();

        if (File.Exists(filePath))
        {
            // If the file exists, read the binary data from the file
            Debug.Log("Loaded existing ToDo list");
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fileStream = File.Open(filePath, FileMode.Open))
                {
                    todoList = (List<ToDo>)formatter.Deserialize(fileStream);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error loading ToDo list: {e.Message}");
            }
        }
        else
        {
            Debug.Log("Created new ToDo list");
            todoList = new List<ToDo>();
            SaveToDoList();
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

    public void SetToDoData()
    {
        Debug.Log("SetToDoData");

        ToDo newTodo = new ToDo
        {
            title = titleText.text,
            description = descriptionText.text,
            endDate = endDateText.text,
            difficulty = difficultyText
        };

        // Add the new ToDo to the list
        todoList.Add(newTodo);

        // Save the updated list to a file using binary serialization
        SaveToDoList();

        PrintSavedToDoList();

        // Load the next scene
        LoadNextScene();
    }

    private void PrintSavedToDoList()
    {
        Debug.Log("Saved ToDo List:");

        foreach (ToDo todo in todoList)
        {
            Debug.Log($"Title: {todo.title}, Description: {todo.description}, End Date: {todo.endDate}, Difficulty: {todo.difficulty}");
        }
    }

    private void PrintLoadedToDoList()
    {
        Debug.Log("Loaded ToDo List:");

        foreach (ToDo todo in todoList)
        {
            Debug.Log($"Title: {todo.title}, Description: {todo.description}, End Date: {todo.endDate}, Difficulty: {todo.difficulty}");
        }
    }

    private void SaveToDoList()
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string filePath = GetToDoListFilePath();

            // Write the binary data to the file
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

    private void LoadNextScene()
    {
        // Load the next scene (you can customize the scene name)
        SceneManager.LoadScene("TaskMenuTodos");
    }

    private string GetToDoListFilePath()
    {
        // Choose a path for your file (you can customize the path)
        string fileName = "ToDoList.dat";
        string path = Path.Combine(Application.persistentDataPath, fileName);
        return path;
    }
}
