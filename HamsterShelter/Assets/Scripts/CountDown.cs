using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {

	float timeRemaining = 60;
	public Text timeLeft;

	// Use this for initialization
	void Start () {
		timeLeft = GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		timeRemaining -= Time.deltaTime;
		timeLeft.text = "Time Remaining: " + (int)timeRemaining;

	}


}