using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CampusInfoScript : MonoBehaviour
{
    private Hashtable indexedBuildings = new Hashtable();
    public BuildingPosition[] positions;
    //public string loadedLayerName = "OrtoExpress";

    [Serializable]
    public struct BuildingPosition
    {
        public GameObject building;
        public GameObject camera;
    }

    private void Awake()
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





}
