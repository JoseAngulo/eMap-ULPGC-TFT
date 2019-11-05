using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickBuilding : MonoBehaviour {

    [SerializeField]
    private LayerMask clickablesLayer;

    [SerializeField]
    private float maxRayDistance = 50.0f;

    private BuildingProperties building;

    private int platformID = -1;     // Use -1 when using Editor controls (WebGL builds,etc) and 0 when using mobile controls (Android, iOS).


	void Update ()
    {

        if (isLongClick(0))
        {
            Debug.Log("Se ha hecho un click largo");
        }

        if (isDoubleClick(0))
        {
            Debug.Log("Se ha hecho un doble click");
        }


        if (Input.GetMouseButtonDown(0))
        {

            // Check if an UI element was clicked.

            if (EventSystem.current.IsPointerOverGameObject(platformID))
            {
                Debug.Log("Elemento de interfaz gráfica pulsado");
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
            RaycastHit rayHit;

            if (Physics.Raycast(ray, out rayHit, maxRayDistance, clickablesLayer))
            {

                //Debug.Log("Se ha interceptado una capa válida");

                // Update actual selected building.
                building = rayHit.collider.transform.root.GetComponent<BuildingProperties>();

                activateArrow();
            }
            else
            {
                //Debug.Log("No se ha interceptado una capa válida");
                //building = null;
                if (building) { deactivateArrow(); }
                
            }
        }

        // Update mainPageButton properties based on selected building.
        UpdateMainPageButton();

    }

    private GameObject getBuildingArrow(BuildingProperties building)
    {
        return building.selectedArrow;
    }

    private void deactivateArrow()
    {
        building.selectedArrow.SetActive(false);

        /*foreach (GameObject arrow in Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Arrow"))
        {
            arrow.SetActive(false);
        }*/
    }

    private void activateArrow()
    {
        building.selectedArrow.SetActive(true);
    }

    private void UpdateMainPageButton()
    {
        GameObject urlButton = GameObject.Find("OpenMainPage");
        MainPageButtonProperties buttonProperties = urlButton.GetComponent<MainPageButtonProperties>();

        if (building != null){
            buttonProperties.buildingName = building.buildingName;
            buttonProperties.buildingMainPage = building.buildingMainPage;
            urlButton.GetComponentInChildren<TextMeshProUGUI>().text = "Ir a la página de: " + building.name;
        }
        else
        {
            buttonProperties.buildingName = "None";
            buttonProperties.buildingMainPage = "";
            urlButton.GetComponentInChildren<TextMeshProUGUI>().text = "Seleccione un edificio";

        }
        
    }

    private bool isLongClick(int _mouseButton)
    {
        return false;
    }

    private bool isDoubleClick(int _mouseButton)
    {
        return false;
    }

}
