using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    Transform currentTable;
    [SerializeField] Transform holdPoint;
    Transform item;
    int highlightedMask, catchableMask, tableMask;

    List<GameObject> tablesAround = new List<GameObject>();
    List<GameObject> catchablesAround = new List<GameObject>();
    GameObject closestTable, closestCatchable;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        highlightedMask = LayerMask.NameToLayer("HighLighted");
        tableMask = LayerMask.NameToLayer("Table");
        catchableMask = LayerMask.NameToLayer("Catchable");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && closestCatchable != null)
        {
            if (holdPoint.childCount == 0)
            {
                anim.SetBool("holding", true);
                closestCatchable.layer = catchableMask;
                closestCatchable.GetComponent<Rigidbody>().isKinematic = true;
                closestCatchable.transform.SetParent(holdPoint);
                closestCatchable.transform.localPosition = new Vector3(0.028f, -0.362f, 0.032f);
                closestCatchable.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            else if(closestTable != null) //Dejar en mesa.
            {
                anim.SetBool("holding", false);
                closestCatchable.GetComponent<Rigidbody>().isKinematic = true;
                closestCatchable.transform.SetParent(closestTable.transform);
                closestCatchable.transform.localPosition = new Vector3(0, 0.5f, 0);
                closestCatchable.transform.localEulerAngles = new Vector3(-90, 0, 0);
                closestCatchable.layer = catchableMask;
            }
            else
            {
                anim.SetBool("holding", false);
                closestCatchable.transform.SetParent(null);
                closestCatchable.GetComponent<Rigidbody>().isKinematic = false;
            }

        }
        //if (movement.direction.sqrMagnitude != 0 && closestTable != null)
        //{
        //    CalculateClosestTable();
        //}

    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("sdsa");
        if(other.gameObject.layer == tableMask)
        {

            if (!tablesAround.Contains(other.gameObject))
                tablesAround.Add(other.gameObject); //Lista de mesas.
            
            closestTable = CheckClosestObject(tablesAround);
            if(closestTable != null)
                closestTable.layer = highlightedMask;
        }

        if (other.gameObject.layer == catchableMask)
        {

            if (!catchablesAround.Contains(other.gameObject))
                catchablesAround.Add(other.gameObject); //Lista de catchables.

            closestCatchable = CheckClosestObject(catchablesAround);
            if (closestCatchable != null)
                closestCatchable.layer = highlightedMask;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == highlightedMask)
        {
            tablesAround.Clear();
            catchablesAround.Clear();
            if(closestTable != null)
            {
                closestTable.layer = tableMask;
                closestTable = null;
            }
            if (closestCatchable != null)
            {
                closestCatchable.layer = catchableMask;
                closestCatchable = null;
            }
        }
    }

    GameObject CheckClosestObject(List<GameObject> objectsAround)
    {
        float bet = 0;
        GameObject closestObject = null;
        foreach (GameObject thisObject in objectsAround)
        {
            thisObject.layer = tableMask;
            Vector3 dirToTable = Vector3.Normalize(thisObject.transform.position - transform.position);
            float dotProduct = Vector3.Dot(transform.forward, dirToTable);
            if (dotProduct >= bet)
            {
                bet = dotProduct;
                closestObject = thisObject;
            }
        }
        return closestObject;

    }
}
