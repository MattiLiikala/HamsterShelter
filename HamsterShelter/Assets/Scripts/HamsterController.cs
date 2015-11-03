using UnityEngine;
using System.Collections;

public class HamsterController : MonoBehaviour {

	private bool m_FacingRight = true;
	private Animator animator;
	private float walkspeed = 2.0f;
	float walkingdirection = 1.0f;
	private float wallLeft = -5.0f;
	private float wallRight = 5.0f;
	Vector2 walkAmount;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		walkAmount.x = walkingdirection * walkspeed * Time.deltaTime;

		if (walkingdirection > 0.0f && transform.position.x >= wallRight) {
				walkingdirection = -1.0f;
				Flip ();
		} else if (walkingdirection < 0.0f && transform.position.x <= wallLeft) {
			walkingdirection = 1.0f;
			Flip ();
		}
		transform.Translate(walkAmount);
	}

	private void Flip()
	{
			// Switch the way the player is labelled as facing.
			m_FacingRight = !m_FacingRight;

			// Multiply the player's x local scale by -1.
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
	}
}
