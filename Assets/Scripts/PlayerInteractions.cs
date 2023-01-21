using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlayerInteractions : MonoBehaviour
{
    [HideInInspector] public GameObject holdItem;
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
            else if (detectScr.closestTable != null) //ó si hay una caja cerca...
            {
                detectScr.closestTable.GetComponent<IInteractable>().Interact(gameObject);
            }

        }

        //----------------Si tenemos algo en mano-----------------------------------------------------//
        else if (detectScr.closestTable != null) //Y hay mesa delante...
        {
            detectScr.closestTable.GetComponent<IInteractable>().Interact(gameObject);
        }
        else //Si no hay mesa delante.
        {
            ReleasePickUp(null, false, 0);
        }
    }

    private void ChopIngredient(GameObject closestTable)
    {
        Ingredient holdIngredient = holdItem.GetComponent<Ingredient>();

        //Si lo que tengo en mano está entre los items que se pueden cortar...
        if (holdIngredient.stackIngredients.Count == 1 &&
        holdIngredient.stackIngredients.Intersect(CraftingSystem.system.chopableIngredients).Any())
        {
            ReleasePickUp(closestTable.transform, true, 0.6f); //Lo dejamos en la mesa.
        }
    }

    public void MixIngredient(GameObject closestTable, Ingredient ingredientToMix)
    {
        Ingredient holdIngredient = holdItem.GetComponent<Ingredient>();

        //Intentar mezclar ingredientes
        Ingredient newIngredient = CraftingSystem.system.GetRecipeResult(holdIngredient, ingredientToMix);

        if (newIngredient != null)
        {
            //Los quito de la lista.
            detectScr.closePickables.Remove(holdItem);
            detectScr.closePickables.Remove(ingredientToMix.gameObject);
            Destroy(holdItem);
            Destroy(ingredientToMix.gameObject);
            holdItem = Instantiate(newIngredient.gameObject, closestTable.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            ReleasePickUp(closestTable.transform, true, 0.5f);
        }
    }

    public void ReleasePickUp(Transform parent, bool asKinematic, float yOffset)
    {
        anim.SetBool("holding", false);
        holdItem.GetComponent<Collider>().enabled = true;
        holdItem.GetComponent<Rigidbody>().isKinematic = asKinematic;
        holdItem.transform.SetParent(parent);
        if(parent != null)
        {
            holdItem.transform.localPosition = new Vector3(0f, yOffset, 0f);
            holdItem.transform.localEulerAngles = Vector3.zero;
        }
        holdItem = null;
    }

    //private void OpenCrate()
    //{
    //    GameObject crate = detectScr.closestTable;
    //    Animator animCrate = crate.GetComponent<Animator>();
    //    IngredientCrate crateScript = crate.GetComponent<IngredientCrate>();
    //    animCrate.SetTrigger("open");
    //    holdItem = Instantiate(crateScript.ingredientToSpawn.gameObject, crate.transform.position, Quaternion.identity);
    //    CatchPickUp();
    //}

    public void CatchPickUp()
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
