using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeTextScript : MonoBehaviour
{
    public float fromY;
    public float toY;

    void Start()
    {

        RectTransform welcomeTextRect = gameObject.GetComponent<RectTransform>();
        LeanTween.moveY(welcomeTextRect, fromY, 1.5f).setEaseOutCubic();
        LeanTween.alphaText(welcomeTextRect, 1f, 1.5f).setOnComplete(() => LeanTween.alphaText(welcomeTextRect, 0f, 1.5f).setDelay(0.5f));
               
        LeanTween.moveY(welcomeTextRect, toY, 1.5f).setEaseInCubic().setDelay(2f);
    }
}
