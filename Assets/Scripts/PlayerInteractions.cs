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
            }

        }

        //----------------Si tenemos algo en mano-----------------------------------------------------//
        else if (detectScr.closestTable != null) //Y hay mesa delante...
        {
            GameObject closestTable = detectScr.closestTable;
            if (closestTable.transform.childCount == 0) // Si la mesa está libre...
            {
                ReleasePickUp(closestTable.transform, true, true, 0.5f); //Lo dejamos en la mesa.
            }

            //Si no está libre, mira a ver si es otro ingrediente...
            else if(closestTable.transform.GetChild(0).TryGetComponent(out Ingredient ingredientToMix))
            {
                MixIngredient(closestTable, ingredientToMix);
            }
            //O mira a ver si es una mesa con cuchillo
            else if(closestTable.transform.CompareTag("KnifeTable") && closestTable.transform.childCount == 2) //Mesa con cuchillo libre
            {
                ReleaseOnKnifeTable(closestTable);
            }
            //O una mesa con plato...
            else if (closestTable.transform.GetChild(0).CompareTag("Plate"))
            {
                ReleaseOnPlate(closestTable);
            }
        }
        else //Si no hay mesa delante.
        {
            ReleasePickUp(null, false, true, 0);
        }
    }

    private void ReleaseOnKnifeTable(GameObject closestTable)
    {
        //Porque no tenemos por qué tener un ingrediente en mano, puede ser otra cosa.
        if(holdItem.TryGetComponent(out Ingredient holdIngredient))
        {
            //Si lo que tengo en mano está entre los items que se pueden cortar...
            if (holdIngredient.stackIngredients.Count == 1 &&
            holdIngredient.stackIngredients.Intersect(CraftingSystem.system.chopableIngredients).Any())
            {
                ReleasePickUp(closestTable.transform, true, true, 0.6f); //Lo dejamos en la mesa.
            }

        }

    }

    void ReleaseOnPlate(GameObject closestTable)
    {
        //Porque no tenemos por qué tener un ingrediente en mano, puede ser otra cosa.
        if (holdItem.TryGetComponent(out Ingredient holdIngredient))
        {
            //Si lo que tengo en mano está entre los items que se pueden cortar y que pueden estar sobre plato.
            if (holdIngredient.stackIngredients.Count == 1 &&
            holdIngredient.stackIngredients.Intersect(CraftingSystem.system.canBeOnPlateIngredients).Any())
            {
                ReleasePickUp(closestTable.transform.GetChild(0), true, false, 0.03f); //Lo dejamos en la mesa.
            }

        }
    }
    private void MixIngredient(GameObject closestTable, Ingredient ingredientToMix)
    {
        //Porque no tenemos por qué tener un ingrediente en mano, puede ser otra cosa.
        if (holdItem.TryGetComponent(out Ingredient holdIngredient))
        {

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
                ReleasePickUp(closestTable.transform, true, true, 0.5f);
            }
        }
    }

    private void ReleasePickUp(Transform parent, bool asKinematic, bool enableColl, float yOffset)
    {
        anim.SetBool("holding", false);
        holdItem.GetComponent<Collider>().enabled = enableColl;
        holdItem.GetComponent<Rigidbody>().isKinematic = asKinematic;
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
