using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public List<Recipe> recipes;

    public List<Ingredient.IngredientType> chopableIngredients;

    public List<Ingredient.IngredientType> canBeOnPlateIngredients;

    public List<Ingredient.IngredientType> canBeOnPanIngredients;

    public List<Ingredient.IngredientType> canBeOnSaucepanIngredients;


    public static CraftingSystem system;
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

            //Si en esta receta...
            if(!holdIngredient.stackIngredients.Intersect(ingredientToMix.stackIngredients).Any() //´No está repetido el que sostienes.
                && ingredientsForThisRecipe.Intersect(holdIngredient.stackIngredients).Any() //Y hay un ingrediente como el que sostienes
                && ingredientsForThisRecipe.Intersect(ingredientToMix.stackIngredients).Any()) //Y hay un ingrediente como el que vas a mezclar...
            {
                //Concateno ambas listas.
                holdIngredient.stackIngredients = holdIngredient.stackIngredients.Concat(ingredientToMix.stackIngredients).ToList();
                break; //No debería seguir iterando!!!.
            }
        }
        foreach (Recipe recipe in recipes)
        {
            //Para verificar que todos los ingredientes hasta ahora en la lista coinciden con la receta.
            if (holdIngredient.stackIngredients.All(recipe.ingredientsNeeded.Contains) && holdIngredient.stackIngredients.Count == recipe.ingredientsNeeded.Count)
            {
                Debug.Log("Se forma la recta: " + recipe.name);
                return recipe.result;
            }
        }
        return null;
    }

}
