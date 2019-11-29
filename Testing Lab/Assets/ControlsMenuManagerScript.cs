using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenuManagerScript : MonoBehaviour
{
    public GameObject controlsMenu;
    public GameObject mapLayerButtons;
    public GameObject campussesButtons;

    public void ShowControlsMenu()
    {
        controlsMenu.SetActive(true);
        mapLayerButtons.SetActive(false);
        campussesButtons.SetActive(false);
    }

    public void HiddeControlsMenu()
    {
        controlsMenu.SetActive(false);
        mapLayerButtons.SetActive(true);
        campussesButtons.SetActive(true);
    }

    


}
