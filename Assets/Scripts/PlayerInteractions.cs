using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    GameObject holdItem;
    Animator anim;
    [SerializeField] Transform holdPoint;

    PlayerDetections detectScr;
    Utilities utilities;
    private void Awake()
    {
        utilities = Utilities.instance;
        anim = GetComponent<Animator>();
        detectScr = GetComponent<PlayerDetections>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Interact()
    {
        //--------------------Si no tenemos nada en mano-------------------------------------------//
        if (holdItem == null)
        {
            if (detectScr.closestPickable != null) //y si hay un pick up cerca... (se le da prioridad frente a la caja)
            {
                holdItem = detectScr.closestPickable;
                CatchPickUp(); //Cogemos el pickup.
            }
            else if (detectScr.closestTable != null && detectScr.closestTable.CompareTag("Crate")) //ó si hay una caja cerca...
            {
                OpenCrate();
                CatchPickUp();
            }

        }

        //----------------Si tenemos algo en mano-----------------------------------------------------//
        else if (detectScr.closestTable != null) //Y hay mesa delante...
        {
            GameObject closestTable = detectScr.closestTable;
            if (closestTable.transform.childCount == 0) // Si la mesa está libre...
            {
                Debug.Log("Mesa libre!");
                ReleasePickUp(closestTable.transform, true); //Lo dejamos en la mesa.
            }

            else
            {
                GameObject ingredientOnTable = closestTable.transform.GetChild(0).gameObject;

                //Si no, comprobar si los ingredientes se pueden mezclar...
                Ingredient holdIngredient = holdItem.GetComponent<Ingredient>();
                Ingredient ingredientToMix = ingredientOnTable.GetComponent<Ingredient>();

                //Si se pueden mezclar...
                Ingredient newIngredient = CraftingSystem.system.GetRecipeResult(holdIngredient, ingredientToMix);

                if (newIngredient != null)
                {
                    //Los quito de la lista.
                    detectScr.closePickables.Remove(holdItem);
                    detectScr.closePickables.Remove(ingredientOnTable);
                    Destroy(holdItem);
                    Destroy(ingredientOnTable);
                    holdItem = Instantiate(newIngredient.gameObject, closestTable.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    ReleasePickUp(closestTable.transform, true);
                }
            }
        }
        else //Si no hay mesa delante.
        {
            ReleasePickUp(null, false);
        }
    }

    private void ReleasePickUp(Transform parent, bool asKinematic)
    {
        anim.SetBool("holding", false);
        holdItem.GetComponent<Collider>().enabled = true;
        holdItem.GetComponent<Rigidbody>().isKinematic = asKinematic;
        holdItem.transform.SetParent(parent);
        if(parent != null)
        {
            holdItem.transform.localPosition = new Vector3(0f, 0.5f, 0f);
            holdItem.transform.localEulerAngles = Vector3.zero;
        }
        holdItem = null;
    }

    private void OpenCrate()
    {
        GameObject crate = detectScr.closestTable;
        Animator animCrate = crate.GetComponent<Animator>();
        IngredientCrate crateScript = crate.GetComponent<IngredientCrate>();
        animCrate.SetTrigger("open");
        holdItem = Instantiate(crateScript.ingredientToSpawn.gameObject, crate.transform.position, Quaternion.identity);
    }

    void CatchPickUp()
    {
        //Animations
        anim.SetBool("holding", true);

        
        //Le sacamos de la lista para que no lo tome entre los pickables cercanos.
        detectScr.closePickables.Remove(holdItem);
        
        //Change item properties.
        utilities.ChangeAllGameObjectLayers(holdItem, detectScr.interactuableMask);
        holdItem.GetComponent<Rigidbody>().isKinematic = true;
        holdItem.GetComponent<Collider>().enabled = false;
        holdItem.transform.SetParent(holdPoint);
        holdItem.transform.localPosition = new Vector3(0, 0, 0.2f);
        holdItem.transform.localEulerAngles = new Vector3(60, 100, 90);
    }
}
