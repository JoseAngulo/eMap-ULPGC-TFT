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
    public float _longClickTime = 0.6f;
    public float _doubleClickTime = 0.4f;

    [DllImport("__Internal")]
    private static extern void OpenPageInNewTab(string url);

    void Start()
    {
        platformID = -1;
        maxRayDistance = 100.0f;
    }


    void Update ()
    {
        
        if (isLongClick(0))
        {
            Debug.Log("Se ha hecho un click largo");
        }

        if (isDoubleClickOnBuilding(0))
        {
            Debug.Log("Se ha hecho un doble click");
            Debug.Log("Valor de building tras docle click: " + building);

            if (building)
            {
                OpenURLOnBrowser();
            }
           
        }

    }

    private GameObject getBuildingArrow(BuildingProperties building)
    {
        return building.selectedArrow;
    }

    private void deactivateArrow()
    {

        building.selectedArrow.SetActive(false);
    }

    private void activateArrow()
    {
        building.selectedArrow.SetActive(true);
    }
    
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

    private bool isDoubleClickOnBuilding(int _mouseButton)
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        if (Input.GetMouseButtonDown(_mouseButton))
        {

            clicks++;

            if (clicks == 1)
            {
                Debug.Log("Clicks vale 1");
                lastTimer = Time.unscaledTime;
                /*
                if (EventSystem.current.IsPointerOverGameObject(platformID))
                {
                    Debug.Log("Elemento de interfaz gráfica pulsado");
                    return false;
                }*/

                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);

                if (Physics.Raycast(ray, out rayHit, maxRayDistance, clickablesLayer))
                {

                    //Debug.Log("Se ha interceptado una capa válida");

                    // Update actual selected building.
                    if (building) { deactivateArrow(); }
                    building = rayHit.collider.transform.root.GetComponent<BuildingProperties>();

                    activateArrow();
                }
                else
                {
                    //Debug.Log("No se ha interceptado una capa válida");
                    
                    if (building) { deactivateArrow(); }
                    Debug.Log("Clicks: " + clicks);
                    building = null;
                    clicks = 0;
                }


            }

            if (clicks >= 2)
            {
                Debug.Log("Clicks vale 2");
                if (!(Physics.Raycast(ray, out rayHit, maxRayDistance, clickablesLayer)) && building) {
                   Debug.Log("Anulando building, el segundo click no fue en un edificio: " + building);
                   deactivateArrow();
                   building = null;
                }
                else
                {
                    BuildingProperties clickedBuilding = rayHit.collider.transform.root.GetComponent<BuildingProperties>();
                    if (!clickedBuilding.name.Equals(building.name))
                    {
                        deactivateArrow();
                        building = clickedBuilding;
                        activateArrow();
                        return false;
                    }

                }

                Debug.Log("Valor de building tras comprobar segundo click: " + building);

                currentTimer = Time.unscaledTime;
                float difference = currentTimer - lastTimer;

                if (difference <= _doubleClickTime)
                {
                    Debug.Log("Clicks: " + clicks);
                    clicks = 0;
                    Debug.Log("Tiempo de doble click cumplido: " + clicks);
                    return true;
                }
                else
                {
                    clicks = 0;
                    Debug.Log("Tiempo de doble click NO cumplido: " + clicks);
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
