using UnityEngine;
using System.Collections;

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
        if (rigidBody != null)
        {
            rigidBody.isKinematic = false;
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


}