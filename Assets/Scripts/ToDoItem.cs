// ToDoItem.cs
using UnityEngine;
using UnityEngine.UI;

public class ToDoItem : MonoBehaviour
{
    public Text titleText;
    public Text descriptionText;
    public Text endDateText;
    public Text difficultyText;

    private ToDo todo;

    public void SetToDoData(ToDo todoData)
    {
        todo = todoData;
        titleText.text = todo.title;
        descriptionText.text = todo.description;
        endDateText.text = todo.endDate.ToString("MM/dd/yyyy");
        difficultyText.text = todo.difficulty.ToString();
    }

    public void RemoveToDo()
    {
        ToDoManager.Instance.RemoveToDo(todo);
    }
}
