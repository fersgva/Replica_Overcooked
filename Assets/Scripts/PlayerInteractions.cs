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
        if(Input.GetKeyDown(KeyCode.E))
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
                    //Activar animación de caja.
                    Debug.Log("ABRIR");
                }

            }

            //----------------Si tenemos algo en mano-----------------------------------------------------//
            else if(detectScr.closestTable != null) //Si tenemos algo en mano y hay mesa delante.
            {
                anim.SetBool("holding", false);
                holdItem.GetComponent<Collider>().enabled = true;
                holdItem.GetComponent<Rigidbody>().isKinematic = true;
                holdItem.transform.SetParent(detectScr.closestTable.transform);
                holdItem.transform.localPosition = new Vector3(0f, 0.5f, 0f);
                holdItem.transform.localEulerAngles = Vector3.zero;
                holdItem = null;
            }
            else //Si no hay mesa delante.
            {
                anim.SetBool("holding", false);
                holdItem.transform.SetParent(null);
                holdItem.GetComponent<Collider>().enabled = true;
                holdItem.GetComponent<Rigidbody>().isKinematic = false;
                holdItem = null;
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
