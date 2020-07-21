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

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { set; get; }
    private void Awake()
    {
        Instance = this;
    }
    private LetterManager letterManager => LetterManager.Instance;
    public Question[] questions;
    public int currentQuestion;

    public TextMeshProUGUI levelText;

    private void Start()
    {
        letterManager.SetQuestion(questions[currentQuestion]);
        levelText.text = "Level: " + (currentQuestion + 1);
    }

    public void NextQuestion()
    {
        letterManager.ResetScreen();
        currentQuestion++;
        letterManager.SetQuestion(questions[currentQuestion]);
        levelText.text = "Level: " + (currentQuestion + 1);
    }
}
