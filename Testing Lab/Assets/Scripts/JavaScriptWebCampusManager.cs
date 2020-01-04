using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JavaScriptWebCampusManager : MonoBehaviour
{

    //[DllImport("__Internal")]
    //private static extern void HideLoadingScreenFadeOut();

    //[DllImport("__Internal")]
    //public static extern int AddNumbers(int x, int y);

    public GameObject campusController;

    //private static bool _isfirstSceneLoaded = false;

/*
    void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "Tafira" && !_isfirstSceneLoaded)
        {
            SceneManager.sceneLoaded += OnSceneLoadedFadeOutLoadingScreen; 
        }
        
    }*/
    /*
    private void OnSceneLoadedFadeOutLoadingScreen(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("LA SUMA DE ADD_NUMBERS ES: " + AddNumbers(4,5));
        //HideLoadingScreenFadeOut();
        SceneManager.sceneLoaded -= OnSceneLoadedFadeOutLoadingScreen;
        _isfirstSceneLoaded = true;
    }*/

    public void changeCampusFromJavascript(string campusName)
    {
        campusController.GetComponent<CampusseController>().OpenCampusScene(campusName);
    }
    /*
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoadedFadeOutLoadingScreen;
    }*/


}
