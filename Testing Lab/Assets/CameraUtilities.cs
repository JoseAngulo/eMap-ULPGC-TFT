using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraUtilities : MonoBehaviour
{

    public  Camera mainCamera;
    public  LayerMask terrainLayer;
    public  float rayCastDistance;


    public void moveCameraToPosition(Vector3 position)
    {
        Transform cameraPivot = mainCamera.transform.parent;

        //mainCamera.transform.parent = null;

        mainCamera.transform.position = position;
        //mainCamera.transform.position = position;

        RaycastHit hit;
        /*
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, rayCastDistance, terrainLayer))
        {
            Vector3 pivotSpawnPoint = hit.point;
            cameraPivot.transform.position = pivotSpawnPoint;
        }

        mainCamera.transform.parent = cameraPivot;*/

    }

    public void rotateCameraToPoint()
    {
        Transform cameraPivot = mainCamera.transform.parent;

        Vector3 temp = cameraPivot.rotation.eulerAngles;
        temp.x = 90.0f;
        cameraPivot.rotation = Quaternion.Euler(temp);

        //mainCamera.transform.parent = null;

        //Quaternion QT = Quaternion.Euler(point.x,point.y,0);

        //Debug.Log("ÁNGULOS point     X:" + point.x + " Y: " + point.y + " Z: " + point.z);

        //cameraPivot.transform.rotation = QT;
        Debug.Log("ÁNGULOS PIVOT DESPUÉS    X:" + cameraPivot.transform.rotation.x + " Y: " + cameraPivot.transform.rotation.y + " Z: " + cameraPivot.transform.rotation.z);
        /*mainCamera.transform.rotation = QT;

        RaycastHit hit;

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, rayCastDistance, terrainLayer))
        {
            Vector3 pivotSpawnPoint = hit.point;
            cameraPivot.transform.position = pivotSpawnPoint;
        }

        mainCamera.transform.parent = cameraPivot;*/
    }


    public void moveCameraToBuilding(Vector3 cameraPosition, Vector3 pivotPosition, Vector3 pivotRotation)
    {
        Transform cameraPivot = mainCamera.transform.parent;
        

        // Change camera position
        mainCamera.transform.localPosition = cameraPosition;

        // Change pivot position
        /*RaycastHit hit;

        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, rayCastDistance, terrainLayer))
        {
            Vector3 pivotSpawnPoint = hit.point;
            cameraPivot.transform.position = pivotSpawnPoint;
        }*/
        
        cameraPivot.transform.position = pivotPosition;

        // Change pivot rotation
        cameraPivot.transform.rotation = Quaternion.Euler(pivotRotation);

        //mainCamera.transform.parent = cameraPivot;

        //mainCamera.transform.position = new Vector3(0,0,-5f);

        

    }



}
