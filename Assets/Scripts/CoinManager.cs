using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { set; get; }
    public TextMeshProUGUI coinText => GetComponent<TextMeshProUGUI>();
    public int currentCoin;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Load();
    }

    private void UpdateCoin()
    {
        coinText.text = currentCoin.ToString();
    }

    public void AddCoin(int value)
    {
        currentCoin += value;
        UpdateCoin();
        Save();
    }

    public void UseCoin(int value)
    {
        currentCoin -= value;
        UpdateCoin();
        Save();
    }

    public void Save()
    {
        PlayerPrefs.SetInt("coin", currentCoin);
    }

    public void Load()
    {
        currentCoin = PlayerPrefs.GetInt("coin");
        UpdateCoin();
    }
}
