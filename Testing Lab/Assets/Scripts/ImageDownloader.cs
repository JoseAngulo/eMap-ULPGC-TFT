using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;


public class ImageDownloader : MonoBehaviour
{

    private string url;

    private void Start()
    {
        //File url
        //Save Path
        //string savePath = "C:\\Users\\Alpha\\Documents\\Unity\\eMap ULPGC\\Testing Lab\\Assets\\Resources\\MapImages\\";
        //string savePath = Path.Combine(Application.persistentDataPath, "data");
        //savePath = Path.Combine(savePath, "Images");
        //savePath = Path.Combine(savePath, "logo.png");
        //downloadImage(url, savePath);
    }

    public void downloadImage(string url, string pathToSaveImage, string layerName, int imageIndex)
    {
        this.url = url;
        pathToSaveImage += layerName + "_" + imageIndex + ".jpg";
        StartCoroutine(_downloadImage(url, pathToSaveImage));
    }

    private IEnumerator _downloadImage(string url, string savePath)
    {
        
        UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url);
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                //Debug.Log("ERROR DE NETWORKING");
                Debug.LogError(uwr.error);
            }
            else
            {
                Debug.Log("Success");
                //Texture myTexture = DownloadHandlerTexture.GetContent(uwr);
                byte[] results = uwr.downloadHandler.data;
                saveImage(savePath, results);

            }
        }
    }

    void saveImage(string path, byte[] imageBytes)
    {
        //Debug.Log("TRATANDO DE GUARDAR IMAGEN");
        //Create Directory if it does not exist
        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        try
        {
            //Debug.Log("GUARDANDO.............");
            File.WriteAllBytes(path, imageBytes);
            //Debug.Log("IMAGE GUARDADA CON ÉXITO");
            Debug.Log("Saved Data to: " + path.Replace("/", "\\"));
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed To Save Data to: " + path.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
        }
    }

    public IEnumerator loadImageFromServer(string imageURL, GameObject plane)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imageURL);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("URL DE TEXTURA: " + imageURL);
            Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            plane.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", texture);
            Debug.Log("TEXTURA ACTUALIZADA: " + texture.name);
        }
    }

    public byte[] loadImage(string path)
    {
        byte[] dataByte = null;

        //Exit if Directory or File does not exist
        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Debug.LogWarning("EL DIRECTORIO DE CARGA NO EXISTE");
            return null;
        }

        if (!File.Exists(path))
        {
            Debug.Log("EL ARCHIVO NO EXISTE");
            return null;
        }

        try
        {
            Debug.Log("CARGANDO IMAGEN DESDE LOADIMAGE");
            dataByte = File.ReadAllBytes(path);
            Debug.Log("Loaded Data from: " + path.Replace("/", "\\"));
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed To Load Data from: " + path.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
        }

        return dataByte;
    }
    

}