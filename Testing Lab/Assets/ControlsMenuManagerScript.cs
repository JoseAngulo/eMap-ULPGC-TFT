using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenuManagerScript : MonoBehaviour
{
    public GameObject controlsMenu;
    public GameObject mapLayerButtons;

    public void ShowControlsMenu()
    {
        controlsMenu.SetActive(true);
        mapLayerButtons.SetActive(false);
        //Time.timeScale = 0f;
    }

    public void HiddeControlsMenu()
    {
        controlsMenu.SetActive(false);
        mapLayerButtons.SetActive(true);
        //Time.timeScale = 1f;
    }

    


}
