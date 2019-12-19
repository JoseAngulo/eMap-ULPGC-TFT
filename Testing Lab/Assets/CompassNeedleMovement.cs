using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassNeedleMovement : MonoBehaviour
{
    public Transform north;
    public Camera camera;
    private Vector3 targetDirection;

    public RectTransform compassCenter;
       
    void Update()
    {
        Vector3 camPos = camera.WorldToScreenPoint(camera.transform.position);
        Vector3 northPos = camera.WorldToScreenPoint(north.position);

        Vector3 northDirection = compassCenter.position - northPos;

        float rotationZ = Mathf.Atan2(northDirection.y, northDirection.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + 90f);
                
    }
}
