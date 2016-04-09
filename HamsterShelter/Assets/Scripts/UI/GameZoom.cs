using UnityEngine;
using System.Collections;

public class GameZoom : MonoBehaviour {

    //If set to true, this scene can be zoomed by pinching
    public bool ZoomableScene = true;
    public float ZoomSpeed = 0.03f;
    public float PanSpeed = 0.05f;
    public float MaxZoom = 2.8f;
    public Camera ZoomCamera;

    private float origZoom;
    private bool isZoomed = false;

    //Max bounds for camera panning
    private double minX, maxX, minY, maxY;
    private float mapX;
    private float mapY;

	// Use this for initialization
	void Start () {
        if (ZoomableScene && ZoomCamera == null)
        {
            //If no camera was set, try default camera
            ZoomCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            if (ZoomCamera == null) throw new System.Exception("Camera for GameZoom not found!");
        }
        origZoom = ZoomCamera.orthographicSize;
        //Set up bounds according to "Tausta"
        GameObject tausta;
        tausta = GameObject.Find("Tausta");
        //since the name is different in one scene...
        if (tausta == null) tausta = GameObject.Find("Tausta2");
        mapX = tausta.GetComponent<Renderer>().bounds.size.x;
        mapY = tausta.GetComponent<Renderer>().bounds.size.y;
        CalculateBounds();
	}
	
	// Update is called once per frame
	void Update () {
        if (ZoomableScene)
        {
            if (Input.touchCount == 2)
            {
                Touch t1 = Input.GetTouch(0);
                Touch t2 = Input.GetTouch(1);

                //Get the distances of touches
                Vector2 t1prev = t1.position - t1.deltaPosition;
                Vector2 t2prev = t2.position - t2.deltaPosition;
                float prevMag = (t1prev - t2prev).magnitude;
                float currMag = (t1.position - t2.position).magnitude;

                //Get the distance difference between last frames
                float delta = prevMag - currMag;

                ZoomCamera.orthographicSize += delta * ZoomSpeed;
                ZoomCamera.orthographicSize = Mathf.Max(ZoomCamera.orthographicSize, MaxZoom);
                ZoomCamera.orthographicSize = Mathf.Min(ZoomCamera.orthographicSize, origZoom);
                CalculateBounds();
                //Set the isZoomed boolean
                if (ZoomCamera.orthographicSize == origZoom) isZoomed = false;
                else isZoomed = true;

                //After the camera has zoomed, check that the bounds have not been reached
                Vector3 newPos = ZoomCamera.transform.position;
                newPos.x = Mathf.Clamp(newPos.x, (float)minX, (float)maxX);
                newPos.y = Mathf.Clamp(newPos.y, (float)minY, (float)maxY);
                ZoomCamera.transform.position = newPos;
            }
            //If nothing is being dragged, enable moving in scene by touch
            if(Input.touchCount == 1 && !DraggableObject.Dragging)
            {
                Vector2 t1 = Input.GetTouch(0).deltaPosition;
                Vector3 newPos = new Vector3(ZoomCamera.transform.localPosition.x - t1.x * PanSpeed, 
                    ZoomCamera.transform.localPosition.y-t1.y * PanSpeed,
                    ZoomCamera.transform.localPosition.z);
                ZoomCamera.transform.localPosition = newPos;
                Vector3 clamped = ZoomCamera.transform.position;
                clamped.x = Mathf.Clamp(clamped.x, (float)minX, (float)maxX);
                clamped.y = Mathf.Clamp(clamped.y, (float)minY, (float)maxY);
                ZoomCamera.transform.position = clamped;
            }
        }
	}

    void CalculateBounds()
    {
        var vertExtent = ZoomCamera.orthographicSize;
        var horzExtent = vertExtent * Screen.width / Screen.height;

        //Calculations assume map is position at the origin
        minX = horzExtent - mapX / 2.0;
        maxX = mapX / 2.0 - horzExtent;
        minY = vertExtent - mapY / 2.0;
        maxY = mapY / 2.0 - vertExtent;
    }
}
