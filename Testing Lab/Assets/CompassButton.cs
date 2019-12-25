using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassButton : MonoBehaviour
{
    public RectTransform compass;

    private bool compassShowed = false;

    private float initialYOffset;
    public float yOffset;
    public float animationDuration;



    private void Start()
    {
        initialYOffset = compass.anchoredPosition.y;
        Debug.Log("Y OFFSET: " + initialYOffset);
    }

    public void showCompassUI()
    {

        if (!compassShowed)
        {
            showCompass();
        }
        else
        {
            hiddeCompass();
        }

    }

    private void showCompass()
    {
        //Debug.Log(buttons[0].transform.position.y); //initialYOffset = buttons[0].transform.position.y;
        Debug.Log(yOffset);
        compass.gameObject.SetActive(true);
        LeanTween.moveY(compass.GetComponent<RectTransform>(), yOffset, animationDuration).setEaseOutCirc();

        compassShowed = true;

    }

    public void hiddeCompass()
    {
        LTDescr leanButtonDescription;
        leanButtonDescription = LeanTween.moveY(compass.GetComponent<RectTransform>(), initialYOffset, animationDuration).setEaseOutCirc();
        leanButtonDescription.setOnComplete(() => compass.gameObject.SetActive(false));

        compassShowed = false;

    }
}
