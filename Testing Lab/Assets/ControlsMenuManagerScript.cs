using System.Collections.Generic;
using UnityEngine;

public class ControlsMenuManagerScript : MonoBehaviour
{
    public List<GameObject> uiElements;


    public void ShowControlsMenu()
    {
        foreach (GameObject element in uiElements)
        {
            element.SetActive(false);
        }

        uiElements[0].SetActive(true);

    }

    public void HiddeControlsMenu()
    {
        foreach (GameObject element in uiElements)
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer && element.name == "CampussesButton")
            {
                continue;
            }

            element.SetActive(true);

        }

        uiElements[0].SetActive(false);

    }

}
