using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour, IInteractable
{
    public virtual void Interact(PlayerInteractions interacter, GameObject otherObject)
    {
        if (!otherObject) return;

        if (transform.childCount == 0) //MESA: VACÍA
        {
            interacter.ReleasePickUp(otherObject, transform, true, true, 0.5f);
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
                interacter.ReleaseOnPlate(holdIngredient, transform.GetChild(0).gameObject);
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
            if (transform.GetChild(0).TryGetComponent(out Ingredient ingredient))
            {
                //Es como que primero se deja el plato en mesa...
                interacter.GetComponent<PlayerDetections>().closePickables.Remove(ingredient.gameObject);
                interacter.ReleasePickUp(otherObject, gameObject.transform, true, true, 0.5f);

                //Y después, ponemos al ingrediente como hijo del plato.
                interacter.ReleaseOnPlate(ingredient, otherObject);
            }
        }
    }

}
