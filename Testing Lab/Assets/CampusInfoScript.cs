using System;
using System.Collections;
using UnityEngine;


public class CampusInfoScript : MonoBehaviour
{
    private Hashtable indexedBuildings = new Hashtable();
    public BuildingPosition[] positions;
    private static string loadedLayerName = "OrtoExpress";

    [Serializable]
    public struct BuildingPosition
    {
        public GameObject building;
        public GameObject camera;
    }

    private void Start()
    {
        for (int i = 0; i < positions.Length; i++)
        {
            indexedBuildings.Add(i, positions[i]);
        }
    }

    public Hashtable getIndexedBuildings()
    {
        return indexedBuildings;
    }

    public BuildingPosition getBuilding(int index)
    {
        Debug.Log("DEVOLVIENDO EL EDIFICIO CON ÍNDICE..." + index);
        return (BuildingPosition)indexedBuildings[index];
    }

    public void setLoadedLayerName(string layerName)
    {
        Debug.Log("CAMBIANDO LA TEXTURA " + loadedLayerName + " A " + layerName);
        loadedLayerName = layerName;
    }

    public string getLoadedLayer()
    {
        return loadedLayerName;
    }





}
