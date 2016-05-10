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

    //clickOffset if the selected object has not been dragged from the middle of the object
    private Vector3 clickOffset = new Vector3(0,0,0);

    //Toggle to false is snapping is disabled
    public static bool UseSnapping = true;

    public Counter Counter;

	public float GridSize = 0.3f;
    private float LerpSpeed = 0.2f;

    private Vector3 offset;

    private Rigidbody2D rigidBody;
    private Collider2D collider;
    private SpriteRenderer spriterenderer;

    private Color originalRendererColor;
    private bool wallSelectionStatus;


    //used for storing the gravity scale because it's set to 0 while dragging
    private float prevGravityScale;

    private Vector3 draggingStartPos;

    void Start()
    {
        rigidBody   = GetComponent<Rigidbody2D>();
        collider    = GetComponent<Collider2D>();
        spriterenderer    = GetComponent<SpriteRenderer>();
    }

    public void OnMouseDown()
    {
        if (DraggedObject == null)
        {
            StartDragging();
            //Determine offset
            float diffx = this.collider.bounds.center.x - Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            float diffy = this.collider.bounds.center.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            clickOffset = new Vector3(diffx, diffy, 0);
        }
    }
	
    /// <summary>
    /// Disables the rigidbody and collider attached to the object (if there are any)
    /// </summary>
    public void StartDragging()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        if (GetComponent<WallScript>() != null) wallSelectionStatus = GetComponent<WallScript>().IsSelected();
        Dragging = true;
        if (spriterenderer != null) originalRendererColor = spriterenderer.color;
        else originalRendererColor = Color.white;

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
            if (WallScript.SelectedWall == DraggedObject) spriterenderer.color = originalRendererColor;
            else spriterenderer.color = Color.white;
        }

        //If the object should be snapped to position
        if(UseSnapping && GetComponent<WallScript>() != null) SnapToPos();

        if (rigidBody != null)
        {
            if(!GetComponent<WallScript>()) rigidBody.isKinematic = false;
            rigidBody.gravityScale = prevGravityScale;
        }

        if (collider != null) collider.isTrigger = false;
        //If the wall was selected prior to dragging, set it back as selected since clicking has unselected it
        if (GetComponent<WallScript>() != null && !wallSelectionStatus
            && Vector3.Distance(transform.position, draggingStartPos) > 0.05f)
        {
            GetComponent<WallScript>().SelectWall(DraggedObject.gameObject);
        }

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

            if (spriterenderer != null)
            {
                spriterenderer.color = isPlaceable ? originalRendererColor : Color.red;
            }

            //align the object to the grid
            dragPos.x = (Mathf.Floor(dragPos.x / GridSize)+0.5f) * GridSize;
            dragPos.y = (Mathf.Floor(dragPos.y / GridSize)+0.5f) * GridSize;
            
            gameObject.transform.position = dragPos + clickOffset;
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
    }

    private void Snap(GameObject thisMagnet, GameObject otherMagnet) {

        //Align the two magnets to be next to each other
        float diffx = thisMagnet.transform.position.x - otherMagnet.transform.position.x;
        float diffy = thisMagnet.transform.position.y - otherMagnet.transform.position.y;
        Vector3 newPos = new Vector3(transform.position.x - diffx, transform.position.y - diffy, -1);
        //TODO: Check that the new position of the block is legal!
        transform.position = newPos;
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