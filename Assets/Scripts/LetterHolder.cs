using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class LetterHolder : MonoBehaviour, IDropHandler
{
    /// <summary>
    /// This script will hold all the information about our chars.
    /// It'll also handle changing of the chars.
    /// </summary>

    private RectTransform rectTransform => GetComponent<RectTransform>();                       // Reference to our rect transform.
    private LetterManager letterManager => LetterManager.Instance;                              // Reference to our LetterManager.
    private GameManager gM => GameManager.Instance;

    public bool isOccupied;                                                                     // Whether our char is occupied or not

    public string desiredValue;                                                                 // Desired value of the char. This will be set from LetterManager
    public string currentValue;                                                                 // Current value of the char.

    public bool isCorrect;                                                                      // Check if our answer is correct or not.

    public GameObject curLetterObject = null;                                                   // To store letter we'll be holding.
	#region Mouse Control

	public void OnDrop(PointerEventData eventData)
    {
        // If it is not occupied drop the letter, if not send it back to it's place
        if (!isOccupied)
        {
            // Save the current letter, set current letters LetterHolder, change parent and scale it accordingly 
            curLetterObject = eventData.pointerDrag;
            curLetterObject.GetComponent<Letter>().isPlaced = true;

            SetCurrentLetter(curLetterObject);
        }
        else
        {
            // Swap the letters, first check if we tried to place another letter placed somewhere else.
            if (eventData.pointerDrag.gameObject.GetComponent<Letter>().isPlaced)
            {
                Debug.Log("swap");
                GameObject newLetter = eventData.pointerDrag;
                LetterHolder newLetterHolder = newLetter.gameObject.GetComponent<Letter>().oldHolder;
                
                SetCurrentLetter(newLetter);
                newLetter.GetComponent<Letter>().oldHolder = this;

                newLetterHolder.SetCurrentLetter(curLetterObject);
                curLetterObject.GetComponent<Letter>().oldHolder = newLetterHolder;

                newLetterHolder.curLetterObject = curLetterObject;
                curLetterObject = newLetter;
            }
            else
            {
                // Send old letter to it's original position.
                RevertCurrentLetter();
                
                // Update the currentLetterObject
                curLetterObject = eventData.pointerDrag;

                curLetterObject.GetComponent<Letter>().isPlaced = true;
                SetCurrentLetter(curLetterObject);
            }
        }
        
    }

    /// <summary>
    /// Places current letter to it's position.
    /// </summary>
    public void SetCurrentLetter(GameObject obj)
    {
        obj.GetComponent<Letter>().curHolder = this;
        obj.GetComponent<Letter>().oldHolder = this;
        currentValue = obj.GetComponent<Letter>().curLetter;
        this.isOccupied = true;
        obj.transform.SetParent(transform, false);
        obj.GetComponent<RectTransform>().anchoredPosition = obj.GetComponent<Letter>().startPos;
        letterManager.UpdateHolders();
        
        if (currentValue == desiredValue)
        {
            isCorrect = true;
        }
        else
        {
            isCorrect = false;
        }
    }

    /// <summary>
    /// Sends current letter to it's original position.
    /// </summary>
    public void RevertCurrentLetter()
    {
        gM.isMouseDragging = false;
        curLetterObject.transform.SetParent(curLetterObject.GetComponent<Letter>().parentTransform);
        curLetterObject.GetComponent<RectTransform>().anchoredPosition = curLetterObject.GetComponent<Letter>().startPos;
        isOccupied = false;
        curLetterObject.GetComponent<Letter>().curHolder = null;
        curLetterObject.GetComponent<Letter>().isPlaced = false;
        curLetterObject = null;
        letterManager.UpdateHolders();
    }
	#endregion
}
