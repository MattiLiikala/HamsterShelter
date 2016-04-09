using UnityEngine;
using System.Collections;

public class GameZoom : MonoBehaviour {

    //If set to true, this scene can be zoomed by pinching
    public bool ZoomableScene = true;
    public float ZoomSpeed = 0.5f;
    public Camera ZoomCamera;

	// Use this for initialization
	void Start () {
        if (ZoomableScene && ZoomCamera == null)
        {
            //If no camera was set, try default camera
            ZoomCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            if (ZoomCamera == null) throw new System.Exception("Camera for GameZoom not found!");
        }
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
            }
        }
	}
}
