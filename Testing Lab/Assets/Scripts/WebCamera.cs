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
    public float minZoom = 0.8f;
    public float maxZoom = 7.5f;
    public float minRotation = 0.8f;
    public float maxRotation = 0.8f;
    public float smoot = 0.04f;
    public LayerMask terrainLayer;
    private bool editorScanOldRay = true;
    private Vector3 editorOldRayPos;
    private bool firstExecution = true;
    private bool rotating = false;
    private float rayCastDistance = 100.0f;
    private BoxCollider cameraCollider;


    void Start()
    {
        cameraCollider = GetComponent<BoxCollider>();
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

        // Debugging Line
        if (Physics.Raycast(_XForm_Parent.position, -Vector3.up, out downHit, terrainLayer))
        {

            offsetDistance = downHit.distance;
            Debug.DrawLine(transform.position, downHit.point, Color.red);
        }

        
        //Rotation of the Camera based on Mouse Coordinates
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
        {
            rotating = true;

            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                //Debug.Log($"X: {Input.GetAxis("Mouse X")}");

                _LocalRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
                _LocalRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitivity;

                //Clamp Y rotation to horizon and not flipping over at the top
                if (_LocalRotation.y < 10f)
                    _LocalRotation.y = 10f;
                else if (_LocalRotation.y > 90f)
                    _LocalRotation.y = 90f;

               firstExecution = false;
            }

            Quaternion QT;
            QT = Quaternion.Euler(_XForm_Parent.rotation.y, _XForm_Parent.rotation.x, 0);

            if (!firstExecution)
            {
                //Debug.Log("CASO INICIAL DE QUATERNION");
                QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
                _XForm_Parent.rotation = Quaternion.Lerp(_XForm_Parent.rotation, QT, Time.deltaTime * OrbitDampening);

                Vector3 actualRotation = _XForm_Parent.rotation.eulerAngles;
                actualRotation.y = Mathf.Clamp(actualRotation.y, 10f, 90f);
                //_XForm_Parent.rotation = Quaternion.Euler(actualRotation);

                //_XForm_Parent.eulerAngles = new Vector3(0, Mathf.Clamp(_XForm_Parent.rotation.y, 5f,90f), 0);
                //Debug.Log("Rotación del padre : " + _XForm_Parent.rotation.eulerAngles);
                //Debug.Log("Transform.position tras ROTAR: " + transform.position);
                //_XForm_Parent.localPosition = new Vector3(transform.localPosition.x, Mathf.Clamp(transform.localPosition.y, minZoom, maxZoom), transform.localPosition.z);

            }

        }
        else
        {
            rotating = false;
        }


        // Actual camera rig transformations 

        

              
        if (!rotating)
        {

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
                    /*
                    Debug.Log("HIT_POINT SIN ROTAR: " + hit.point);
                    Debug.Log("DELTA_RAY_POSITION SIN ROTAR: " + deltaRayPosition);
                    Debug.Log("EDITOR_OLD_RAY_POS SIN ROTAR: " + editorOldRayPos);
                    */
                    _XForm_Parent.Translate(-deltaRayPosition * smoot * panSpeed, Space.World);
                    //transform.position = new Vector3(Mathf.Clamp(transform.position.x, minXpos, maxXpos), 0, Mathf.Clamp(transform.position.z, minYpos, maxYpos));      //Locking position of camera, that you can drag out of map

                }


            }

            if (Input.GetMouseButtonUp(0))          //Called when user release left mouse button
            {
                editorScanOldRay = true;                    //We will need to scan oldRay position after again
                editorOldRayPos = Vector3.zero;     //Resets oldRayPosition
            }




            // Zooming input from our mouse scroll wheel
            /*
            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                float scrollamount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitvity;

                scrollamount *= (_CameraDistance * 0.8f);

                _CameraDistance += scrollamount * -1f;

                _CameraDistance = Mathf.Clamp(_CameraDistance, 0.8f, 10f);
            }*/

            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                // Detach pivot from Camera
                this.transform.parent = null;

                RaycastHit hit;
                Ray ray = this.transform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                Vector3 desiredPosition;

                if (Physics.Raycast(ray, out hit, rayCastDistance, terrainLayer))
                {
                    desiredPosition = hit.point;
                }
                else
                {
                    desiredPosition = transform.position;
                }
                float distance = Vector3.Distance(desiredPosition, transform.position);
                Vector3 direction = Vector3.Normalize(desiredPosition - transform.position) * (distance * Input.GetAxis("Mouse ScrollWheel"));

                if ((transform.position.y + direction.y) <= minZoom || (transform.position.y + direction.y) >= maxZoom)
                {
                    direction.z = 0f;
                    direction.x = 0f;
                }

                transform.position += direction;
                

                //Vector3 zClamp = new Vector3(0f,0f,Mathf.Clamp(transform.position.z, minZoom, maxZoom));
                //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, Mathf.Clamp(transform.localPosition.z, minZoom, maxZoom));
                transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, minZoom, maxZoom), transform.position.z);
                //Debug.Log("Fijando posición en Y tras zoom. Posición: " + transform.localPosition.y + "Valor de Math.Clamp(): " + Mathf.Clamp(transform.localPosition.y, minZoom, maxZoom));
                // Update camera pivot to new position on the map
                Vector3 pivotSpawnPoint;

                if (Physics.Raycast(_XForm_Camera.position, _XForm_Camera.forward, out hit, rayCastDistance, terrainLayer))
                {
                    pivotSpawnPoint = hit.point;
                    _XForm_Parent.transform.position = pivotSpawnPoint;
                }
              
                this.transform.parent = this._XForm_Parent;
                               

            }


            if (_XForm_Camera.localPosition.z != _CameraDistance * -1f)
            {
                //Debug.Log("Posicionando cámara en el último IF");
                //_XForm_Camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(_XForm_Camera.localPosition.z, _CameraDistance * -1f, Time.deltaTime * ScrollDampening));
            }


        }



        
    }

}