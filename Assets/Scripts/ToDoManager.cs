// ToDoManager.cs
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToDoManager : MonoBehaviour
{
    public static ToDoManager Instance;

    public GameObject todoPrefab;
    public Transform todoListParent;

    private List<ToDo> todos = new List<ToDo>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddToDo(ToDo todo)
    {
        todos.Add(todo);
        UpdateToDoListUI();
    }

    public void RemoveToDo(ToDo todo)
    {
        todos.Remove(todo);
        UpdateToDoListUI();
    }

    private void UpdateToDoListUI()
    {
        // Clear existing ToDo items
        foreach (Transform child in todoListParent)
        {
            Destroy(child.gameObject);
        }

        // Instantiate ToDo items from the list
        foreach (ToDo todo in todos)
        {
            GameObject todoItem = Instantiate(todoPrefab, todoListParent);
            todoItem.GetComponent<ToDoItem>().SetToDoData(todo);
        }
    }
}
