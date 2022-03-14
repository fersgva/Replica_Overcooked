using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSimpleInteractions : MonoBehaviour
{
    [SerializeField] LayerMask isInteractuable;
    int highlightedMask, interactuableMask;
    GameObject currentTarget, lastTarget, holdItem;
    Animator anim;
    [SerializeField] Transform holdPoint;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        interactuableMask = LayerMask.NameToLayer("Interactuable");
        highlightedMask = LayerMask.NameToLayer("HighLighted");
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(holdItem == null) //Si no tenemos nada en mano...
            {
                if(currentTarget != null && currentTarget.CompareTag("PickUp")) //Si hay PickUp en suelo.
                {
                    holdItem = currentTarget;
                    CatchPickUp();
                }
                else if(currentTarget != null && currentTarget.transform.childCount > 0) //Si hay PickUp en mesa....
                {
                    holdItem = currentTarget.transform.GetChild(0).gameObject;
                    CatchPickUp();
                }

            }

            //----------------Si tenemos algo en mano-----------------------------------------------------//
            else
            {
                if(currentTarget != null && currentTarget.transform.childCount == 0) //Si tenemos algo en mano y hay mesa DISPONIBLE.
                {
                    anim.SetBool("holding", false);
                    holdItem.GetComponent<Collider>().enabled = true;
                    holdItem.GetComponent<Rigidbody>().isKinematic = true;
                    holdItem.transform.SetParent(currentTarget.transform);
                    holdItem.transform.localPosition = new Vector3(0f, 0.5f, 0f);
                    holdItem.transform.localEulerAngles = new Vector3(-90, 0, 0);
                    holdItem.layer = interactuableMask;
                    holdItem = null;
                }
                else if(currentTarget == null) //Si no hay mesa delante.
                {
                    anim.SetBool("holding", false);
                    holdItem.transform.SetParent(null);
                    holdItem.GetComponent<Collider>().enabled = true;
                    holdItem.GetComponent<Rigidbody>().isKinematic = false;
                    holdItem.layer = interactuableMask;
                    holdItem = null;
                }

            }
        }
    }
    private void FixedUpdate()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position + new Vector3(0, 0.05f, 0), transform.forward, out hit, 1, isInteractuable))
        {
            GameObject target = hit.collider.gameObject;
            lastTarget = currentTarget;
            if(lastTarget != null)
            {
                lastTarget.layer = interactuableMask;
            }
            currentTarget = target;
            currentTarget.layer = highlightedMask;
        }
        else if (currentTarget != null)
        {
            currentTarget.layer = interactuableMask;
            currentTarget = null;
            lastTarget = null;
        }
        
        Debug.DrawLine(transform.position + new Vector3(0, 0.1f, 0), transform.position + 1 * transform.forward, Color.red);
    }
    void CatchPickUp()
    {
        anim.SetBool("holding", true);
        holdItem.GetComponent<Rigidbody>().isKinematic = true;
        holdItem.GetComponent<Collider>().enabled = false;
        holdItem.transform.SetParent(holdPoint);
        holdItem.transform.localPosition = new Vector3(0.028f, -0.362f, 0.032f);
        holdItem.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
}
