using UnityEngine;
using System.Collections;

public class ScoreCounter : Counter {

    public static ScoreCounter Instance;

	void Start () 
    {
        Instance = this;
	}
	
}
