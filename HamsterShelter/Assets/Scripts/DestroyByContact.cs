using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour 
{
    public float Delay;

    public GameObject SpawnOnContact;

    private bool destroyed;

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy();
    }

	void OnTriggerEnter2D(Collider2D other)
    {
        Destroy();
    }

    private void Destroy()
    {
        if (destroyed) return;

        destroyed = true;

        if (SpawnOnContact != null)
        {
            Instantiate(SpawnOnContact, transform.position, transform.rotation);
        }

		Destroy(gameObject, Delay);
	}

}
