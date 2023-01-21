using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour, IInteractable
{
    public void Interact(GameObject interacter)
    {
        PlayerInteractions interactionsScript = interacter.GetComponent<PlayerInteractions>();


        if (transform.childCount == 0) // Si la mesa está libre...
        {
            interactionsScript.ReleasePickUp(transform, true, 0.5f); //Lo dejamos en la mesa.
        }

        else if (transform.GetChild(0).TryGetComponent(out Ingredient ingredientToMix))
        {
            interactionsScript.MixIngredient(gameObject, ingredientToMix);
        }
        //else if (transform.CompareTag("KnifeTable") && transform.childCount == 1) //Mesa con cuchillo libre
        //{
        //    ChopIngredient(gameObject);
        //}
        //else //Mesa con grifo.
        //{

        //}
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
