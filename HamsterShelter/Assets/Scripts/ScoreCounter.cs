using UnityEngine;
using System.Collections;

public class ScoreCounter : Counter {

    public static ScoreCounter Instance;

    protected override void Awake()
    {
        base.Awake();

        Instance = this;
    }

}
