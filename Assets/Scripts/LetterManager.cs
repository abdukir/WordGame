using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public struct LetterInfo
{
    public bool isOccupied;
    public bool isCorrect;

    public LetterInfo(bool isOccupied, bool isCorrect)
    {
        this.isOccupied = isOccupied;
        this.isCorrect = isCorrect;
    }
}

public class LetterManager : MonoBehaviour
{
    /// <summary>
    /// This script will manage all the chars. So we won't
    /// need to deal with all the chars while trying to do
    /// something with them. It'll also check if the users
    /// input is correct or not.
    /// </summary>

    private const string LETTERS = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZXQW";
    
    public static LetterManager Instance { set; get; }
    private void Awake()
    {
        Instance = this;
    }

    public GameObject testTik;

    public LetterHolder[] letterHolders;                                                        // This is array of the all letter holders in the scene.
    public Dictionary<LetterHolder,LetterInfo> currentLetterHolders;                            // This is the list of available letterholder that active in scene.
    public Queue<LetterHolder> usableHolders;                                                   // This is the list of unocupied letterholders.

    public Letter[] letters;                                                                    // Reference to our letters.

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
        currentLetterHolders = new Dictionary<LetterHolder, LetterInfo>();
        usableHolders = new Queue<LetterHolder>();
        foreach (LetterHolder holder in letterHolders)
        {
            if (holder.gameObject.activeInHierarchy)
            {
                currentLetterHolders.Add(holder, new LetterInfo(holder.isOccupied, holder.isCorrect) );
                if (!holder.isOccupied)
                {
                    usableHolders.Enqueue(holder);
                }
            }
        }
        // Check every holders correctivity value, if none of them are false, answer is correct!
        if (!currentLetterHolders.ContainsValue(new LetterInfo(false,false)))
        {
            // Correct answer!!
            Debug.Log("All Correct");
            testTik.SetActive(true);
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

    [ContextMenu("test question")]
    public void Test()
    {
        SetQuestion("DENEME");
    }

    public void SetQuestion(string answer)
    {
        // First enable enough letter holders to hold our answer
        for (int i = 0; i < answer.Length; i++)
        {
            letterHolders[i].gameObject.SetActive(true);
            UpdateHolders();
            letterHolders[i].desiredValue = answer[i].ToString();
            letters[i].SetLetter(answer[i].ToString());
        }

        // Set up enough letters and randomize rest.
        for (int i = answer.Length; i < letters.Length; i++)
        {
            int rand = Random.Range(0, LETTERS.Length);
            letters[i].SetLetter(LETTERS[rand].ToString());
        }
        MixLetters();
    }

    [ContextMenu("Test Mix")]
    public void TestMix()
    {
        MixLetters();
    }

    public void MixLetters()
    {
        List<Vector2> letterPositions = new List<Vector2>();
        for (int i = 0; i < letters.Length; i++)
        {
            letterPositions.Add(letters[i].transform.parent.GetComponent<RectTransform>().anchoredPosition);
        }
        for (int i = 0; i < letters.Length; i++)
        {
            int rand = Random.Range(0, letterPositions.Count);
            letters[i].transform.parent.GetComponent<RectTransform>().anchoredPosition = letterPositions[rand];
            letterPositions.RemoveAt(rand);
        }

    }
}
