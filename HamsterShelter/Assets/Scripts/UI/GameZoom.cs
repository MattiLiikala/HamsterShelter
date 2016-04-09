using UnityEngine;
using System.Collections;

public class GameZoom : MonoBehaviour {

    //If set to true, this scene can be zoomed by pinching
    public bool ZoomableScene = true;
    public float ZoomSpeed = 0.03f;
    public float PanSpeed = 0.1f;
    public Camera ZoomCamera;

    private float maxZoom;
    private bool isZoomed = false;

    private double minX, maxX, minY, maxY;
    //TODO: Setup map size programmatically
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
        maxZoom = ZoomCamera.orthographicSize;
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
        Debug.Log(GameObject.Find("Tausta2").GetComponent<Renderer>().bounds.size.x);
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
                ZoomCamera.orthographicSize = Mathf.Max(ZoomCamera.orthographicSize, 3f);
                ZoomCamera.orthographicSize = Mathf.Min(ZoomCamera.orthographicSize, maxZoom);
                CalculateBounds();
                //Set the isZoomed boolean
                if (ZoomCamera.orthographicSize == maxZoom) isZoomed = false;
                else isZoomed = true;
            }
            //If the scene is zoomed in and nothing is being dragged, enable moving in scene by touch
            if(Input.touchCount == 1 && isZoomed && !DraggableObject.Dragging)
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
