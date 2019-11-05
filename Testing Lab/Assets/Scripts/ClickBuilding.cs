using TMPro;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.EventSystems;

public class ClickBuilding : MonoBehaviour {

    [SerializeField]
    private LayerMask clickablesLayer;

    [SerializeField]
    private float maxRayDistance;

    private BuildingProperties building;
    private static float timer;
    private static float lastTimer;
    private static float currentTimer;
    private static int clicks;


    private int platformID;     // Use -1 when using Editor controls (WebGL builds,etc) and 0 when using mobile controls (Android, iOS).
    private float _longClickTime;
    private float _doubleClickTime;

    [DllImport("__Internal")]
    private static extern void OpenPageInNewTab(string url);

    void Awake()
    {
        platformID = -1;
        maxRayDistance = 50.0f;
        _longClickTime = 0.6f;
        _doubleClickTime = 0.4f;
    }


    void Update ()
    {

        if (isLongClick(0))
        {
            Debug.Log("Se ha hecho un click largo");
        }

        if (isDoubleClick(0))
        {
            Debug.Log("Se ha hecho un doble click");

            if (building)
            {
                OpenURLOnBrowser();
            }
           
        }


        if (Input.GetMouseButtonDown(0))
        {

            // Check if an UI element was clicked.

            
        }

        // Update mainPageButton properties based on selected building.
        //UpdateMainPageButton();

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
    /*
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
        
    }*/

    private bool isLongClick(int _mouseButton)
    {
        if (Input.GetMouseButton(_mouseButton))
        {
            timer += Time.deltaTime;

            if (timer >= _longClickTime)
            {
                clicks = 0;
                timer = 0;
                return true;
            }
        }

        if (Input.GetMouseButtonUp(_mouseButton))
        {
            timer = 0;
        }


        return false;
    }

    private bool isDoubleClick(int _mouseButton)
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        if (Input.GetMouseButtonDown(_mouseButton))
        {

            clicks++;

            if (clicks == 1)
            {
                lastTimer = Time.unscaledTime;
                
                if (EventSystem.current.IsPointerOverGameObject(platformID))
                {
                    Debug.Log("Elemento de interfaz gráfica pulsado");
                    return false;
                }

                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);

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

            if (clicks >= 2)
            {
                if (!(Physics.Raycast(ray, out rayHit, maxRayDistance, clickablesLayer)) && building) {
                   deactivateArrow();
                   building = null;
                }
                
                currentTimer = Time.unscaledTime;
                float difference = currentTimer - lastTimer;

                if (difference <= _doubleClickTime)
                {
                    clicks = 0;
                    return true;
                }
                else
                {
                    clicks = 0;
                }

                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                
            }
        }
        
        return false;
    }

    private void OpenURLOnBrowser()
    {

        #if UNITY_WEBGL && !UNITY_EDITOR
            OpenPageInNewTab(building.buildingMainPage);
          
        #else
            Application.OpenURL(building.buildingMainPage);

        #endif

    }


}
