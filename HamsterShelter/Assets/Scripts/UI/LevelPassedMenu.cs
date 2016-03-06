using UnityEngine;
using System.Collections;

public class LevelPassedMenu : MonoBehaviour {

    public GameObject[] Peanuts;

	public void ShowScore()
    {
        float percentage = ScoreCounter.Instance.Count / (float)ScoreCounter.Instance.Total;

        int peanutCount = (int)Mathf.Floor(Peanuts.Length * percentage);

        if (ScoreCounter.Instance.Count > 0) peanutCount = Mathf.Max(1, peanutCount);

        gameObject.SetActive(true);
        StartCoroutine(ScoreAnimation(peanutCount));
    }

    private IEnumerator ScoreAnimation(int peanutCount)
    {       
        for (int i = 0; i < Peanuts.Length; i++)
        {
            Peanuts[i].SetActive(false);
        }

        var audioSource = GetComponent<AudioSource>();

        for (int i = 0; i < peanutCount; i++)
        {
            yield return new WaitForSeconds(1.0f);

            if (audioSource != null) audioSource.Play();
            Peanuts[i].SetActive(true);
        }
    }
}
