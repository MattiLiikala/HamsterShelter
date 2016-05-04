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

	}

    public List<GameObject> GetMagnets()
    {
        return snappingMagnets;
    }


    public List<GameObject> GetNearbyMagnets(GameObject anotherMagnet)
    {
        List<GameObject> nearMagnets = new List<GameObject>();
        Collider2D[] nearColliders = Physics2D.OverlapCircleAll(anotherMagnet.transform.position, 0.3f);
        foreach(Collider2D c in nearColliders)
        {
            if(c.tag == "SnappingMagnet" && c.gameObject != anotherMagnet && !snappingMagnets.Contains(c.gameObject))
            {
                nearMagnets.Add(c.gameObject);
            }
        }
        return nearMagnets;
    }
}
