using UnityEngine;
using System.Collections;

public class SpawnButton : MonoBehaviour {

	public GameObject hamsteri;
	public Vector3 spawnPosition;


	public void onClick(){

		Instantiate (hamsteri, spawnPosition, Quaternion.identity);
	}
}