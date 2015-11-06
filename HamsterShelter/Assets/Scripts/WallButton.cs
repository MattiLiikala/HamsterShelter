using UnityEngine;
using System.Collections;

public class WallButton : MonoBehaviour {
	public GameObject wall;
	public Vector3 spawnPosition;
	

	public void onClick(){

		Instantiate (wall, spawnPosition, Quaternion.identity);
	}
}
