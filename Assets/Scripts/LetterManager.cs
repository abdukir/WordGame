using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

[System.Serializable]
public struct LetterInfo
{
    public bool isOccupied;
    public bool isCorrect;
    
    public bool Check()
    {
        bool toReturn = false;
        if (isCorrect && isOccupied)
        {
            toReturn = true;
        }
        return toReturn;
    }

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
    private DataManager dataManager => DataManager.Instance;
    private GameManager gM => GameManager.Instance;
    public static LetterManager Instance { set; get; }
    private void Awake()
    {
        Instance = this;
    }

    public LetterHolder[] letterHolders;                                                        // This is array of the all letter holders in the scene.
    public Dictionary<LetterHolder,bool> currentLetterHolders;                                  // This is the list of available letterholder that active in scene.
    public Queue<LetterHolder> usableHolders;                                                   // This is the list of unocupied letterholders.

    public Letter[] letters;                                                                    // Reference to our letters.

    public TextMeshProUGUI questionText;
    public Image questionImage;
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
                currentLetterHolders.Add(holder, holder.isCorrect);
                if (!holder.isOccupied)
                {
                    usableHolders.Enqueue(holder);
                }
            }
        }
        // Check every holders correctivity value, if none of them are false, answer is correct!
        if (!currentLetterHolders.ContainsValue(false))
        {
            // Correct Answer
            Debug.LogWarning("All Correct!");
            CoinManager.Instance.AddCoin(10);
            dataManager.NextQuestion();
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
            desiredHolder.curLetterObject = letter.gameObject;
            desiredHolder.isOccupied = true;
            letter.isPlaced = true;
            desiredHolder.SetCurrentLetter(letter.gameObject);
        }
        else
        {
            Debug.LogWarning("List is full!");
        }
    }

    public void ResetScreen()
    {
        foreach (Letter letter in letters)
        {
            letter.SendToOriginalPos();
        }
        foreach (LetterHolder holder in letterHolders)
        {
            holder.ResetHolder();
            holder.gameObject.SetActive(false);
        }
    }

    public void SetQuestion(Question question)
    {
        // First enable enough letter holders to hold our answer
        for (int i = 0; i < question.answer.Length; i++)
        {
            letterHolders[i].gameObject.SetActive(true);
            letterHolders[i].desiredValue = question.answer[i].ToString();
            letters[i].SetLetter(question.answer[i].ToString());
        }
        UpdateHolders();

        // Set up enough letters and randomize rest.
        for (int i = question.answer.Length; i < letters.Length; i++)
        {
            int rand = Random.Range(0, LETTERS.Length);
            letters[i].SetLetter(LETTERS[rand].ToString());
        }
        questionImage.sprite = question.sprite;
        MixLetters();
        questionText.text = question.question;
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

    [ContextMenu("test hint")]
    public void GiveHint()
    {
        CoinManager.Instance.UseCoin(10);
        List<Letter> nonPlacedLetters = new List<Letter>();
        for (int i = 0; i < letters.Length; i++)
        {
            if (!letters[i].isPlaced)
            {
                nonPlacedLetters.Add(letters[i]);
            }
        }
        LetterHolder hintHolder = usableHolders.ToArray()[Random.Range(0, usableHolders.Count)];
        for (int i = 0; i < nonPlacedLetters.Count; i++)
        {
            if (nonPlacedLetters[i].curLetter == hintHolder.desiredValue)
            {
                hintHolder.SetCurrentLetter(nonPlacedLetters[i].gameObject);
                break;
            }
        }
        
        Debug.LogWarning("Hint!");

    }
}
