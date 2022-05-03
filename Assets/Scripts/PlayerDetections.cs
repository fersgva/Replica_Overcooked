using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetections : MonoBehaviour
{

    [HideInInspector]public GameObject closestPickable, closestTable;
    [HideInInspector] public List<GameObject> closePickables = new List<GameObject>();
    List<GameObject> closeTables = new List<GameObject>();

    [HideInInspector] public int interactuableMask, highlightedMask;
    
    PlayerMovement movementScr;
    Utilities utilities;
    // Start is called before the first frame update
    private void Awake()
    {
        utilities = Utilities.instance;
        interactuableMask = LayerMask.NameToLayer("Interactuable");
        highlightedMask = LayerMask.NameToLayer("HighLighted");
        movementScr = GetComponent<PlayerMovement>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(movementScr.h != 0 || movementScr.v != 0)
        {
            UpdateClosestTable(closestTable);
            UpdateClosestPickable(closestPickable);
        }
    }

    #region trigger detection
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Table"))
        {
            GameObject thisCloseTable = other.gameObject; 
            if(!closeTables.Contains(thisCloseTable))
                closeTables.Add(thisCloseTable);

            UpdateClosestTable(closestTable);
        }
        //Cajas de ingredientes. Funcionan también como mesa si tenemos algo en mano
        if(other.gameObject.CompareTag("Crate"))
        {
            GameObject thisCloseCrate = other.gameObject;
            if (!closeTables.Contains(thisCloseCrate))
                closeTables.Add(thisCloseCrate);

            UpdateClosestTable(closestTable);
        }
        if (other.gameObject.CompareTag("PickUp"))
        {
            GameObject thisClosePickUp = other.gameObject;
            if (!closePickables.Contains(thisClosePickUp))
                closePickables.Add(thisClosePickUp);

            UpdateClosestPickable(closestPickable);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Table"))
        {
            GameObject thisCloseTable = other.gameObject;
            closeTables.Remove(thisCloseTable);

            UpdateClosestTable(closestTable);
        }
        //Cajas de ingredientes. Funcionan también como mesa si tenemos algo en mano
        if (other.gameObject.CompareTag("Crate"))
        {
            GameObject thisCloseCrate = other.gameObject;
            closeTables.Remove(thisCloseCrate);

            UpdateClosestTable(closestTable);
        }
        if (other.gameObject.CompareTag("PickUp"))
        {
            GameObject thisClosePickUp = other.gameObject;
            closePickables.Remove(thisClosePickUp);

            UpdateClosestPickable(closestPickable);
        }
    }
    #endregion


    #region closest tables and pickables 
    void UpdateClosestTable(GameObject currentClosest)
    {
        //La anterior mesa que estaba más cercana.
        if (currentClosest != null)
        {
            utilities.ChangeAllGameObjectLayers(currentClosest, interactuableMask);
        }

        //La nueva mesa más cercana.
        closestTable = CheckClosestItem(closeTables);
        if (closestTable != null)
        {
            utilities.ChangeAllGameObjectLayers(closestTable, highlightedMask);
        }
           
    }


    void UpdateClosestPickable(GameObject currentClosest)
    {
        //El anterior pick up que estaba más cercana.
        if (currentClosest != null)
        {
            utilities.ChangeAllGameObjectLayers(closestPickable, interactuableMask);
        }

        //Para el nuevo pick Up más cercano.
        closestPickable = CheckClosestItem(closePickables);
        if (closestPickable != null)
        {
            utilities.ChangeAllGameObjectLayers(closestPickable, highlightedMask);
        }
    }
    GameObject CheckClosestItem(List<GameObject> objectsType)
    {
        float bet = -Mathf.Infinity;
        GameObject closestItem = null;
        foreach (GameObject item in objectsType)
        {
            Vector3 dirToItem = Vector3.Normalize(item.transform.position - transform.position);
            float dotProduct = Vector3.Dot(transform.forward, dirToItem);
            if (dotProduct > bet)
            {
                closestItem = item;
                bet = dotProduct;
            }
        }
        return closestItem;
    }
    #endregion


}
