using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Letter : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    /// <summary>
    /// This will manage all the letters user can place.
    /// It'll also handle dragging and dropping.
    /// </summary>
    private RectTransform rectTransform => GetComponent<RectTransform>();                           // Reference to our rect transform.
    private GameManager gM => GameManager.Instance;                                                 // Reference to GameManager
    private TextMeshProUGUI letterText => transform.GetChild(0).GetComponent<TextMeshProUGUI>();    // Reference to our letter text.
    private LetterManager letterManager => LetterManager.Instance;                                  // Reference to our LetterManager.

    [SerializeField] private Canvas canvas = null;                                                  // Reference to our current canvas.

    public string curLetter;                                                                        // Current value of our letter holder.
    public LetterHolder curHolder = null;                                                           // Object we are in it.    
    public LetterHolder oldHolder = null;                                                           // Previous LetterHolder
    public Vector2 startPos;                                                                        // Starting position 
    public Transform parentTransform;                                                               // Starting transform

    public bool isPlaced;                                                                           // To check if our letter is placed or not.

    private void Start()
    {
        startPos = rectTransform.anchoredPosition;                                                  // Store our starting position for later use.
        parentTransform = transform.parent;                                                         // Store our parent for later use.
        //letterText.text = curLetter;                                                                // Update our letter.
    }

    public void SetLetter(string _letter)
    {
        curLetter = _letter;
        letterText.text = curLetter;
    }

    /// <summary>
    /// Set the curHolder to null
    /// Also reenable the raycast target
    /// </summary>
    private void OnEnable()
    {
        // Make it clickable after we enable it.
        curHolder = null;
        GetComponent<Image>().raycastTarget = true;
    }

    #region MouseControl

    /// <summary>
    /// Set mouse dragging value
    /// Set it's parent to it's default parent
    /// Also send it back to it's starting position.
    /// Set the default char holders occupation to false.
    /// </summary>
    public void SendToOriginalPos()
    {
        gM.isMouseDragging = false;
        transform.SetParent(parentTransform, false);
        rectTransform.anchoredPosition = startPos;

        isPlaced = false;
    }

    /// <summary>
    /// Set it's parent to main canvas to make it look above everything
    /// Set its position to mouses
    /// Disable it's isRaycast value to stop it from blocking rays
    /// Set isMouseDragging to true to stop other letters scaling up
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(canvas.transform);
        rectTransform.position = eventData.position;
        GetComponent<Image>().raycastTarget = false;
        gM.isMouseDragging = true;
        if (curHolder != null)
        {
            curHolder.isOccupied = false;
        }
        curHolder = null;
    }

    /// <summary>
    /// Hold it's position to mouses
    /// Also make sure isDragging stays true.
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        gM.isMouseDragging = true;
    }

    // Re-enable the raycast target to true to make it redraggable again.
    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = true;
        if (curHolder == null)
        {
            SendToOriginalPos();
        }

    }

    // Scale it up when the mouse hovers over it.
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!gM.isMouseDragging)
        {
            transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        }
    }

    // Scale it down when mouse exits.
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }

    /// <summary>
    /// We'll be handling auto placing in here
    /// First we'll check if out letter is placed or not
    /// If it's placed we'll send it to it's original pos
    /// If not we need to send it to first empty place on
    /// The LetterHolders
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isPlaced)
        {
            curHolder.isOccupied = false;
            curHolder.isCorrect = false;
            SendToOriginalPos();
            oldHolder = null;
            curHolder = null;
            letterManager.UpdateHolders();
        }
        else
        {
            // Place it on the first empty slot
            letterManager.AddLetter(this);
        }
    }


    #endregion
}
