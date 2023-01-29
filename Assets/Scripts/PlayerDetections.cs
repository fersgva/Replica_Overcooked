using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetections : MonoBehaviour
{

    [HideInInspector]public GameObject closestPickable, closestTable;
    [HideInInspector] public List<GameObject> closePickables = new List<GameObject>();
    List<GameObject> closeTables = new List<GameObject>();

    [HideInInspector] public int interactuableMask, highlightedMask;
    
    // Start is called before the first frame update
    private void Awake()
    {
        interactuableMask = LayerMask.NameToLayer("Interactuable");
        highlightedMask = LayerMask.NameToLayer("HighLighted");
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region trigger detection
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Table") || other.gameObject.CompareTag("Crate"))
        {
            GameObject thisCloseTable = other.gameObject; 
            if(!closeTables.Contains(thisCloseTable))
                closeTables.Add(thisCloseTable);

            UpdateClosestTable(closestTable);
        }
        if (other.gameObject.CompareTag("Ingredient") || other.gameObject.CompareTag("Pan")
        ||   other.gameObject.CompareTag("Plate"))
        {
            GameObject thisClosePickUp = other.gameObject;
            if (!closePickables.Contains(thisClosePickUp))
                closePickables.Add(thisClosePickUp);

            UpdateClosestPickable(closestPickable);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Table") || other.gameObject.CompareTag("Crate"))
        {
            GameObject thisCloseTable = other.gameObject;
            closeTables.Remove(thisCloseTable);

            UpdateClosestTable(closestTable);
        }
        if (other.gameObject.CompareTag("Ingredient") || other.gameObject.CompareTag("Pan")
        || other.gameObject.CompareTag("Plate"))
        {
            GameObject thisClosePickUp = other.gameObject;
            closePickables.Remove(thisClosePickUp);

            UpdateClosestPickable(closestPickable);
        }
    }
    #endregion


    #region closest tables and pickables 
    public void UpdateClosestTable(GameObject currentClosest)
    {
        //La anterior mesa que estaba más cercana.
        if (currentClosest != null)
        {
            Utilities.ChangeAllGameObjectLayers(currentClosest, interactuableMask);
        }

        //La nueva mesa más cercana.
        closestTable = CheckClosestItem(closeTables);
        if (closestTable != null)
        {
            Utilities.ChangeAllGameObjectLayers(closestTable, highlightedMask);
        }
           
    }


    public void UpdateClosestPickable(GameObject currentClosest)
    {
        //El anterior pick up que estaba más cercana.
        if (currentClosest != null)
        {
            Utilities.ChangeAllGameObjectLayers(closestPickable, interactuableMask);
        }

        //Para el nuevo pick Up más cercano.
        closestPickable = CheckClosestItem(closePickables);
        if (closestPickable != null)
        {
            Utilities.ChangeAllGameObjectLayers(closestPickable, highlightedMask);
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
