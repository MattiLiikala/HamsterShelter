using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WallController : MonoBehaviour {


	public int total = 5;
	public static int count;
    public static WallController instance;
	Text text;


	void Awake () {
		text = GetComponent<Text> ();
        instance = this;

	}

	// Update is called once per frame
	void Update () {
		text.text = "Blocks used: " + count + "/" + total;
	}

	public void increaseCount() {
		count = count + 1;

	}
		

}