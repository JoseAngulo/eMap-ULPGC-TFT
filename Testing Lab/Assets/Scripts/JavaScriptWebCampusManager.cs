//using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JavaScriptWebCampusManager : MonoBehaviour
{
    /*
    [DllImport("__Internal")]
    private static extern void HideLoadingScreenFadeOut();

    [DllImport("__Internal")]
    private static extern int AddNumbers(int x, int y);*/

    public GameObject campusController;

    private static bool _isfirstSceneLoaded = false;


    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "Tafira" && !_isfirstSceneLoaded)
        {
            SceneManager.sceneLoaded += OnSceneLoadedFadeOutLoadingScreen; 
        }
        
    }

    private void OnSceneLoadedFadeOutLoadingScreen(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log(AddNumbers(4,5));
        //HideLoadingScreenFadeOut();
        SceneManager.sceneLoaded -= OnSceneLoadedFadeOutLoadingScreen;
        _isfirstSceneLoaded = true;
    }

    public void changeCampusFromJavascript(string campusName)
    {
        campusController.GetComponent<CampusseController>().OpenCampusScene(campusName);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoadedFadeOutLoadingScreen;
    }


}
