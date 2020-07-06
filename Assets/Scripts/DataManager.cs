using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
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

    private void Start()
    {
        letterManager.SetQuestion(questions[currentQuestion]);
    }

    public void NextQuestion()
    {
        letterManager.ResetScreen();
        currentQuestion++;
        letterManager.SetQuestion(questions[currentQuestion]);
    }
}
