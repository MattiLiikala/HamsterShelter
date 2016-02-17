using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountdownCLK : MonoBehaviour 
{
    [SerializeField]
    private Text text;
    
	private float timeRemaining;

    private bool countdownFinished;

    void Start()
    {
        timeRemaining = MeteorRain.Instance.WaitTimer;
    }

	// Update is called once per frame
	void Update () 
    {
        if (countdownFinished) return;
        timeRemaining = MeteorRain.Instance.WaitTimer;

        text.text = timeRemaining > 0 ? "Time remaining: " + (int)timeRemaining : "TAKE COVER!";

        if (timeRemaining <= 0.0f)
        {
            countdownFinished = true;
        }
	}    
}
