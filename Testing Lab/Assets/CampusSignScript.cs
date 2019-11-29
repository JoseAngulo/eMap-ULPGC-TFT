using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CampusSignScript : MonoBehaviour
{
    public float fromY;

    // Update is called once per frame
    void Update()
    {
        RectTransform welcomeTextRect = gameObject.GetComponent<RectTransform>();
        LeanTween.moveY(welcomeTextRect, fromY, 1f).setEaseOutBack();
    }
}
