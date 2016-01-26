﻿using UnityEngine;
using System.Collections;

public class MeteorRain : MonoBehaviour {

	public GameObject Meteor;

	public Vector2 SpawnAreaSize, SpawnVelocity;
	public int Amount;
	public float Wait, StartWait;
	
	void Start () {
		StartCoroutine (SpawnWaves());
	}
	
	IEnumerator SpawnWaves () {
		yield return new WaitForSeconds(StartWait);

		for(int i = 0; i < Amount; i++)
		{
			Vector3 spawnPosition = 
                new Vector3(
                    transform.position.x + Random.Range(-0.5f,0.5f) * SpawnAreaSize.x,
                    transform.position.y + Random.Range(-0.5f, 0.5f) * SpawnAreaSize.y, 
                    0.0f);

			var meteor = (GameObject)Instantiate(Meteor, spawnPosition, Quaternion.identity);

            meteor.GetComponent<Rigidbody2D>().velocity = SpawnVelocity;

			yield return new WaitForSeconds(Wait);
		} 			
	}

    void OnDrawGizmos() {
        Gizmos.color = Color.red;

        //visualize the spawn area
        Gizmos.DrawWireCube(transform.position, new Vector3(SpawnAreaSize.x, SpawnAreaSize.y, 0.0f));

        //draw a line showing the initial direction of the meteors
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(SpawnVelocity.x, SpawnVelocity.y, 0.0f));
    }
	
	
}
