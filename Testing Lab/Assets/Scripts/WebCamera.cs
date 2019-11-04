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
    private bool firstExecution = true;


    void Start()
    {
        this._XForm_Camera = this.transform;
        this._XForm_Parent = this.transform.parent;
        _LocalRotation.x = 0;
        _LocalRotation.y = 0;
        //Debug.Log("Nombre del objeto padre durante START() --->  X: " + _XForm_Parent.name);
        //Debug.Log("Coordenadas de rotación del objeto durante START() --->  X: " + _XForm_Parent.rotation.eulerAngles.x + " Y: " + _XForm_Parent.rotation.y);
        //Debug.Log("Rotación de _LocalRotation durante START() --->  X: " + _LocalRotation.x + " Y: " + _LocalRotation.y);
        camera = GetComponent<Camera>();
    }
    
    void LateUpdate()
    {
        RaycastHit downHit;
        float offsetDistance;

        if (Physics.Raycast(_XForm_Parent.position, -Vector3.up, out downHit, terrainLayer))
        {

            offsetDistance = downHit.distance;
            Debug.DrawLine(transform.position, downHit.point, Color.red);
        }




        if (Input.GetMouseButton(0))            //Called while user holdin left mouse button
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);    //New ray from middle of camera to position of mouse
            RaycastHit hit;                                         //In hit will be stored informations about object that ray hit

            //Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);   //Visualization of debugging ray

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
        if (Input.GetAxis("Mouse X") != 0 && Input.GetMouseButton(1) || Input.GetAxis("Mouse Y") != 0 && Input.GetMouseButton(1))
        {

            _LocalRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
            _LocalRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitivity;

            //clamp the y rotation to horizon and not flipping over at the top
            if (_LocalRotation.y < 2f)
                _LocalRotation.y = 2f;
            else if (_LocalRotation.y > 90f)
                _LocalRotation.y = 90f;

            firstExecution = false;


        }

        // Zooming input from our mouse scroll wheel
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            float scrollamount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitvity;

            scrollamount *= (_CameraDistance * 0.8f);

            _CameraDistance += scrollamount * -1f;

            _CameraDistance = Mathf.Clamp(_CameraDistance, 0.8f, 10f);
        }


        // Actual camera rig transformations 
        //Debug.Log("Coordenadas de rotación del objeto antes de la transformación --->  X: " + _XForm_Parent.rotation.eulerAngles.x + " Y: " + _XForm_Parent.rotation.y);
        //Debug.Log("Coordenadas de _localRotation antes de la transformación --->  X: " + _LocalRotation.x + " Y: " + _LocalRotation.y);

        Quaternion QT;
        QT = Quaternion.Euler(_XForm_Parent.rotation.y, _XForm_Parent.rotation.x, 0);

        if (!firstExecution) 
        {
            //Debug.Log("CASO INICIAL DE QUATERNION");
            QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
            _XForm_Parent.rotation = Quaternion.Lerp(_XForm_Parent.rotation, QT, Time.deltaTime * OrbitDampening);
            
        }

        //Debug.Log("QUATERNION X: " + QT.x);
        //Debug.Log("QUATERNION Y: " + QT.eulerAngles.y);

        //Debug.Log("Coordenadas de _localRotation después de la transformación --->  X: " + _LocalRotation.x + " Y: " + _LocalRotation.y);
        //Debug.Log("Coordenadas de rotación después de la transformación --->  X: " + _XForm_Parent.rotation.eulerAngles.x + " Y: " + _XForm_Parent.rotation.eulerAngles.y);
        //Debug.Log("Coordenadas de _localRotation después de la transformación --->  X: " + _LocalRotation.x + " Y: " + _LocalRotation.y);

        if (_XForm_Camera.localPosition.z != _CameraDistance * -1f)
        {
            //Debug.Log("Posicionando cámara en el último IF");
            _XForm_Camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(_XForm_Camera.localPosition.z, _CameraDistance * -1f, Time.deltaTime * ScrollDampening));
        }


    }
}