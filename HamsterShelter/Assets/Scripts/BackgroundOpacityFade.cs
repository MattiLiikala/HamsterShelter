using UnityEngine;
using System.Collections;

public class BackgroundOpacityFade : MonoBehaviour
{
	private new SpriteRenderer renderer;

	// Use this for initialization
	void Start ()
    {
		renderer = this.GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update ()
    {
		renderer.color = new Color(1f, 1f, 1f, MeteorRain.Instance.WaitTimer / MeteorRain.Instance.WaitDuration);
	}
}
