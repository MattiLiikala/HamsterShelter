using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpawnObjectButton : MonoBehaviour, IPointerDownHandler
{

	public GameObject ObjectToSpawn;

    public Vector3 ObjectRotation;

	//public Vector3 spawnPosition;
	public static int SpawnedAmount;
	public int MaxSpawnAmount = 5;

    public void OnPointerDown(PointerEventData eventData)
    {
		if (SpawnedAmount >= MaxSpawnAmount) return;

        Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spawnPosition.z = 0.0f;

		var spawnedObject = (GameObject)Instantiate (ObjectToSpawn, spawnPosition, Quaternion.Euler(ObjectRotation));

        //start dragging the object if it has a DraggableObject-component
        DraggableObject draggable = spawnedObject.GetComponent<DraggableObject>();
        if (draggable != null) draggable.StartDragging();

        SpawnedAmount = SpawnedAmount + 1;
        WallController.increaseCount();
    }
}
