using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {

	public int durability;
	
	void OnTriggerEnter2D(Collider2D other) {
		if (durability > 0) {
			durability = durability - 1;
		}
		else {
		Destroy(gameObject);
		}
	}
}
