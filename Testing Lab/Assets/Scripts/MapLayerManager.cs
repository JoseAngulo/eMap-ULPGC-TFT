using System;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class MapLayerManager : MonoBehaviour
{

    //private string savePath = "C:\\Users\\Alpha\\Documents\\Unity\\Escena de pruebas\\Assets\\Images\\downloaded\\";
    private string savePath;


    private ImageDownloader downloader;
    private int tilesNumber;

    private void Awake()
    {
        downloader = gameObject.AddComponent<ImageDownloader>();
        tilesNumber = 4;
        savePath = Application.dataPath;
        String sceneName = SceneManager.GetActiveScene().name;
        if (Application.platform == RuntimePlatform.WebGLPlayer) { savePath = Application.dataPath + "/Resources/" + sceneName + "/"; }
        if (Application.platform == RuntimePlatform.WindowsEditor) { savePath = Application.dataPath + "/Resources/Images/downloaded/" + sceneName + "/"; }
    }

    public void UpdateLayer(string layerName, string baseImageURL)
    {
        
        Debug.Log("DATAPATH: " + Application.dataPath);

        setLayerTexture(layerName);

    }
    
    public void setLayerTexture(string layerName)
    {
        
        Debug.Log("BUSCANDO PLANOS");
        GameObject[] planes = GameObject.FindGameObjectsWithTag("MapTerrain");
        
        Debug.Log("ENCONTRADOS " + planes.Length + " planos");
        
        byte[] imageBytes;
        Texture2D texture = new Texture2D(2048, 2048);

        Debug.Log("CARGANDO TODAS LAS TEXTURAS");
        Debug.Log("VALOR DE TILESNUMBER = " + tilesNumber);
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
        Debug.Log("SE HA TERMINADO DE CARGAR TODAS LAS TEXTURAS... ");

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
