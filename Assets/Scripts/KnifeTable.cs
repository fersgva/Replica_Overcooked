using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeTable : MonoBehaviour, IActionable
{
    [SerializeField] GameObject progressBar;
    public GameObject TriggerAction()
    {
        if(transform.childCount == 3) //Sólo si hay ingrediente encima.
        {
            progressBar.SetActive(true);
            //Camera.main.ScreenToWorldPoint()
            //progressBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.5f, 0));
            Debug.Log("Corto!");

        }
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
