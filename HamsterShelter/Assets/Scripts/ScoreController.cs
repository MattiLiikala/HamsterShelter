using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreController : MonoBehaviour {

	public static int total;
	public static int count;
	Text text;


	void Awake () {
		text = GetComponent<Text> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		text.text = "Hamsters alive: " + count + "/" + total;
	}

	public void increaseCount() {
		count = count + 1;
		increaseTotal ();

	}
	public void increaseTotal() {
		total = total + 1;
	}
	public static void decreaseCount() {
		count = count - 1;
	}

}

