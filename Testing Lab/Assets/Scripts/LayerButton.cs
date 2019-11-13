using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayerButton : MonoBehaviour
{
    public GameObject layerManager;
    public string layerName;
    public string baseImageURL;

    void Start()
    {
        Button button = gameObject.GetComponent<Button>();
        MapLayerManager mapLayerManager = layerManager.GetComponent<MapLayerManager>();

        button.onClick.AddListener(delegate { mapLayerManager.UpdateLayer(layerName, baseImageURL); });
        
    }

}
