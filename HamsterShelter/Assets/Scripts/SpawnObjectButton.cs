using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpawnObjectButton : MonoBehaviour, IPointerDownHandler
{
    //a counter which is increased when pressing the button
    public Counter Counter;

	public GameObject ObjectToSpawn;

    public Vector3 ObjectRotation;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Counter != null)
        {
            if (Counter.Count >= Counter.Total) return;
        }

        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPosition.z = 0.0f;

		var spawnedObject = (GameObject)Instantiate (ObjectToSpawn, spawnPosition, Quaternion.Euler(ObjectRotation));

        //start dragging the object if it has a DraggableObject-component
        DraggableObject draggable = spawnedObject.GetComponent<DraggableObject>();
        if (draggable != null)
        {
            draggable.Counter = Counter;
            draggable.StartDragging();
        }

        if (Counter != null) Counter.Count++;
    }
}
