using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                interacter.ReleaseOnPlate(transform.GetChild(1).GetComponent<Ingredient>(), gameObject);
            }
        }

    }

    
}
