using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem system;
    public List<Recipe> recipes;
    private void Awake()
    {
        if (system == null)
            system = this;
        else
            Destroy(gameObject);
    }
    public Ingredient GetRecipeResult(Ingredient holdIngredient, Ingredient ingredientToMix)
    {
        foreach (Recipe recipe in recipes)
        {
            List<Ingredient.IngredientType> ingredientsForThisRecipe = recipe.ingredientsNeeded;
            Debug.Log(holdIngredient.type);
            Debug.Log(holdIngredient.type);
            if(ingredientsForThisRecipe.Contains(holdIngredient.type) && ingredientsForThisRecipe.Contains(ingredientToMix.type))
            {
                return recipe.result;
            }
        }
        return null;
    }
}
