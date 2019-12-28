using UnityEngine;

public class JavaScriptWebCampusManager : MonoBehaviour
{
    public GameObject campusController;


    public void changeCampusFromJavascript(string campusName)
    {
        campusController.GetComponent<CampusseController>().OpenCampusScene(campusName);
    }
}
