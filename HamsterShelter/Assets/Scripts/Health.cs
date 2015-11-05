using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int health;
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Hazard") {
			
			if (health == 0) {
				Destroy(gameObject);
			}
			else {
				health = health - 1;
			}
		}
		
	}
}