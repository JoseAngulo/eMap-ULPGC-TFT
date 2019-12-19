using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassNeedleMovement : MonoBehaviour
{

    public Transform target;
    private Vector3 targetDirection;


    
    void Update()
    {
        targetDirection.z = target.eulerAngles.y;
        transform.localEulerAngles = targetDirection;
    }
}
