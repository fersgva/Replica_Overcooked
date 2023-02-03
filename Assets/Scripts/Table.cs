using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour, IInteractable
{
    public virtual void Interact(PlayerInteractions interacter, GameObject otherObject)
    {
        if (transform.childCount == 0) //MESA: VACÍA
        {
            interacter.ReleasePickUp(transform, true, true, 0.5f);
        }
        else if (otherObject.TryGetComponent(out Ingredient holdIngredient)) //MANO: INGREDIENTE
        {
            //MESA: INGREDIENTE
            if (transform.GetChild(0).TryGetComponent(out Ingredient ingredientToMix))
            {
                interacter.MixIngredient(gameObject, holdIngredient, ingredientToMix);
            }
            //MESA: PLATO
            else if (transform.GetChild(0).CompareTag("Plate"))
            {
                interacter.ReleaseOnPlate(holdIngredient, gameObject);
            }
            //MESA: SARTÉN
            else if (transform.GetChild(0).CompareTag("Pan"))
            {
                interacter.ReleaseOnPan(holdIngredient, gameObject);
            }
        }
        else if (otherObject.CompareTag("Plate")) //MANO: PLATO.
        {
            //MESA: INGREDIENTE
            if (transform.GetChild(0).TryGetComponent(out Ingredient ingredientOnTable))
            {
                interacter.ReleaseOnPlate(ingredientOnTable, gameObject);
            }
        }
    }

}
