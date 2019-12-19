using UnityEngine;
using static CampusInfoScript;

public class SceneManagerScript : MonoBehaviour
{
    public CampusInfoScript campusInfo;
    public CameraUtilities cameraUtilities;
    public GameObject arrowActivator;
    public MapLayerManager mapLayerManager;
    private int lastBuildingIndex = 0;
    
    private void Start()
    {
        Debug.Log("LLAMANDO AL ACTUALIZADOR DE TEXTURAS DESDE SCENE_MANAGER");
        mapLayerManager.setLayerTexture(campusInfo.getLoadedLayer());
        Debug.Log("LA TEXTURA CARGADA DEBERÍA SER => " + campusInfo.getLoadedLayer());
    }
    

    public void cameraToBuildingPosition(int index)
    {
        // Deactivate previously selected building arrow
        BuildingPosition buildingInfo = campusInfo.getBuilding(lastBuildingIndex);

        deactivatePreviousBuildingArrow(buildingInfo);
       
        lastBuildingIndex = index;

        // Get pivot and camera position asigned to current selected building and their position and rotation
        buildingInfo = campusInfo.getBuilding(index);

        //Transform buildingTransform = buildingInfo.building.transform;
        Transform pivotTransform = buildingInfo.camera.transform;
        Transform cameraTransform = pivotTransform.GetChild(0).transform;

        Vector3 pivotPosition = new Vector3(pivotTransform.position.x, pivotTransform.position.y, pivotTransform.position.z);
        Vector3 pivotRotation = new Vector3(pivotTransform.rotation.eulerAngles.x, pivotTransform.rotation.eulerAngles.y, pivotTransform.rotation.eulerAngles.z);
        Vector3 cameraPosition = new Vector3(cameraTransform.localPosition.x, cameraTransform.localPosition.y, cameraTransform.localPosition.z);

        // Move and rotate camera and it's pivot.
        cameraUtilities.moveCameraToBuilding(cameraPosition, pivotPosition, pivotRotation);

        // Activate selected building arrow
        activateBuildingArrow(buildingInfo);
        
        
    }

    private void activateBuildingArrow(BuildingPosition buildingInfo)
    {
        arrowActivator.GetComponent<DoubleClick>().activateArrow(buildingInfo.building.GetComponent<BuildingProperties>());
    }

    private void deactivatePreviousBuildingArrow(BuildingPosition buildingInfo)
    {
        arrowActivator.GetComponent<DoubleClick>().deActivateArrow(buildingInfo.building.GetComponent<BuildingProperties>());
    }
}
