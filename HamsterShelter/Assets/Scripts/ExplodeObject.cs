using UnityEngine;
using System.Collections;

public class ExplodeObject : MonoBehaviour
{
    public float MinImpulse, MaxImpulse;

	// Use this for initialization
	void Start ()
    {
        var rigidBodies = GetComponentsInChildren<Rigidbody2D>();
        
        foreach (Rigidbody2D rigidBody in rigidBodies)
        {
            Vector2 impulse = new Vector2(
                rigidBody.position.x - transform.position.x, 
                rigidBody.position.y - transform.position.y);

            impulse = impulse.normalized;
            impulse.x *= Random.Range(MinImpulse, MaxImpulse);
            impulse.y *= Random.Range(MinImpulse, MaxImpulse);

            rigidBody.AddForce(impulse, ForceMode2D.Impulse);
        }
	}
}
