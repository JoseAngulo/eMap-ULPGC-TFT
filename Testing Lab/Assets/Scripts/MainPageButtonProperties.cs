using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MainPageButtonProperties : MonoBehaviour {

    [HideInInspector]
    public string buildingMainPage = "";

    [HideInInspector]
    public string buildingName = "Seleccione un edificio";

    [DllImport("__Internal")]
    private static extern void OpenPageInNewTab(string url);

    public void OpenURLOnBrowser()
    {
        if (buildingMainPage != "")
        {
            #if UNITY_WEBGL && !UNITY_EDITOR
            OpenPageInNewTab(buildingMainPage);
          
            #else
            Application.OpenURL(buildingMainPage);

            #endif
        }
    }

}
