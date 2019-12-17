using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static CampusInfoScript;

public class SceneManagerScript : MonoBehaviour
{
    public CampusInfoScript campusInfo;
    public CameraUtilities cameraUtilities;
    public GameObject arrowActivator;
    public MapLayerManager mapLayerManager;
    /*
    private void Start()
    {
        mapLayerManager.setLayerTexture(campusInfo.loadedLayerName);
    }
    */

    public void cameraToBuildingPosition(int index)
    {
        if (index == 0 || index == 1)
        {
            // Get pivot and camera position asigned to selected building and their position and rotation
            BuildingPosition buildingInfo = campusInfo.getBuilding(index);

            //Transform buildingTransform = buildingInfo.building.transform;
            Transform pivotTransform = buildingInfo.camera.transform;
            Transform cameraTransform = pivotTransform.GetChild(0).transform;

            Vector3 pivotPosition = new Vector3(pivotTransform.localPosition.x, pivotTransform.localPosition.y, pivotTransform.localPosition.z);
            Vector3 pivotRotation = new Vector3(pivotTransform.localRotation.eulerAngles.x, pivotTransform.localRotation.eulerAngles.y, pivotTransform.localRotation.eulerAngles.z);
            Vector3 cameraPosition = new Vector3(cameraTransform.localPosition.x, cameraTransform.localPosition.y, cameraTransform.localPosition.z);

            // Move and rotate camera and it's pivot.
            cameraUtilities.moveCameraToBuilding(cameraPosition, pivotPosition, pivotRotation);

            // Activate selected building arrow
            arrowActivator.GetComponent<DoubleClick>().activateArrow(buildingInfo.building.GetComponent<BuildingProperties>());

        }


        if (index == 14 || index == 5 || index == 8)
        {

            Debug.Log("BUSCANDO EL EDIFICIO CON ÍNDICE: " + index);
            BuildingPosition buildingInfo = campusInfo.getBuilding(index);

            Transform buildingTransform = buildingInfo.building.transform;
            Transform cameraTransform = buildingInfo.camera.transform;

            Vector3 cameraPosition = new Vector3();
            Vector3 pivotPosition = new Vector3();
            Vector3 pivotRotation = new Vector3();

            if (index == 14)
            {
                cameraPosition = new Vector3(2.235174e-08f, 2.384186e-07f, -1.747732f);
                pivotPosition = new Vector3(-0.0655365f, 3.934838e-17f, -3.257901f);
                pivotRotation = new Vector3(48.6f, -10.8f, 0f);
            }

            if (index == 5)
            {
                cameraPosition = new Vector3(0f, 0f, -1.903279f);
                pivotPosition = new Vector3(-0.9377327f, 4.726122e-17f, -2.846585f);
                pivotRotation = new Vector3(44f, 54.8f, 0f);
            }

            if (index == 8)
            {
                cameraPosition = new Vector3(-1.639128e-07f, 2.980232e-08f, -1.534185f);
                pivotPosition = new Vector3(2.2387f, -5.679581e-17f, 2.412147f);
                pivotRotation = new Vector3(47.2f, -81.00001f, 0f);
            }

            // Move camera and pivot to building position
            cameraUtilities.moveCameraToBuilding(cameraPosition, pivotPosition, pivotRotation);

            // Activate building arrow animation
            arrowActivator.GetComponent<DoubleClick>().activateArrow(buildingInfo.building.GetComponent<BuildingProperties>());
            //cameraUtilities.moveCameraToPosition(cameraPosition);
            //cameraUtilities.rotateCameraToPoint();
        }

    }


    private void checkTafiraBuildings(int index)
    {
        
    }

    

    
}
