using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOnBuilding : MonoBehaviour {


    public string name;
    public string buildingMainPage = "https://istic.es/";

    void Start () {
        name = "Facultad de Teología";
	}

    public void PrintName()
    {
        Debug.Log("El nombre del edificio seleccionado es: " + name);
    }


    public void OpenURLOnBrowser()
    {
        Application.OpenURL(buildingMainPage);
    }
	
}
