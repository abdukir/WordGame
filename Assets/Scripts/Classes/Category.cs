using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [System.Serializable]
public class Category
{
    /// <summary>
    /// This is our category class. This'll hold information about
    /// our categories. They'll also store all the questions they have
    /// We have different category types, we can create different levels
    /// with them. 
    /// </summary>
    
    public string categoryName;                                                     // Name of our category
    public Sprite categoryLogo;                                                     // Logo of the category, we'll use these in category selection screen.

    public CategoryType categoryType;                                               // Type of our category. We can have different types. 

    public List<Question> questions;                                                // To store all the questions this category will have.

    public Category(string categoryName, Sprite categoryLogo, CategoryType categoryType, List<Question> questions)
    {
        this.categoryName = categoryName ?? throw new ArgumentNullException(nameof(categoryName));
        this.categoryLogo = categoryLogo ?? throw new ArgumentNullException(nameof(categoryLogo));
        this.categoryType = categoryType;
        this.questions = questions ?? throw new ArgumentNullException(nameof(questions));
    }
}

public enum CategoryType
{
    Image,
    Text,
    Sound,
    Mix
}
