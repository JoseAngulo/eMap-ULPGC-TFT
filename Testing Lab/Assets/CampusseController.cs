using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CampusseController : MonoBehaviour
{

    public List<RectTransform> buttons;
    private bool buttonsShowed = false;
    private float initialYOffset;
    public float yOffset;
    public float animationDuration;


    private void Start()
    {
        Debug.Log("LA PLATAFORMA ES: " + Application.platform);
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {

            this.gameObject.SetActive(false);
        }
        else
        {
            initialYOffset = buttons[0].anchoredPosition.y;
        }

    }

    public void showCampussesButtons()
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


    public void OpenCampusScene(String sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }


}
