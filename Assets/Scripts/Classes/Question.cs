using System;
using UnityEngine;

[System.Serializable]
public class Question
{
    /// <summary>
    /// Here we construct our question object. This can be expanded.
    /// We should select an enum for every question we create, game
    /// screen'll be generated based on the question type. All the 
    /// values will be feed from json that we'll get from a server.
    /// </summary>
    
    public QuestionType type;

    public Sprite image;                                                        // To store our image for question. This can stay null if are using other question types
    public string text;                                                         // This is for the text type questions.
    public AudioClip audio;                                                     // This is for audio type questions. These can stay null if we are not using them.

    public string answer;                                                       // This is the answer for every kind of question.
    public string hint;                                                         // We can add hints for every question. Hints can be revealed with credits.
    public int value;                                                           // We can specify how much credit a question will give with this.

    public Question(QuestionType type, Sprite image, string text, AudioClip audio, string answer, string hint, int value)
    {
        this.type = type;
        this.image = image ?? throw new ArgumentNullException(nameof(image));
        this.text = text ?? throw new ArgumentNullException(nameof(text));
        this.audio = audio ?? throw new ArgumentNullException(nameof(audio));
        this.answer = answer ?? throw new ArgumentNullException(nameof(answer));
        this.hint = hint ?? throw new ArgumentNullException(nameof(hint));
        this.value = value;
    }
}

/// <summary>
/// We can also expand this too. 
/// </summary>
public enum QuestionType
{
    Image,
    Text,
    Sound
}