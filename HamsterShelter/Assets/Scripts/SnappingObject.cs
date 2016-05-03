using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnappingObject : MonoBehaviour {

    private List<GameObject> snappingMagnets;

	// Use this for initialization
	void Start () {
        //Get this objects snapping points
        snappingMagnets = new List<GameObject>();
        foreach (Transform t in transform)
        {
            if (t.tag == "SnappingMagnet")
            {
                snappingMagnets.Add(t.gameObject);
            } 
        }
	}
	
	// Update is called once per frame
	void Update () {

        //For each magnet (of a dragged object) find nearby magnets
        if (DraggableObject.GetDraggedObject() == this.gameObject)
        {
            foreach (GameObject go in snappingMagnets)
            {
                List<GameObject> nearMagnets = getNearbyMagnets(go);
            }
        }
	}


    private List<GameObject> getNearbyMagnets(GameObject anotherMagnet)
    {
        List<GameObject> nearMagnets = new List<GameObject>();
        Collider2D[] nearColliders = Physics2D.OverlapCircleAll(anotherMagnet.transform.position, 0.5f);
        foreach(Collider2D c in nearColliders)
        {
            if(c.tag == "SnappingMagnet" && c.gameObject != anotherMagnet)
            {
                nearMagnets.Add(c.gameObject);
            }
        }
        return nearMagnets;
    }
}
