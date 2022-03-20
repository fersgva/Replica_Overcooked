using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour
{
    public static Utilities instance;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeAllGameObjectLayers(GameObject gO, int layerMask)
    {
        Transform[] allChildrenTransforms = gO.GetComponentsInChildren<Transform>();
        foreach (Transform thisTransform in allChildrenTransforms)
        {
            thisTransform.gameObject.layer = layerMask;
        }
    }
}
