using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoubleClick : MonoBehaviour
{

    public float _doubleClickTime = 0.4f;
    public GameObject dropdown;

    [SerializeField]
    private LayerMask clickablesLayer;

    [SerializeField]
    private float maxRayDistance;

    private BuildingProperties building;



    
    private float firstClickTime;
    private float timeBetweenClicks;
    private int clickCounter;
    private bool coroutineAllowed;
    private bool doubleClickDetected;

    [DllImport("__Internal")]
    private static extern void OpenPageInNewTab(string url);

    // Start is called before the first frame update
    void Start()
    {
        firstClickTime = 0f;
        timeBetweenClicks = 0.3f;
        clickCounter = 0;
        maxRayDistance = 50.0f;
        coroutineAllowed = true;
        doubleClickDetected = false;
    }

    // Update is called once per frame
    void Update()
    {
        isDoubleClickOnBuilding(0);

        //Debug.Log("Valor de building tras docle click: " + building);

        if (doubleClickDetected) {
            OpenURLOnBrowser();
            doubleClickDetected = false;
        }
               
    }

    private void isDoubleClickOnBuilding(int _mouseButton)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;


        if (Input.GetMouseButtonDown(0))
        {
            // Don't perform any action if an UI element was clicked
            if (EventSystem.current.IsPointerOverGameObject()) { return; }

            clickCounter++;

            if (clickCounter == 1)
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out rayHit, maxRayDistance, clickablesLayer))
                {
                    // Update actual selected building.
                    Debug.Log("Se ha picado sobre edificio");
                    if (building) { deactivateArrow(); }
                    building = rayHit.collider.transform.root.GetComponent<BuildingProperties>();

                    //* Update dropdown label with building name //*
                    dropdown.GetComponent<DropdownController>().changeDropLabel(building.name);

                    if (!getBuildingArrow(building).activeSelf)
                    {
                        Debug.Log("Activando flecha");
                        activateArrow();
                    };
                }
                else
                {
                    if (building) { deactivateArrow(); }
                    Debug.Log("No se ha picado sobre edificio");
                    building = null;
                    clickCounter = 0;
                }

                if (coroutineAllowed)
                {
                    Debug.Log("Comprobando doble click");
                    firstClickTime = Time.time;
                    StartCoroutine(DoubleClickDetection());
                }
            }

        }
                
    }

    private IEnumerator DoubleClickDetection()
    {
        coroutineAllowed = false;
        bool wasDoubleClicked = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        
        while (Time.time <= firstClickTime + timeBetweenClicks && !wasDoubleClicked)
        {
            if (clickCounter == 2)
            {
                Debug.Log("Clicks vale 2");

                if (!(Physics.Raycast(ray, out rayHit, maxRayDistance, clickablesLayer)) && building)
                {
                    Debug.Log("Anulando building, el segundo click no fue en un edificio: " + building);
                    Debug.Log("Desactivando flecha");
                    deactivateArrow();
                    building = null;
                    doubleClickDetected = false;
                    break;
                }
                else
                {
                    BuildingProperties selectedBuilding = rayHit.collider.transform.root.GetComponent<BuildingProperties>();
                    if (!selectedBuilding.name.Equals(building.name))
                    {
                        Debug.Log("Desactivando flecha antigua");
                        deactivateArrow();
                        building = selectedBuilding;
                        Debug.Log("Desactivando flecha nueva");
                        activateArrow();
                        doubleClickDetected = false;
                        break;
                    }

                    //Debug.Log("Double click");

                }

                wasDoubleClicked = true;
                doubleClickDetected = true;
                Debug.Log("Doble click detectado");
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        clickCounter = 0;
        firstClickTime = 0f;
        coroutineAllowed = true;
    }



    private GameObject getBuildingArrow(BuildingProperties building) { return building.selectedArrow; }

    private void deactivateArrow() { building.selectedArrow.SetActive(false); }

    private void activateArrow() { building.selectedArrow.SetActive(true); }

    public void activateArrow(BuildingProperties buildingProperties) { buildingProperties.selectedArrow.SetActive(true); }

    public void deActivateArrow(BuildingProperties buildingProperties) { buildingProperties.selectedArrow.SetActive(false); }

    private void OpenURLOnBrowser()
    {

        #if UNITY_WEBGL && !UNITY_EDITOR
            OpenPageInNewTab(building.buildingMainPage);
          
        #else
        Application.OpenURL(building.buildingMainPage);

        #endif

    }

}
