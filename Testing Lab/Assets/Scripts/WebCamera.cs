using UnityEngine;
using System.Collections;

public class WebCamera : MonoBehaviour
{

    protected Camera camera;

    protected Transform _XForm_Camera;
    protected Transform _XForm_Parent;

    protected Vector3 _LocalRotation;
    protected float _CameraDistance = 10f;

    public float panSpeed = 4.0f;
    public float MouseSensitivity = 4f;
    public float ScrollSensitvity = 2f;
    public float OrbitDampening = 10f;
    public float ScrollDampening = 6f;
    public float smoot = 0.04f;
    public LayerMask terrainLayer;
    private bool editorScanOldRay = true;
    private Vector3 editorOldRayPos;

    public bool CameraDisabled = false;


    // 
    void Start()
    {
        this._XForm_Camera = this.transform;
        this._XForm_Parent = this.transform.parent;
        camera = GetComponent<Camera>();
    }
    
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            CameraDisabled = !CameraDisabled;
        }

        if (!CameraDisabled)
        {

            if (Input.GetMouseButton(0))            //Called while user holdin left mouse button
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);    //New ray from middle of camera to position of mouse
                RaycastHit hit;                                         //In hit will be stored informations about object that ray hit

                //Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);   //Visualization of 

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, terrainLayer))     //Called if infinity long ray hit something what is layer of terrainLayer 
                {
                    if (editorScanOldRay)   //This will be called once you click, to save your old ray hit position needed for calculating deltaPosition
                    {
                        editorOldRayPos = hit.point;    //Store actual ray hit position in oldRayPos variable
                        editorScanOldRay = false;       //scan old ray will be no more needed so it will turn it self off
                    }

                    Vector3 deltaRayPosition = hit.point - editorOldRayPos; //Calculation of how much did ray move (deltaPosition)
                    deltaRayPosition.y = 0f;                                //We dont want to move camera on Y axes, so we will 0 value the Y coordinate

                    _XForm_Parent.Translate(-deltaRayPosition * smoot * panSpeed, Space.World);
                    //transform.position = new Vector3(Mathf.Clamp(transform.position.x, minXpos, maxXpos), 0, Mathf.Clamp(transform.position.z, minYpos, maxYpos));      //Locking position of camera, that you can drag out of map

                }

            }

            if (Input.GetMouseButtonUp(0))          //Called when user release left mouse button
            {
                editorScanOldRay = true;                    //We will need to scan oldRay position after again
                editorOldRayPos = Vector3.zero;     //Resets oldRayPosition
            }


            //Rotation of the Camera based on Mouse Coordinates
            if (Input.GetAxis("Mouse X") != 0 && Input.GetMouseButton(1) || Input.GetAxis("Mouse Y") != 0 && Input.GetMouseButton(1))﻿
            {
                
                _LocalRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
                _LocalRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitivity;

                //Clamp the y Rotation to horizon and not flipping over at the top
                if (_LocalRotation.y < 0f)
                    _LocalRotation.y = 0f;
                else if (_LocalRotation.y > 90f)
                    _LocalRotation.y = 90f;
            }
            //Zooming Input from our Mouse Scroll Wheel
            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitvity;

                ScrollAmount *= (_CameraDistance * 0.3f);

                _CameraDistance += ScrollAmount * -1f;

                _CameraDistance = Mathf.Clamp(_CameraDistance, 0.5f, 10f);
            }
        }
        
        //Actual Camera Rig Transformations       
        Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
       _XForm_Parent.rotation = Quaternion.Lerp(_XForm_Parent.rotation, QT, Time.deltaTime * OrbitDampening);
            
        
        if (_XForm_Camera.localPosition.z != _CameraDistance * -1f)
        {
            _XForm_Camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(_XForm_Camera.localPosition.z, _CameraDistance * -1f, Time.deltaTime * ScrollDampening));
        }


    }
}