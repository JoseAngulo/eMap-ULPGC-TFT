using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour {

    private Vector3 FirstPoint;
    private Vector3 SecondPoint;
    private float xAngle;
    private float yAngle;
    private float xAngleTemp;
    private float yAngleTemp;
    private Transform _XForm_Parent;
    public float zoomMinThreshold = 0.0f;
    private float maxMovementX = 2.0f;
    private float maxMovementY = 5.0f;
    public float mobileZoomSpeed = 2.0f;
    private float previousDistance;
    private bool supressTilting = false;
    private bool supressZooming = false;


    void Start()
    {
        xAngle = 0;
        yAngle = 0;
        FirstPoint = Vector3.zero;
        SecondPoint = Vector3.zero;
        _XForm_Parent = transform.parent;
       // _XForm_Parent.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);

    }

    void LateUpdate()
    {
        
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);   //Store informations about first touch
            Touch touch1 = Input.GetTouch(1);   //Store informations about second touch
                                                
            if (touch0.phase == TouchPhase.Began && touch1.phase == TouchPhase.Began)
            {
                FirstPoint = (touch0.position + touch1.position) / 2;
                xAngleTemp = xAngle;
                yAngleTemp = yAngle;
            }
            
            // Se comprueba si ambos dedos se mueven en la misma dirección sobre el eje vertical


            // *** AÑADIR que solo se pueda abatir la cámara si se supera un cierto umbral de movimiento en el eje vertical ****
            if (IsTilting(touch0, touch1))
            {
                SecondPoint = (touch0.position + touch1.position) / 2;
                yAngle = yAngleTemp + (SecondPoint.y - FirstPoint.y) * 90 / Screen.height;
            
            
                if (yAngle < 0f)
                    yAngle = 0f;
                else if (yAngle > 90f)
                    yAngle = 90f;
            
                if (yAngle < 0) yAngle += 360;
            
                if (yAngle > 360) yAngle -= 360;
            
                if (yAngle > 90 && yAngle < 270)
                    xAngle = xAngleTemp - (SecondPoint.x - FirstPoint.x) * 180.0f / Screen.width;

                _XForm_Parent.transform.rotation = Quaternion.Euler(yAngle, 0.0f, 0.0f);

                /*
                if (Input.touchCount == 0)
                {
                    Debug.Log("RESETEANDO ZOOMING");
                    supressZooming = false;
                }*/


            }else if (IsZooming(touch0, touch1))
            {

                Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;     //Store previous position of first touch
                Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;     //Store previous position of second touch

                float prevTouchDeltaMag = (touch0PrevPos - touch1PrevPos).magnitude;    //Calculate previous magnitude between touches, needed for calculating delta magnitude
                float touchDeltaMag = (touch0.position - touch1.position).magnitude;    //Calculate magnitude between touches, needed for calculating delta magnitude

                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;           //Calculate delta position of touches magnitude

                if (deltaMagnitudeDiff > 0 || deltaMagnitudeDiff < -0)  //Called if delta position is in range of minMovement to start zoom
                {
                    if ((touch0.deltaPosition.y < maxMovementY && touch0.deltaPosition.y > -maxMovementY) || (touch1.deltaPosition.y < maxMovementY && touch1.deltaPosition.y > -maxMovementY)) //Called if movements on Y axes are in range of maxMovement to start zoom, more about maxMovementY variable in variables commentaries
                    {
                        deltaMagnitudeDiff = (deltaMagnitudeDiff / Screen.height) * 1080;   //Calculate deltaMagnitudeDiff same for every screen resolution, so speed of zoom is same on every device

                        Camera.main.transform.Translate(0, 0, -deltaMagnitudeDiff * 0.04f * mobileZoomSpeed * Time.deltaTime);   //Moves camera by inverted deltaMagnitudeDiff
                        Camera.main.transform.localPosition = new Vector3(0, 0, Mathf.Clamp(Camera.main.transform.localPosition.z, -10.0f, -1.0f)); //Lock local position of camera, so you cant zoom too far away
                    }
                }

            }



            //Se comprueba si los dedos se mueven en la misma dirección pero en sentido contrario sobre el eje horizontal.

            /*
            Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;     //Store previous position of first touch
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;     //Store previous position of second touch

            float prevTouchDeltaMag = (touch0PrevPos - touch1PrevPos).magnitude;    //Calculate previous magnitude between touches, needed for calculating delta magnitude
            float touchDeltaMag = (touch0.position - touch1.position).magnitude;    //Calculate magnitude between touches, needed for calculating delta magnitude

            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;           //Calculate delta position of touches magnitude

            if (deltaMagnitudeDiff > 0 || deltaMagnitudeDiff < -0)  //Called if delta position is in range of minMovement to start zoom
            {
                if ((touch0.deltaPosition.y < maxMovementY && touch0.deltaPosition.y > -maxMovementY) || (touch1.deltaPosition.y < maxMovementY && touch1.deltaPosition.y > -maxMovementY)) //Called if movements on Y axes are in range of maxMovement to start zoom, more about maxMovementY variable in variables commentaries
                {
                    deltaMagnitudeDiff = (deltaMagnitudeDiff / Screen.height) * 1080;   //Calculate deltaMagnitudeDiff same for every screen resolution, so speed of zoom is same on every device

                    Camera.main.transform.Translate(0, 0, -deltaMagnitudeDiff * 0.04f *mobileZoomSpeed);   //Moves camera by inverted deltaMagnitudeDiff
                    Camera.main.transform.localPosition = new Vector3(0, 0, Mathf.Clamp(Camera.main.transform.localPosition.z, -10.0f, -1.0f)); //Lock local position of camera, so you cant zoom to far
                }
            }*/

        }

        if (Input.touchCount == 0)
        {
            Debug.Log("RESETEANDO VARIABLES DE CONTROL");
            supressTilting = false;
            supressZooming = false;
        }

    }

    private bool IsZooming(Touch touch0, Touch touch1)
    {


        Debug.Log("SUPRESSZOOMING vale: " + supressZooming);
        Debug.Log("SUPRESSTILTING vale: " + supressTilting);


        if (touch0.phase == TouchPhase.Moved & touch1.phase == TouchPhase.Moved &&
                ((touch0.deltaPosition.x > 0 && touch1.deltaPosition.x < 0)
                || (touch0.deltaPosition.x < 0 && touch1.deltaPosition.x > 0)) && 
                !supressZooming)
        {
            Debug.Log("VOY A HACER UN ZOOM");
            supressTilting = true;
            return true;
        }
        return false;
    }

    private bool IsTilting(Touch touch0, Touch touch1)
    {

        Debug.Log("SUPRESSZOOMING vale: " + supressZooming);
        Debug.Log("SUPRESSTILTING vale: " + supressTilting);


        if (touch0.phase == TouchPhase.Moved & touch1.phase == TouchPhase.Moved &&
                ((touch0.deltaPosition.y > 0 && touch1.deltaPosition.y > 0)
                || (touch0.deltaPosition.y < 0 && touch1.deltaPosition.y < 0))
                && !supressTilting
                )
        {
            
            if ((touch0.deltaPosition.x < maxMovementX && touch0.deltaPosition.x > -maxMovementX) || (touch1.deltaPosition.x < maxMovementX && touch1.deltaPosition.x > -maxMovementX))
            {
                Debug.Log("VOY A HACER UN TILTEO");
                supressZooming = true;
                return true;
            }

            supressZooming = true;
            return true;
        }
        return false;
    }


}
