using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLayersController : MonoBehaviour
{

    //private Button[] layerButtons;
    public List<GameObject> buttons;
    private bool buttonsShowed = false;
    public float initialXOffset;
    private float xOffset;

    //public GameObject b1;
    //public GameObject b2;
    //public GameObject b3;

    /*
    void Start()
    {
        layerButtons = GetComponentsInChildren<Button>();
    }*/

    public void showLayerButtons()
    {

        if (!buttonsShowed)
        {

            showButtons();
            /*
            xOffset = 85f;

            LeanTween.moveX(b1.GetComponent<RectTransform>(), xOffset, 1f).setEaseOutCirc();
            xOffset += 85f;
            LeanTween.moveX(b2.GetComponent<RectTransform>(), xOffset, 1f).setEaseOutCirc();
            xOffset += 85f;
            LeanTween.moveX(b3.GetComponent<RectTransform>(), xOffset, 1f).setEaseOutCirc();

            buttonsShowed = true;*/
        }
        else
        {
            hiddeButtons();
            /*
            xOffset = 0f;
            LeanTween.moveX(b1.GetComponent<RectTransform>(), xOffset, 1f).setEaseOutCirc();
            LeanTween.moveX(b2.GetComponent<RectTransform>(), xOffset, 1f).setEaseOutCirc();
            LeanTween.moveX(b3.GetComponent<RectTransform>(), xOffset, 1f).setEaseOutCirc();
            //xOffset -= 85f;
            
            //xOffset -= 85f;
            
           
            buttonsShowed = false;*/
        }

       



        /*
        foreach (GameObject button in objects)
        {
            Debug.Log("xOffset: " + xOffset);
            Debug.Log(button.name);
            LeanTween.moveX(button,xOffset, 1f).setEaseOutCirc();
            xOffset += 0.25f;
            //button.gameObject.SetActive(!button.IsActive());
        }*/
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

        //deactivateButton();

        buttonsShowed = false;

    }

    private void deactivateButton(Button button)
    {
        //foreach (GameObject button in buttons) { button.gameObject.SetActive(false); }
    }
}
