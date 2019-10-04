using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMapImageOnClick : MonoBehaviour
{

    public GameObject map;
    public String imageURL;

    public void changeMapImage()
    {
        StartCoroutine(imageLoader());
    }
    
    private IEnumerator imageLoader()
    {
        WWW www = new WWW(imageURL);
        yield return www;
        if (www == null)
        {
            Debug.Log("IMAGEN NO ENCONTRADA");
        }
        else
        {
            Debug.Log("CAMBIANDO IMAGEN...");
        }
        map.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", www.texture);
    }
}
