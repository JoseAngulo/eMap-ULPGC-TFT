using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassMovement : MonoBehaviour
{

    public Vector3 northDirection;
    public Transform player;
    public Quaternion targetDirection;

    public RectTransform northLayer;
    public RectTransform targetLayer;

    public Transform targetPlace;

    
    void Update()
    {
        changeNorthDirection();
        changeTargetDirection();
    }

    private void changeTargetDirection()
    {
        northDirection.z = player.eulerAngles.y;
        northLayer.localEulerAngles = northDirection;
    }

    private void changeNorthDirection()
    {
        Vector3 direction = transform.position - targetPlace.position;
        targetDirection = Quaternion.LookRotation(direction);

        targetDirection.z = -targetDirection.y;
        targetDirection.x = 0;
        targetDirection.y = 0;

        targetLayer.localRotation = targetDirection * Quaternion.Euler(northDirection);

    }
}
