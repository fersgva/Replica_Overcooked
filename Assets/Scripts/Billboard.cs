using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] Camera ortographicCam;
    [SerializeField] Camera mainCamera;
    Quaternion initRotation;
    Transform parent;
    Vector3 pixelsOffset = new Vector3(0, 0.1f, 0);
    private void Awake()
    {
        parent = transform.parent;
    }
    // Start is called before the first frame update
    void Start()
    {
        initRotation = transform.rotation;
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
