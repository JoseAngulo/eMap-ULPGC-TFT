using UnityEngine;

public class CameraMobile : MonoBehaviour {

    public float speed = 0.01f;
    public float zoomModifier = 2f;
    public float minZoom = 10.0f;
    public float maxZoom = 50.0f;

	void Update () {
		
        if(Input.touchCount == 2)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                Camera.main.transform.eulerAngles += new Vector3(-touchDeltaPosition.y, 0, 0);
            }
        }
        
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -5.0f, 5.0f),
            Mathf.Clamp(transform.position.y, 2.0f, 8.0f),
            Mathf.Clamp(transform.position.z, -7.0f, 7.0f)
        );

        
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            float deltaMagnitudDiff = prevTouchDeltaMag - touchDeltaMag;

            float zoom = deltaMagnitudDiff * zoomModifier * Time.deltaTime;

            Camera.main.transform.Translate(0,zoom,zoom);

            Camera.main.fieldOfView += zoom;
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, minZoom, maxZoom);

        }

	}
}
