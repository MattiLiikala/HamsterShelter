using UnityEngine;
using System.Collections;

public class SpawnHamsterButton : MonoBehaviour {

	public GameObject Hamster;
	public Vector3 SpawnPosition;

	public void onClick()
    {
        ScoreController.Count++;
		Instantiate (Hamster, SpawnPosition, Quaternion.identity);
	}
}