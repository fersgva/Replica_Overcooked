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
            foreach (Ingredient.IngredientType ingredientType in ingredientsForThisRecipe)
            {
                if(ingredientType == holdIngredient.type)
                {
                    ingredientsForThisRecipe.Remove(ingredientType);
                }
                if (ingredientType == ingredientToMix.type)
                {
                    ingredientsForThisRecipe.Remove(ingredientType);
                }
            }
            if(ingredientsForThisRecipe.Count == 0)
            {
                return recipe.result;
            }
        }
        return null;
    }
}
