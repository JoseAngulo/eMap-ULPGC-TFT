using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CampusButtonController : MonoBehaviour
{

    public string campusName;
    public GameObject campusManager;

    void Start()
    {
        Button button = gameObject.GetComponent<Button>();
        CampusseController campusController = campusManager.GetComponent<CampusseController>();
        button.onClick.AddListener(delegate { campusController.OpenCampusScene(campusName); });
    }

}
