using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]

public class DraggableObject : MonoBehaviour
{
    private static GameObject DraggedObject;

    public Counter Counter;

	public float GridSize = 0.3f;

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
        if (DraggedObject == null) StartDragging();
    }
	
    /// <summary>
    /// Disables the rigidbody and collider attached to the object (if there are any)
    /// </summary>
    public void StartDragging()
    {
        if (MeteorRain.Instance != null && MeteorRain.Instance.HasStarted) return;

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
        if (collider != null) collider.enabled = false;

        DraggedObject = this.gameObject;
        draggingStartPos = transform.position;
    }

    /// <summary>
    /// re-enables the rigidbody and collider attached to the object
    /// </summary>
    public void StopDragging()
    {
        if (rigidBody != null)
        {
            rigidBody.isKinematic = false;
            rigidBody.gravityScale = prevGravityScale;
        }
        if (collider != null) collider.enabled = true;

        //if couldn't place the object at the specific position, delete it and decrease the counter
        if (!IsPlaceablePosition(transform.position))
        {
            if (Counter != null)
            {
                Counter.Count--;
                Destroy(gameObject);
            }
            else
            {
                transform.position = draggingStartPos;
                renderer.color = Color.white;
            }
        }

        DraggedObject = null;
    }

    void Update()
    {
        if (DraggedObject != this.gameObject) return;

        if (Input.GetMouseButton(0) && (MeteorRain.Instance == null || !MeteorRain.Instance.HasStarted))
        {
            Vector3 dragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragPos.z = 0;

            bool isPlaceable = IsPlaceablePosition(dragPos);

            if (renderer != null)
            {
                renderer.color = isPlaceable ? Color.white : Color.red;
            }

            //align the object to the grid
            dragPos.x = Mathf.Round(dragPos.x / GridSize) * GridSize;
            dragPos.y = Mathf.Round(dragPos.y / GridSize) * GridSize;

            gameObject.transform.position = dragPos;
        }
        else
        {
            StopDragging();
        }
    }

    private bool IsPlaceablePosition(Vector3 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero, 1.0f, LayerMask.GetMask("PlaceableArea"));

        return hit.collider != null;
    }
}