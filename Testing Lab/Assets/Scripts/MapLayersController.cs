﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLayersController : MonoBehaviour
{

    public List<GameObject> buttons;
    private bool buttonsShowed = false;
    public float initialXOffset;
    private float xOffset;

    public void showLayerButtons()
    {

        if (!buttonsShowed)
        {
            showButtons();
        }
        else
        {
            hiddeButtons();
        }

    }

    private void showButtons()
    {
        xOffset = initialXOffset;

        foreach (GameObject button in buttons)
        {
            button.gameObject.SetActive(true);
            LeanTween.moveX(button.GetComponent<RectTransform>(), xOffset, 1f).setEaseOutCirc();
            xOffset += initialXOffset;
        }

        buttonsShowed = true;

    }
    
    public void hiddeButtons()
    {
        xOffset = 0f;
        LTDescr leanButtonDescription;

        foreach (GameObject button in buttons)
        {
            leanButtonDescription = LeanTween.moveX(button.GetComponent<RectTransform>(), xOffset, 1f).setEaseOutCirc();
            leanButtonDescription.setOnComplete(() => button.gameObject.SetActive(false));
        }

        buttonsShowed = false;

    }

}
