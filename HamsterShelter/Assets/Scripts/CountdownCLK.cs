using UnityEngine;
using System.Collections;

public class CountdownCLK : MonoBehaviour {

	float timeRemaining = 60;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		timeRemaining -= Time.deltaTime;

	}

	void OnGUI(){
		if (timeRemaining > 0)
			{
					GUI.Label(new Rect(500,20,Screen.width,Screen.height), "Time Remaining:" + (int)timeRemaining);
			}
			else
			{
					GUI.Label(new Rect(500,20,Screen.width,Screen.height), "TAKE COVER!");
			}
	}

}
