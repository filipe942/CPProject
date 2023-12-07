using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
public class DynamicListFrequents : MonoBehaviour
{
    public GameObject listItemPrefab;
    public Transform listItemHolder;
    public int numOfListItems;

    [SerializeField] Text FrequentsCount;

    private void Start()
    {
        LoadAllTodos();

        // Get the loaded Frequents list
        List<FrequentsItem.Frequents> frequentsList = GetLoadedFrequentsList();

        // Iterate over the length of the list
        for (int i = 0; i < frequentsList.Count; i++)
        {
            // Instantiate the listItemPrefab for each Frequents item
            GameObject listItem = Instantiate(listItemPrefab, listItemHolder);

            // Get the FrequentsListItem component from the instantiated prefab
            FrequentsListItem listItemScript = listItem.GetComponent<FrequentsListItem>();

            // Set the data for the FrequentsListItem based on the Frequents item in the list
            listItemScript.SetFrequentsItemData(frequentsList[i].title, frequentsList[i].description, frequentsList[i].frequency, frequentsList[i].difficulty);
        }

        FrequentsCount.text = frequentsList.Count.ToString();
    }

    void Update()
    {        
        List<FrequentsItem.Frequents> frequentsList = GetLoadedFrequentsList();
        FrequentsCount.text = frequentsList.Count.ToString();
    }

    private void LoadAllTodos()
    {
        string filePath = GetFrequentsListFilePath();

        if (File.Exists(filePath))
        {
            try
            {
                // If the file exists, read the binary data from the file
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fileStream = File.Open(filePath, FileMode.Open))
                {
                    // Deserialize the binary data into the list
                    List<FrequentsItem.Frequents> frequentsList = (List<FrequentsItem.Frequents>)formatter.Deserialize(fileStream);

                    // Print the loaded frequentsList
                    PrintLoadedFrequentsList(frequentsList);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error loading Frequents list: {e.Message}");
            }
        }
        else
        {
            print("No Frequents items found.");
        }
    }

    private void PrintLoadedFrequentsList(List<FrequentsItem.Frequents> frequentsList)
    {
        print("Loaded Frequents List:");

        foreach (FrequentsItem.Frequents frequents in frequentsList)
        {
            print($"Title: {frequents.title}, Description: {frequents.description}, Frequency: {frequents.frequency}, Difficulty: {frequents.difficulty}");
        }
    }

    private string GetFrequentsListFilePath()
    {
        // Choose a path for your file (you can customize the path)
        string fileName = "FrequentsList.dat";
        string path = Path.Combine(Application.persistentDataPath, fileName);
        return path;
    }

    private List<FrequentsItem.Frequents> GetLoadedFrequentsList()
    {
        string filePath = GetFrequentsListFilePath();

        if (File.Exists(filePath))
        {
            try
            {
                // If the file exists, read the binary data from the file
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fileStream = File.Open(filePath, FileMode.Open))
                {
                    // Deserialize the binary data into the list
                    List<FrequentsItem.Frequents> frequentsList = (List<FrequentsItem.Frequents>)formatter.Deserialize(fileStream);

                    // Return the loaded frequentsList
                    return frequentsList;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error loading Frequents list: {e.Message}");
            }
        }
        else
        {
            print("No Frequents items found.");
        }

        // If there was an error or no Frequents items found, return an empty list
        return new List<FrequentsItem.Frequents>();
    }
}
