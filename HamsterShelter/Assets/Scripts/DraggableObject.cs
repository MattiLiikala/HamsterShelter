using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider2D))]

public class DraggableObject : MonoBehaviour
{
    private static GameObject DraggedObject;
    //Boolean for checking whether or not any object is being dragged
    public static bool Dragging;
    //If object is colliding while being dragged (i.e. in non-placeable position) illegalCollision = true
    private int illegalCollisions = 0;
    //Toggled when object collides with the trash bin object
    private bool binCollision = false;

    public Counter Counter;

	public float GridSize = 0.3f;
    private float LerpSpeed = 0.2f;

    private Vector3 offset;

    private Rigidbody2D rigidBody;
    private Collider2D collider;
    private SpriteRenderer renderer;
 

    //used for storing the gravity scale because it's set to 0 while dragging
    private float prevGravityScale;

    private Vector3 draggingStartPos;

    void Start()
    {
        rigidBody   = GetComponent<Rigidbody2D>();
        collider    = GetComponent<Collider2D>();
        renderer    = GetComponent<SpriteRenderer>();
    }

    public void OnMouseDown()
    {
        if (DraggedObject == null)
        {
            StartDragging();
        }
    }
	
    /// <summary>
    /// Disables the rigidbody and collider attached to the object (if there are any)
    /// </summary>
    public void StartDragging()
    {
        Dragging = true;

		if (GameManager.Instance.isPaused == true)
			return;
        if (MeteorRain.Instance != null && MeteorRain.Instance.HasStarted) return;
        illegalCollisions = 0;
        //if dragging was started right after instantiating the object, 
        //the Start-method hasn't been called yet and we need to do it manually here:
        if (rigidBody==null && collider==null)
        {
            Start();
        }

        if (rigidBody != null)
        {
            prevGravityScale = rigidBody.gravityScale;

            rigidBody.isKinematic = true;
            rigidBody.gravityScale = 0.0f;
        }
        if (collider != null) collider.isTrigger = true;


        DraggedObject = this.gameObject;
        draggingStartPos = transform.position;
    }

    /// <summary>
    /// re-enables the rigidbody and collider attached to the object
    /// </summary>
    public void StopDragging()
    {
        Dragging = false;

        if (!IsPlaceablePosition(transform.position))
        {
            //If the object is a building block outside of placeablea area, delete it
            if(GetComponent<WallScript>() != null && !IsPlaceableArea(transform.position))
            {
                Destroy(DraggedObject);
                Counter.Count--;
                return;
            }
            //If the original pos is unplaceable, destroy object (i.e. the object was just created)
            if(GetComponent<WallScript>() != null && !IsPlaceableArea(draggingStartPos))
            {
                Destroy(DraggedObject);
                Counter.Count--;
                return;
            }

            //If in nonplaceable position, return the object to its original position
            StartCoroutine(LerpObjectPosition(draggingStartPos));
            renderer.color = Color.white;
        }

        //If the object should be snapped to position
        SnapToPos();

        if (rigidBody != null)
        {
            if(!GetComponent<WallScript>()) rigidBody.isKinematic = false;
            rigidBody.gravityScale = prevGravityScale;
        }

        if (collider != null) collider.isTrigger = false;
        DraggedObject = null;
    }

    void Update()
    {
		
        if (DraggedObject != this.gameObject) return;

        if (Input.GetMouseButton(0) && (MeteorRain.Instance == null || !MeteorRain.Instance.HasStarted))
        {
            Vector3 dragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragPos.z = -1;

            bool isPlaceable = IsPlaceablePosition(dragPos);

            if (renderer != null)
            {
                renderer.color = isPlaceable ? Color.white : Color.red;
            }

            //align the object to the grid
            dragPos.x = (Mathf.Floor(dragPos.x / GridSize)+0.5f) * GridSize;
            dragPos.y = (Mathf.Floor(dragPos.y / GridSize)+0.5f) * GridSize;
            
            gameObject.transform.position = dragPos;
        }
        else
        {
            StopDragging();
        }
    }

    private void SnapToPos()
    {
        SnappingObject snappingScript = GetComponent<SnappingObject>();
        float minDist = 1000;
        GameObject magnet1 = null, magnet2 = null;
        //Find the two nearest magnets
        foreach(GameObject m1 in snappingScript.GetMagnets())
        {
            foreach(GameObject m2 in snappingScript.GetNearbyMagnets(m1))
            {
                float dist = Vector3.Distance(m1.transform.position, m2.transform.position);
                if(dist < minDist)
                {
                    minDist = dist;
                    magnet1 = m1;
                    magnet2 = m2;
                }
            }
        }

        if(magnet1 != null && magnet2 != null)
        {
            Snap(magnet1, magnet2);
        }
        /*if (snappingScript != null && snappingScript.GetMagnets().Count > 0)
        {
            foreach (GameObject magnet in snappingScript.GetMagnets())
            {
                List<GameObject> nearbyMagnets = snappingScript.GetNearbyMagnets(magnet);
                if(nearbyMagnets.Count > 0)
                {
                    //TODO: get the nearest pair of magnets
                    //Snap(magnet, nearbyMagnets[0]);
                    //return;
                }
            }
        }*/
    }

    private void Snap(GameObject thisMagnet, GameObject otherMagnet) {
        /* //Determine which side the othermagnet is on
         Transform otherT = otherMagnet.transform;
         Transform thisT = thisMagnet.transform;
         Bounds ob = otherMagnet.GetComponent<Collider2D>().bounds;
         Bounds tb = thisMagnet.GetComponent<Collider2D>().bounds;
         if (otherT.position.y > thisT.position.y && tb.center.y < ob.center.y - ob.extents.y)
         {
             //Snapping to bottom of top right magnet or top left magnet
             Debug.Log("1!");
             float xdiff = ob.center.x - tb.center.x;
             float ydiff = ob.center.y - tb.center.y;
             transform.position = new Vector3(transform.position.x + xdiff, transform.position.y + ydiff - ob.extents.y * 2, -1);
         }
         else if (otherT.position.y > thisT.position.y)
         {
             //Snapping to sides of top right magnet or top left magnet
             Debug.Log("2!");
             float xdiff;
             if (ob.center.x < tb.center.x) xdiff = (ob.center.x + ob.extents.x) - (tb.center.x - tb.extents.x);
             else xdiff = (ob.center.x - ob.extents.x * 3) - (tb.center.x - tb.extents.x);
             float ydiff = ob.center.y - tb.center.y;
             transform.position = new Vector3(transform.position.x + xdiff, transform.position.y + ydiff, -1);
         }
         else if(otherT.position.y < thisT.position.y)
         {
             //Snapping to bottom right magnet or bottom left magnet
             Debug.Log("3!");
             float xdiff = ob.center.x - tb.center.x;
             float ydiff = ob.center.y - tb.center.y;
             transform.position = new Vector3(transform.position.x + xdiff, transform.position.y + ydiff + ob.extents.y * 2, -1);
         }
         else
         {
             Debug.Log("4!");
         }*/

        //Align the two magnets to be next to each other
        float diffx = thisMagnet.transform.position.x - otherMagnet.transform.position.x;
        float diffy = thisMagnet.transform.position.y - otherMagnet.transform.position.y;
        transform.position = new Vector3(transform.position.x - diffx, transform.position.y - diffy, -1);
    }

    IEnumerator LerpObjectPosition(Vector3 target)
    {
        float elapsed = 0;
        while (elapsed < LerpSpeed)
        {
            transform.position = Vector3.Lerp(transform.position, target, (elapsed / LerpSpeed));
            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    private bool IsPlaceablePosition(Vector3 position)
    {
        //Check if the object is inside the placeable area
        bool inArea = IsPlaceableArea(position);
        return (inArea && illegalCollisions == 0);
    }

    private bool IsPlaceableArea(Vector3 position)
    {
        bool inArea;
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero, 1.0f, LayerMask.GetMask("PlaceableArea"));
        if (hit.collider == null) inArea = false;
        else inArea = true;
        return inArea;
    }

    /// <summary>
    /// If the currently dragged object collides with another object, 
    /// increment illegalCollisions
    /// </summary>
    /// <param name="other"></param>
   void OnTriggerEnter2D(Collider2D other)
    {
        if(this.gameObject == DraggedObject)
        {
            illegalCollisions++;
        }
    }

    /// <summary>
    /// If the currently dragged object collides with another object, 
    /// decrement illegalCollisions
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (this.gameObject == DraggedObject)
        {
            if(illegalCollisions > 0) illegalCollisions--;
        }
    }

    /// <summary>
    /// Get the currently dragged object. Used in SnappingObject.cs
    /// </summary>
    /// <returns></returns>
    public static GameObject GetDraggedObject()
    {
        return DraggedObject;
    }

}