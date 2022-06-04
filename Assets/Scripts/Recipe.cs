using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects/Recipe")]
public class Recipe : ScriptableObject
{
    public List<Ingredient.IngredientType> ingredientsNeeded;
    public Ingredient result;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
