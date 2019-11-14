using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLayersController : MonoBehaviour
{

    //private Button[] layerButtons;
    public List<Button> objects;

    /*
    void Start()
    {
        layerButtons = GetComponentsInChildren<Button>();
    }*/

    public void showLayerButtons()
    {
        foreach (var button in objects)
        {
            button.gameObject.SetActive(!button.IsActive());
        }
    }

}
