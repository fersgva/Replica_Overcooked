using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        foreach (Recipe recipe in recipes) //Iteramos por cada receta...
        {
            //Cogemos el listado de ingredientes de esta receta...
            List<Ingredient.IngredientType> ingredientsForThisRecipe = recipe.ingredientsNeeded;

            //y por cada tipo....
            foreach (Ingredient.IngredientType ingredient in ingredientsForThisRecipe)
            {
                if(ingredientToMix.type == ingredient && !holdIngredient.stackIngredients.Contains(ingredientToMix.type))
                {
                    holdIngredient.stackIngredients.Add(ingredientToMix.type);

                    //Para verificar que todos los ingredientes hasta ahora en la lista coinciden con la receta.
                    if (holdIngredient.stackIngredients.All(recipe.ingredientsNeeded.Contains) && holdIngredient.stackIngredients.Count == recipe.ingredientsNeeded.Count)
                    {
                        Debug.Log("Encontrado!");
                        return recipe.result;
                    }
                }
            }
        }
        return null;
    }
}
