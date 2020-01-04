using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLayersController : MonoBehaviour
{

    public List<RectTransform> buttons;
    private bool buttonsShowed = false;
    private float initialYOffset;
    public float yOffset;
    public float animationDuration;


    private void Start()
    {
        initialYOffset = buttons[0].anchoredPosition.y;

    }

    public void showLayersButtons()
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
        Debug.Log(buttons[0].transform.position.y); //initialYOffset = buttons[0].transform.position.y;

        foreach (RectTransform button in buttons)
        {
            Debug.Log(yOffset);
            button.gameObject.SetActive(true);
            LeanTween.moveY(button, yOffset, animationDuration).setEaseOutCirc();
        }

        buttonsShowed = true;

    }

    public void hiddeButtons()
    {
        LTDescr leanButtonDescription;

        foreach (RectTransform button in buttons)
        {
            leanButtonDescription = LeanTween.moveY(button, initialYOffset, animationDuration).setEaseOutCirc();
            leanButtonDescription.setOnComplete(() => button.gameObject.SetActive(false));
        }

        buttonsShowed = false;

    }


}
