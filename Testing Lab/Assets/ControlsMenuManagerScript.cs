using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenuManagerScript : MonoBehaviour
{
    public List<GameObject> uiElements;

    //public GameObject controlsMenu;
    //public GameObject mapLayerButtons;
    //public GameObject campussesButtons;

    public void ShowControlsMenu()
    {

        foreach (GameObject element in uiElements)
        {
            element.SetActive(false);
        }

        uiElements[0].SetActive(true);

        /*
        controlsMenu.SetActive(true);
        mapLayerButtons.SetActive(false);
        campussesButtons.SetActive(false);*/
    }

    public void HiddeControlsMenu()
    {
        foreach (GameObject element in uiElements)
        {
            element.SetActive(true);
        }

        uiElements[0].SetActive(false);
        /*
        controlsMenu.SetActive(false);
        mapLayerButtons.SetActive(true);
        campussesButtons.SetActive(true);*/
    }

    


}
