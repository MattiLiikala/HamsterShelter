using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour 
{
    public float Delay;

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject, Delay);
    }

	void OnTriggerEnter2D(Collider2D other) 
    {
		Destroy(gameObject, Delay);
	}
}
