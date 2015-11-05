using UnityEngine;
using System.Collections;

public class MeteorRain : MonoBehaviour {

	public GameObject meteori;
	public Vector3 spawnValue;
	public int amount;
	public float wait;
	public float startWait;
	
	void Start () {
		StartCoroutine (SpawnWaves());
	}
	
	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds(startWait);
		for(int i = 0; i < amount; i++)
		{
			Vector3 spawnPosition = new Vector3 (Random.Range(-10, 5), spawnValue.y, spawnValue.z);
			Instantiate(meteori, spawnPosition, Quaternion.identity);
			yield return new WaitForSeconds(wait);
		} 
			
	}
	
	
}
