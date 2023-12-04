using UnityEngine;
using UnityEngine.UI;

public class ToDoListItem : MonoBehaviour
{
    public Text titleText;
    public Text descriptionText;
    public Text difficultyText;

    public void SetToDoItemData(string title, string description, string difficulty)
    {
        titleText.text = title;
        descriptionText.text = description;
        difficultyText.text = difficulty;
    }
}

