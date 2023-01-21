using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientCrate : MonoBehaviour, IInteractable
{
    public Ingredient ingredientToSpawn;
    //PlayerDetections
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void Interact(GameObject interacter)
    {
        anim.SetTrigger("open");
        interacter.GetComponent<PlayerInteractions>().holdItem = Instantiate(ingredientToSpawn.gameObject, transform.position, Quaternion.identity);
        interacter.GetComponent<PlayerInteractions>().CatchPickUp();
    }
}
