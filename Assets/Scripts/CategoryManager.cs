using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryManager : MonoBehaviour
{

    public GameObject gameScreen;

    // Call the category we want based on id

    public void CallCategory(int ID)
    {
        Debug.LogWarning("Category " + ID + " is called");
        // Reset everything

        GameObject gameScreenObject = Instantiate(gameScreen);
        gameObject.SetActive(false);
        gameScreen.transform.GetChild(0).GetComponent<DataManager>().FirstQuestion(ID);

    }
}
