using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlayerInteractions : MonoBehaviour
{
    GameObject holdItem;
    Animator anim;
    [SerializeField] Transform holdPoint;

    PlayerDetections detectScr;
    private void Awake()
    {
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
        //MANO: NADA
        if (holdItem == null)
        {
            if (detectScr.closestPickable) //y si hay un pick up cerca... (se le da prioridad frente a la caja)
            {
                CatchPickUp(); //Cogemos el pickup.
            }
            else if (detectScr.closestTable && detectScr.closestTable.CompareTag("Crate")) //� si hay una caja cerca...
            {
                OpenCrate();
            }

        }

        //MANO: ALGO
        else if (detectScr.closestTable)
        {
            GameObject closestTable = detectScr.closestTable;

            closestTable.GetComponent<Table>().Interact(this, holdItem);
        }
        else //Si no hay mesa delante.
        {
            ReleasePickUp(null, false, true, 0);
        }
    }

    public void ReleaseOnKnifeTable(Ingredient holdIngredient, GameObject closestTable)
    {
        //Si lo que tengo en mano est� entre los items que se pueden cortar...
        if (holdIngredient.stackIngredients.Count == 1 &&
        holdIngredient.stackIngredients.Intersect(CraftingSystem.system.chopableIngredients).Any())
        {
            ReleasePickUp(closestTable.transform, true, true, 0.6f); //Lo dejamos en la mesa.
        }

    }

    public void ReleaseOnPlate(Ingredient ingredientToPutOnPlate, GameObject closestTable)
    {
        //Si lo que tengo en mano est� entre los items que se pueden cortar y que pueden estar sobre plato.
        if (ingredientToPutOnPlate.stackIngredients.Count == 1 &&
        ingredientToPutOnPlate.stackIngredients.Intersect(CraftingSystem.system.canBeOnPlateIngredients).Any())
        {
            ReleasePickUp(closestTable.transform.GetChild(0), true, false, 0.03f); //Lo dejamos en la mesa.
        }
    }

    public void ReleaseOnPan(Ingredient holdIngredient, GameObject closestTable)
    {
        //Si lo que tengo en mano est� entre los items que se pueden cortar...
        if (holdIngredient.stackIngredients.Count == 1 &&
        holdIngredient.stackIngredients.Intersect(CraftingSystem.system.canBeOnPanIngredients).Any())
        {
            Debug.Log("pUEDO!");
            ReleasePickUp(closestTable.transform.GetChild(0), true, false, 0.3f);
        }
    }
    public void MixIngredient(GameObject closestTable, Ingredient holdIngredient, Ingredient ingredientToMix)
    {
        //Intentar mezclar ingredientes
        Ingredient newIngredient = CraftingSystem.system.GetRecipeResult(holdIngredient, ingredientToMix);

        if (newIngredient)
        {
            //Los quito de la lista.
            detectScr.closePickables.Remove(holdItem);
            detectScr.closePickables.Remove(ingredientToMix.gameObject);
            Destroy(holdItem);
            Destroy(ingredientToMix.gameObject);
            holdItem = Instantiate(newIngredient.gameObject, closestTable.transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
            ReleasePickUp(closestTable.transform, true, true, 0.5f);
        }
    }

    public void ReleasePickUp(Transform parent, bool asKinematic, bool enableColl, float yOffset)
    {
        anim.SetBool("holding", false);
        holdItem.GetComponent<Collider>().enabled = enableColl;
        holdItem.GetComponent<Rigidbody>().isKinematic = asKinematic;
        //parent.SetParent(holdItem.transform);
        holdItem.transform.SetParent(parent);
        if(parent != null)
        {
            holdItem.transform.localPosition = new Vector3(0f, yOffset, 0f);
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
        CatchPickUp();
    }

    void CatchPickUp()
    {
        holdItem = detectScr.closestPickable;
        //Animations
        anim.SetBool("holding", true);

        
        //Le sacamos de la lista para que no lo tome entre los pickables cercanos.
        detectScr.closePickables.Remove(holdItem);
        
        //Change item properties.
        Utilities.ChangeAllGameObjectLayers(holdItem, detectScr.interactuableMask);
        holdItem.GetComponent<Rigidbody>().isKinematic = true;
        holdItem.GetComponent<Collider>().enabled = false;
        holdItem.transform.SetParent(holdPoint);
        holdItem.transform.localPosition = new Vector3(0, 0, 0.2f);
        holdItem.transform.localEulerAngles = new Vector3(60, 100, 90);
    }
}
