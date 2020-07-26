using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// This script will manage all the game screen elements.
    /// </summary>
    
    public static GameManager Instance { set; get; }

    public bool isMouseDragging;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Load();
    }

    public void Save()
    {

    }

    public void Load()
    {

    }

}
