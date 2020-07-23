using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Question
{
    public string question;
    public string answer;
    public Sprite sprite;
}
[System.Serializable]
public class Category
{
    public string categoryName;
    public Question[] questions;
}

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { set; get; }
    private void Awake()
    {
        Instance = this;
    }
    private LetterManager letterManager => LetterManager.Instance;

    public Category[] categories;

    public int currentCategoryID;
    public int currentQuestion;

    public TextMeshProUGUI levelText;

    public void FirstQuestion(int ID)
    {
        letterManager.SetQuestion(categories[ID].questions[currentQuestion]);
        currentCategoryID = ID;
        levelText.text = "Level: " + (ID + 1);
    }

    public void NextQuestion()
    {
        letterManager.ResetScreen();
        currentQuestion++;
        letterManager.SetQuestion(categories[currentCategoryID].questions[currentQuestion]);
        levelText.text = "Level: " + (currentQuestion + 1);
    }
}
