using UnityEngine;
using System.Collections;

public class BackgroundOpacityFade : MonoBehaviour {

	private float opacity;
	private SpriteRenderer o_renderer;

	// Use this for initialization
	void Start () {
		o_renderer = this.GetComponent<SpriteRenderer>();
		opacity = 1f;
	}

	// Update is called once per frame
	void Update () {
		opacity -= Time.deltaTime / 100;
		o_renderer.color = new Color(1f, 1f, 1f, opacity);
	}
}
