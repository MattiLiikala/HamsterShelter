using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WallController : MonoBehaviour {


	public static int total = 5;
	public static int count;
	Text text;


	void Awake () {
		text = GetComponent<Text> ();

	}

	// Update is called once per frame
	void Update () {
		text.text = "Blocks used: " + count + "/" + total;
	}

	public static void increaseCount() {
		count = count + 1;

	}
		

}