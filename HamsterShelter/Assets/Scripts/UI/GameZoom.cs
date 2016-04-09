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

	// Use this for initialization
	void Start () {
        if (ZoomableScene && ZoomCamera == null)
        {
            //If no camera was set, try default camera
            ZoomCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            if (ZoomCamera == null) throw new System.Exception("Camera for GameZoom not found!");
        }
        maxZoom = ZoomCamera.orthographicSize;
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
                ZoomCamera.orthographicSize = Mathf.Max(ZoomCamera.orthographicSize, 0.1f);
                ZoomCamera.orthographicSize = Mathf.Min(ZoomCamera.orthographicSize, maxZoom);

                //Set the isZoomed boolean
                if (ZoomCamera.orthographicSize == 0.1f || ZoomCamera.orthographicSize == maxZoom) isZoomed = false;
                else isZoomed = true;
            }
            //If the scene is zoomed in, enable moving in scene by touch
            if(Input.touchCount == 1 && isZoomed)
            {
                Vector2 t1 = Input.GetTouch(0).deltaPosition;
                ZoomCamera.transform.Translate(-t1.x * PanSpeed, -t1.y * PanSpeed, 0);
            }
        }
	}
}
