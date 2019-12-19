using UnityEngine;

public class CameraUtilities : MonoBehaviour
{

    public  Camera mainCamera;
    public  LayerMask terrainLayer;
    public  float rayCastDistance;

    public void moveCameraToBuilding(Vector3 cameraPosition, Vector3 pivotPosition, Vector3 pivotRotation)
    {
        Transform cameraPivot = mainCamera.transform.parent;
        
        // Change camera position
        mainCamera.transform.localPosition = cameraPosition;

        // Change pivot position and rotation
        cameraPivot.transform.position = pivotPosition;

        cameraPivot.transform.eulerAngles = pivotRotation;

    }

}
