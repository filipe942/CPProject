using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image barFill;
    [SerializeField] private Text levelText;

    private int currentLevel = 1;
    private float currentXP = 0f;
    private float maxXp = 50f; // Adjust this based on your leveling system

    private Coroutine fillRoutine;

    private void Start()
    {
        UpdateUI();
    }

    public void AddXP(float amount)
    {
        currentXP += amount;

        if (currentXP >= maxXp)
        {
            LevelUp();
        }
        else
        {
            UpdateUI();
        }
    }

    private void LevelUp()
    {
        currentXP = 0f;
        currentLevel++;
        maxXp *= currentLevel; // You can adjust this growth factor based on your leveling system

        UpdateUI();
    }

    private void UpdateUI()
    {
        float fillAmount = currentXP / maxXp;
        barFill.fillAmount = fillAmount;
        levelText.text = "Level " + currentLevel + "\nXP: " + currentXP + "/" + maxXp;
    }
}
