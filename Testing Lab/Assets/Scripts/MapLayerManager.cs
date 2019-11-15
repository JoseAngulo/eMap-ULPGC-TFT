﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.IO;

public class MapLayerManager : MonoBehaviour
{

    //private string savePath = "C:\\Users\\Alpha\\Documents\\Unity\\Escena de pruebas\\Assets\\Images\\downloaded\\";
    private string savePath;


    private ImageDownloader downloader;
    private int tilesNumber;

    private void Start()
    {
        downloader = gameObject.AddComponent<ImageDownloader>();
        tilesNumber = 4;
        savePath = Application.dataPath;
        if (Application.platform == RuntimePlatform.WebGLPlayer) { savePath = Application.dataPath + "/Resources/"; }
        if (Application.platform == RuntimePlatform.WindowsEditor) { savePath = Application.dataPath + "/Resources/Images/downloaded/"; }
    }

    public void UpdateLayer(string layerName, string baseImageURL)
    {
        
        Debug.Log("DATAPATH: " + Application.dataPath);

        setLayerTexture(layerName);

    }
    
    private void setLayerTexture(string layerName)
    {
        
        Debug.Log("BUSCANDO PLANOS");
        GameObject[] planes = GameObject.FindGameObjectsWithTag("MapTerrain");
        
        Debug.Log("ENCONTRADOS " + planes.Length + " planos");
        
        byte[] imageBytes;
        Texture2D texture = new Texture2D(2048, 2048);

        for (int i = 0; i < tilesNumber; i++)
        {
            
            Debug.Log("ACTUALIZANDO PLANO: " + planes[i].name);

            string subLayer = planes[i].name;
            char imageIndex = subLayer[subLayer.Length - 1];

            string loadPath = savePath + layerName + "_" + imageIndex;
            string fileName = layerName + "_" + imageIndex;
            

            Debug.Log("CARGANDO IMAGEN....... " + loadPath);
            
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                imageBytes = downloader.loadImage(loadPath + getImageExtension(loadPath, fileName));
                //imageBytes = downloader.loadImage(loadPath + ".jpg");
                texture = new Texture2D(2048, 2048);
                texture.LoadImage(imageBytes);
                planes[i].GetComponent<MeshRenderer>().material.SetTexture("_MainTex", texture);
                Debug.Log("PLANO ACTUALIZADO *** ");
            }

            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {

                if (layerName.Equals("Cultural") || layerName.Equals("Callejero"))
                {
                    StartCoroutine(downloader.loadImageFromServer(loadPath + ".png", planes[i]));
                }
                else
                {
                    StartCoroutine(downloader.loadImageFromServer(loadPath + ".jpg", planes[i]));
                }
            }
        } 
    } 
    
    private string getImageExtension(string loadPath, string fileName)
    {
        DirectoryInfo dir = new DirectoryInfo(savePath);
        FileInfo[] info = dir.GetFiles(fileName + "*.*");
        string foundFileName = info[0].Name;

        Debug.Log("ARCHIVO PARA COGER EXTENSIÓN: " + foundFileName);
        Debug.Log("EXTENSIÓN: " + foundFileName.Substring(foundFileName.LastIndexOf('.')));

        return foundFileName.Substring(foundFileName.LastIndexOf('.'));
    }
}