using UnityEngine;
using System.Collections;

public class LevelPassedMenu : MonoBehaviour {

    public GameObject[] Peanuts;

	public void ShowScore()
    {
        float percentage = ScoreCounter.Instance.Count / (float)ScoreCounter.Instance.Total;

        int peanutCount = (int)Mathf.Floor(Peanuts.Length * percentage);
        for (int i = 0; i < Peanuts.Length; i++)
        {
            Peanuts[i].SetActive(i<peanutCount);
        }
    }
}
