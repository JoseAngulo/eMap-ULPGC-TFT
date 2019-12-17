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

        Vector3 eulers = cameraPivot.transform.eulerAngles;

        // Change pivot rotation


        Debug.Log("ÁNGULOS DEL PIVOT ----> X: " + cameraPivot.transform.rotation.x + " Y: " + cameraPivot.transform.rotation.y + " Z: " + cameraPivot.transform.rotation.z);
        Debug.Log("ÁNGULOS DEL PIVOT EN EULER ----> X: " + eulers.x + " Y: " + eulers.y + " Z: " + eulers.z);

        Debug.Log("ÁNGULOS PARA ROTAR ----> X: " + pivotRotation.x + " Y: " + pivotRotation.y + " Z: " + pivotRotation.z);
        Quaternion qt = Quaternion.Euler(pivotRotation);
        Debug.Log("ÁNGULOS PARA ROTAR EN EULER ----> X: " + qt.eulerAngles.x + " Y: " + qt.eulerAngles.y + " Z: " + qt.eulerAngles.z);


        cameraPivot.transform.eulerAngles = pivotRotation;

        Debug.Log(" === PIVOT TRANSFORMADO ===");
        Debug.Log("ÁNGULOS DEL PIVOT ----> X: " + cameraPivot.transform.rotation.x + " Y: " + cameraPivot.transform.rotation.y + " Z: " + cameraPivot.transform.rotation.z);
        Debug.Log("ÁNGULOS DEL PIVOT EN EULER ----> X: " + cameraPivot.transform.eulerAngles.x + " Y: " + cameraPivot.transform.eulerAngles.y + " Z: " + cameraPivot.transform.eulerAngles.z);

        //mainCamera.transform.parent = cameraPivot;

        //mainCamera.transform.position = new Vector3(0,0,-5f);



    }



}
