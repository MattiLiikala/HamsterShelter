using UnityEngine;
using System.Collections;

public class WallButton : MonoBehaviour {
	public GameObject wall;
	public Vector3 spawnPosition;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onClick(){

		Instantiate (wall, spawnPosition, Quaternion.identity);
	}
}
