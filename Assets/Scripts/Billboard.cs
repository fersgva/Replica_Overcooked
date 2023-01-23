using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera ortographicCam;
    Camera mainCamera;
    Transform parent;
    Vector3 pixelsOffset = new Vector3(0, 0.1f, 0);
    private void Awake()
    {
        mainCamera = Camera.main;
        ortographicCam = GameObject.FindWithTag("OrtographicCamera").GetComponent<Camera>();
        parent = transform.parent;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Calcular posición en cámara (perspectiva) del padre.
        Vector3 parentScreenPos = mainCamera.WorldToViewportPoint(parent.position); //px
        Vector3 billboardScreenPos = parentScreenPos + pixelsOffset; //px

        //Dicho punto, considerándolo en ortográfico, lo pasamos a una posición del World.
        transform.position = ortographicCam.ViewportToWorldPoint(billboardScreenPos);

        //Miramos a la cámara ortográfica.
        transform.forward = ortographicCam.transform.forward;
    }
}
