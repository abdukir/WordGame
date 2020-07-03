using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterManager : MonoBehaviour
{
    /// <summary>
    /// This script will manage all the chars. So we won't
    /// need to deal with all the chars while trying to do
    /// something with them. It'll also check if the users
    /// input is correct or not.
    /// </summary>
    public static LetterManager Instance { set; get; }
    private void Awake()
    {
        Instance = this;
    }

    public LetterHolder[] letterHolders;                                                        // This is array of the all letter holders in the scene.
    public Dictionary<LetterHolder,bool> currentLetterHolders;                                  // This is the list of available letterholder that active in scene.
    public Queue<LetterHolder> usableHolders;                                                   // This is the list of unocupied letterholders.

    private void Start()
    {
        UpdateHolders();
    }


    /// <summary>
    /// This will update letterholders and store them.
    /// Also it'll find which one of the holders are usable.
    /// </summary>
    public void UpdateHolders()
    {
        currentLetterHolders = new Dictionary<LetterHolder, bool>();
        usableHolders = new Queue<LetterHolder>();
        foreach (LetterHolder holder in letterHolders)
        {
            if (holder.gameObject.activeInHierarchy)
            {
                currentLetterHolders.Add(holder, holder.isOccupied);
                if (!holder.isOccupied)
                {
                    usableHolders.Enqueue(holder);
                }
            }
        }
    }

    /// <summary>
    /// This is called when we want to add a letter to screen without
    /// users selection. This will put the given letter to first empty
    /// slot on the answer section
    /// </summary>
    /// <param name="letter"></param>
    public void AddLetter(Letter letter)
    {
        if (usableHolders.Count > 0)
        {
            LetterHolder desiredHolder = usableHolders.Dequeue();
            desiredHolder.SetCurrentLetter(letter.gameObject);
            desiredHolder.curLetterObject = letter.gameObject;
            desiredHolder.isOccupied = true;
            letter.isPlaced = true;
        }
        else
        {
            Debug.LogWarning("List is full!");
        }
    }

    public void CheckAnswer()
    {

    }
}
