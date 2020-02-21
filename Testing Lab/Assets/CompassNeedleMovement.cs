using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassNeedleMovement : MonoBehaviour
{
    //public Transform north;
    public Camera camera;
    private Vector3 targetDirection;
    public Vector3 northDirection;
       
    void Update()
    {

        // Discard Y component to work with 2 dimensions
        Vector3 cameraForward = camera.transform.forward;
        cameraForward.y = 0;
        /*
        Vector3 northDirection = north.position - camera.transform.position;
        northDirection.y = 0;*/

        // Get the angle between the direction the camera is facing and the direction of the North
        float angles = Vector3.SignedAngle(cameraForward, northDirection, Vector3.up);

        //float rotationZ = Mathf.Atan2(northDirection.y, northDirection.x) * Mathf.Rad2Deg;

        // Rotate needle around Z axis to point towars the North
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, -angles);
                
    }
}
