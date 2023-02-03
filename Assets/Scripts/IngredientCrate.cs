using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientCrate : Table, IInteractable
{
    [SerializeField] Ingredient ingredientToSpawn;

    public override void Interact(PlayerInteractions interacter, GameObject otherObject)
    {
        if(otherObject == null) interacter.OpenCrate(gameObject, ingredientToSpawn); 
    }

}
