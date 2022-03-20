using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    GameObject currentTarget, lastTarget, holdItem;
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
        if(Input.GetKeyDown(KeyCode.E))
        {
            //--------------------Si no tenemos nada en mano-------------------------------------//
            if (holdItem == null) 
            {
                if(detectScr.closestPickable != null)
                {
                    holdItem = detectScr.closestPickable;
                    CatchPickUp();
                }

            }

            //----------------Si tenemos algo en mano-----------------------------------------------------//
            else
            {
                //if (currentTarget != null && currentTarget.transform.childCount == 0) //Si tenemos algo en mano y hay mesa DISPONIBLE.
                //{
                //    anim.SetBool("holding", false);
                //    holdItem.GetComponent<Collider>().enabled = true;
                //    holdItem.GetComponent<Rigidbody>().isKinematic = true;
                //    holdItem.transform.SetParent(currentTarget.transform);
                //    holdItem.transform.localPosition = new Vector3(0f, 0.5f, 0f);
                //    holdItem.transform.localEulerAngles = new Vector3(-90, 0, 0);
                //    holdItem.layer = interactuableMask;
                //    holdItem = null;
                //}
                //else if (currentTarget == null) //Si no hay mesa delante.
                //{
                //    anim.SetBool("holding", false);
                //    holdItem.transform.SetParent(null);
                //    holdItem.GetComponent<Collider>().enabled = true;
                //    holdItem.GetComponent<Rigidbody>().isKinematic = false;
                //    holdItem.layer = interactuableMask;
                //    holdItem = null;
                //}

            }
        }
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
