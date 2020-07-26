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
    
    public bool isDisabled;
    private void Awake()
    {
        Instance = this;
    }

}
