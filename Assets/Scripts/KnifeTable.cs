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
        if (transform.childCount < 3) return null;

        if (transform.GetChild(2).TryGetComponent(out Ingredient ingredientOnTable))
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
        interacter.ReleaseOnKnifeTable(otherObject.GetComponent<Ingredient>(), gameObject);
    }

    
}
