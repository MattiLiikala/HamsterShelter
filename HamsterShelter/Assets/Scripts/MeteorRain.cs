using UnityEngine;
using System.Collections;

public class MeteorRain : MonoBehaviour {

	public GameObject Meteor;

	public Vector3 SpawnPosition, SpawnVelocity;
	public int Amount;
	public float Wait, StartWait;
	
	void Start () {
		StartCoroutine (SpawnWaves());
	}
	
	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds(StartWait);

		for(int i = 0; i < Amount; i++)
		{
			Vector3 spawnPosition = new Vector3 (Random.Range(-10, 5), SpawnPosition.y, SpawnPosition.z);
			var meteor = (GameObject)Instantiate(Meteor, spawnPosition, Quaternion.identity);

            meteor.GetComponent<Rigidbody2D>().velocity = SpawnVelocity;

			yield return new WaitForSeconds(Wait);
		} 
			
	}
	
	
}
