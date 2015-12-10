using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour {


	public int timeTillNext;
	public int level;
	// Use this for initialization
	void Start () {
		StartCoroutine (HopToNext());
	}
	
	IEnumerator HopToNext() {
		yield return new WaitForSeconds(timeTillNext);
		Application.LoadLevel (level);
	}
}
