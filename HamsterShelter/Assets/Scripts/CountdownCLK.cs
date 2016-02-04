using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountdownCLK : MonoBehaviour 
{
    public float StartTime = 60.0f;

    [SerializeField]
    private Text text;
    
	private float timeRemaining;

    private bool countdownFinished;

    void Start()
    {
        timeRemaining = StartTime;
    }

	// Update is called once per frame
	void Update () 
    {
        if (countdownFinished) return;
		timeRemaining -= Time.deltaTime;

        text.text = timeRemaining > 0 ? "Time remaining: " + (int)timeRemaining : "TAKE COVER!";

        if (timeRemaining <= 0.0f)
        {
            countdownFinished = true;
        }
	}    
}
