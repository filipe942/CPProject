using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
public class DynamicListHabit : MonoBehaviour
{
    public GameObject listItemPrefab;
    public Transform listItemHolder;
    public int numOfListItems;

    [SerializeField] Text HabitCount;

    private void Start()
    {
        LoadAllTodos();

        // Get the loaded Habit list
        List<HabitItem.Habit> habitList = GetLoadedHabitList();

        // Iterate over the length of the list
        for (int i = 0; i < habitList.Count; i++)
        {
            // Instantiate the listItemPrefab for each Habit item
            GameObject listItem = Instantiate(listItemPrefab, listItemHolder);

            // Get the HabitListItem component from the instantiated prefab
            HabitListItem listItemScript = listItem.GetComponent<HabitListItem>();

            // Set the data for the HabitListItem based on the Habit item in the list
            listItemScript.SetHabitItemData(habitList[i].title, habitList[i].description, habitList[i].type, habitList[i].difficulty);
        }

        HabitCount.text = habitList.Count.ToString();
    }

    void Update()
    {        
        List<HabitItem.Habit> habitList = GetLoadedHabitList();
        HabitCount.text = habitList.Count.ToString();
    }

    private void LoadAllTodos()
    {
        string filePath = GetHabitListFilePath();

        if (File.Exists(filePath))
        {
            try
            {
                // If the file exists, read the binary data from the file
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fileStream = File.Open(filePath, FileMode.Open))
                {
                    // Deserialize the binary data into the list
                    List<HabitItem.Habit> habitList = (List<HabitItem.Habit>)formatter.Deserialize(fileStream);

                    // Print the loaded habitList
                    PrintLoadedHabitList(habitList);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error loading Habit list: {e.Message}");
            }
        }
        else
        {
            print("No Habit items found.");
        }
    }

    private void PrintLoadedHabitList(List<HabitItem.Habit> habitList)
    {
        print("Loaded Habit List:");

        foreach (HabitItem.Habit habit in habitList)
        {
            print($"Title: {habit.title}, Description: {habit.description}, Type: {habit.type}, Difficulty: {habit.difficulty}");
        }
    }

    private string GetHabitListFilePath()
    {
        // Choose a path for your file (you can customize the path)
        string fileName = "HabitList.dat";
        string path = Path.Combine(Application.persistentDataPath, fileName);
        return path;
    }

    private List<HabitItem.Habit> GetLoadedHabitList()
    {
        string filePath = GetHabitListFilePath();

        if (File.Exists(filePath))
        {
            try
            {
                // If the file exists, read the binary data from the file
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fileStream = File.Open(filePath, FileMode.Open))
                {
                    // Deserialize the binary data into the list
                    List<HabitItem.Habit> habitList = (List<HabitItem.Habit>)formatter.Deserialize(fileStream);

                    // Return the loaded habitList
                    return habitList;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error loading Habit list: {e.Message}");
            }
        }
        else
        {
            print("No Habit items found.");
        }

        // If there was an error or no Habit items found, return an empty list
        return new List<HabitItem.Habit>();
    }
}
