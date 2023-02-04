using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KnifeTable : Table, IInteractable, IActionable
{
    [SerializeField] GameObject progressBar;
    public IActionable target;

   
    //Slider slider;
    private void Awake()
    {
        //slider = progressBar.GetComponentInChildren<Slider>();
    }

    public Ingredient TriggerAction()
    {
        if (transform.childCount > 1 && transform.GetChild(1).TryGetComponent(out Ingredient ingredientOnTable))
        {
            return ingredientOnTable;
        }
        else
        {
            return null;
        }
    }
    public override void Interact(PlayerInteractions interacter, GameObject otherObject)
    {
        if (otherObject)
        {
            Debug.Log("LLego 1!");
            if(otherObject.TryGetComponent(out Ingredient ingredient))
            {
                Debug.Log("llgo 2");
                interacter.ReleaseOnKnifeTable(ingredient, gameObject);
            }

            else if(otherObject.CompareTag("Plate") && transform.childCount > 1)
            {
                Debug.Log("llego 3");
                Ingredient ingredientOnTable = transform.GetChild(1).gameObject.GetComponent<Ingredient>();

                //Sólo si el ingrediente que está sobre la mesa ya ha sido cortado y pertenece a la lista de ingredientes que pueden estar en plato.
                if(ingredientOnTable.stackIngredients.Intersect(CraftingSystem.system.canBeOnPlateIngredients).Any())
                {
                    //Es como que primero se deja el plato en mesa...
                    interacter.GetComponent<PlayerDetections>().closePickables.Remove(ingredientOnTable.gameObject);
                    interacter.ReleasePickUp(otherObject, gameObject.transform, true, true, 0.5f);

                    //Y después, ponemos al ingrediente como hijo del plato.
                    interacter.ReleaseOnPlate(ingredientOnTable, otherObject);

                }
            }
        }

    }

    
}
