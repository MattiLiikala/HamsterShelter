using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]

public class DraggableObject : MonoBehaviour
{
    private static GameObject DraggedObject;

    const float GridSize = 1.0f;

    private Vector3 offset;

    private Rigidbody2D rigidBody;
    private Collider2D collider;

    //used for storing the gravity scale because it's set to 0 while dragging
    private float prevGravityScale;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
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

        DraggedObject = null;
    }

    void Update()
    {
        if (DraggedObject != this.gameObject) return;

        if (Input.GetMouseButton(0))
        {
            Vector3 dragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragPos.z = 0;

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
}