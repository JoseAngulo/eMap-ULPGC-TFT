using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLayersController : MonoBehaviour
{

    public List<GameObject> buttons;
    private bool buttonsShowed = false;
    public float initialXOffset;
    private float initialYOffset;
    private float xOffset;
    public float yOffset;
    public float animationDuration;
    public float animationDelay;


    private void Start()
    {
        initialYOffset = buttons[0].transform.position.y;
        //animationDuration = 1f;
        //animationDelay = 0.5f;

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

        foreach (GameObject button in buttons)
        {
            Debug.Log(yOffset);
            button.gameObject.SetActive(true);
            LeanTween.moveY(button.GetComponent<RectTransform>(), yOffset, animationDuration).setEaseOutCirc().setDelay(animationDelay);
            //animationDuration += animationDelay;
        }

        buttonsShowed = true;

    }

    public void hiddeButtons()
    {
        LTDescr leanButtonDescription;

        foreach (GameObject button in buttons)
        {
            leanButtonDescription = LeanTween.moveY(button.GetComponent<RectTransform>(), initialYOffset, animationDuration).setEaseOutCirc().setDelay(animationDelay);
            leanButtonDescription.setOnComplete(() => button.gameObject.SetActive(false));
            //animationDuration += animationDelay;
        }

        buttonsShowed = false;

    }


}
