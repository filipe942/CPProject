using DungeonKIT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image barFill;
    [SerializeField] private Text levelText;
    [SerializeField] private Text pointsText;

    private int currentLevel = 1;
    private float currentXP = 0f;
    private float maxXp = 50f;
    private int playerPoints = 0;

    private Coroutine fillRoutine;

    private void Start()
    {
        UpdateFromPlayer();
    }

    public void UpdateFromPlayer()
    {
        // Assuming that Player.Instance is an instance of the Player class
        if (PlayerStats.Instance != null)
        {
            currentXP = PlayerStats.Instance.ExperiencePoints;
            currentLevel = PlayerStats.Instance.Level;
            playerPoints = PlayerStats.Instance.Points;
            maxXp = PlayerStats.Instance.Level * 50f;
            UpdateUI();
        }
    }


    private void UpdateUI()
    {
        float fillAmount = currentXP / maxXp;
        barFill.fillAmount = fillAmount;
        levelText.text = "Level " + currentLevel + "\nXP: " + currentXP + "/" + maxXp;
        pointsText.text = playerPoints.ToString();
    }
}
