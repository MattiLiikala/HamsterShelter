using UnityEngine;
using System.Collections;

public class WallButton : MonoBehaviour {
	public GameObject wall;
	public Vector3 spawnPosition;
	public static int maara;
	public int max = 5;
	

	public void onClick(){
		if (maara < max) {
		Instantiate (wall, spawnPosition, Quaternion.identity);
		maara = maara + 1;
		WallController.increaseCount ();
	}
}
}
