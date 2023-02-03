using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static float FromDirectionToAngle(Vector3 direction)
    {
        //actg( z /  x)
        return Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
    }

    public static float FromRelativeDirectionToAngle(Vector3 direction, float yRot)
    {
        return yRot + Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
    }

    public static Vector3 FromAngleToDirection(float angle)
    {
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }
    public static Vector3 FromRelativeAngleToDirection(float angle, float yRot)
    {
        return new Vector3(Mathf.Sin((yRot + angle) * Mathf.Deg2Rad), 0, Mathf.Cos((yRot + angle) * Mathf.Deg2Rad));
    }

    public static Vector3 FromWorldToScreen(Camera cam, Vector3 worldCoordinates)
    {
        return cam.WorldToScreenPoint(worldCoordinates);
    }

    public static Vector3 FromScreenToWorld(Camera cam, Vector3 screenCoordinates)
    {
        return cam.ScreenToWorldPoint(screenCoordinates);
    }

    public static bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return ((layerMask.value & (1 << obj.layer)) > 0);

    }
    public static void ChangeAllGameObjectLayers(GameObject gO, int layerMask)
    {
        Transform[] allChildrenTransforms = gO.GetComponentsInChildren<Transform>();
        foreach (Transform thisTransform in allChildrenTransforms)
        {
            if (thisTransform.gameObject.layer == LayerMask.NameToLayer("WorldUIElement"))
                continue;

            thisTransform.gameObject.layer = layerMask;
        }
    }

    public static void BillboardForPerspectiveCamera()
    {
        ////Calcular posición en cámara (perspectiva) del padre.
        //Vector3 parentScreenPos = mainCamera.WorldToViewportPoint(parent.position); //px
        //Vector3 billboardScreenPos = parentScreenPos + pixelsOffset; //px

        ////Dicho punto, considerándolo en ortográfico, lo pasamos a una posición del World.
        //transform.position = ortographicCam.ViewportToWorldPoint(billboardScreenPos);

        ////Miramos a la cámara ortográfica.
        //transform.forward = ortographicCam.transform.forward;
    }




}
