using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSystem : MonoBehaviour
{
    private int level;
    private int experience;
    private int[] levelThresholds = {0, 500, 1000, 1500, 2000, 2500, 3000, 3500, 4000, 4500, 5000};
    public int Level { get { return level; } }
    public int Experience { get { return experience; } }
    public TextMeshProUGUI levelText, levelExperience;
    public Slider experienceBar;
    public List<Sprite> levelSprites;
    public Image levelImage;
    private void Start()
    {
        LoadStats();
        UpdateText();
        if (level < levelThresholds.Length - 1)
            experienceBar.value = (float)(experience - levelThresholds[level]) / (float)(levelThresholds[level + 1] - levelThresholds[level]);
        else
            experienceBar.value = 1f;
    }
    public void AddExperience(int exp)
    {
        experience += exp;
        while (level < levelThresholds.Length - 1 && experience >= levelThresholds[level + 1])
        {
            level++;
            experienceBar.value = 0;
        }
        UpdateStats();
        UpdateText();
    }
    public void UpdateStats()
    {
        PlayerPrefs.SetInt("Experience", experience);
        PlayerPrefs.SetInt("Level", level);
        LoadStats();
    }
    public void LoadStats()
    {
        level = PlayerPrefs.GetInt("Level");
        experience = PlayerPrefs.GetInt("Experience");
        while (level < levelThresholds.Length - 1 && experience >= levelThresholds[level + 1])
        {
            level++;
        }
    }
    public void UpdateText()
    {
        levelText.text = "Level: " + level;
        if (level < levelThresholds.Length - 1)
        {
            levelExperience.text = experience + " / " + levelThresholds[level + 1].ToString();
            experienceBar.value = (float)(experience - levelThresholds[level]) / (float)(levelThresholds[level + 1] - levelThresholds[level]);
        }
        else
        {
            levelExperience.text = "MAX";
            experienceBar.value = 1f;
        }
        if (level < levelSprites.Count)
            levelImage.sprite = levelSprites[level];
    }
}