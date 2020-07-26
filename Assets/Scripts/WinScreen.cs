using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinScreen : MonoBehaviour
{
    public static WinScreen Instance { set; get; }

    public Image mainImage;

    public TextMeshProUGUI answerText;
    public TextMeshProUGUI creditsText;

    private void Awake()
    {
        Instance = this;
    }

    public void SetWinScreen(Sprite image, string answer, string credits)
    {
        mainImage.sprite = image;

        answerText.text = answer;
        creditsText.text = credits;

        transform.GetChild(0).gameObject.SetActive(true);
    }
}
