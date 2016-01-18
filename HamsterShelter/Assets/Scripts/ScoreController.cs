using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreController : MonoBehaviour {

    private static int total, count;

    private static Text text;

	public static int Total
    {
        get { return total; }
        set
        {
            total = value;
            text.text = "Hamsters alive: " + count + "/" + total;
        }
    }
	public static int Count
    {
        get { return count; }
        set
        {
            count = value;
            total = System.Math.Max(count, total);
            text.text = "Hamsters alive: " + count + "/" + total;
        }
    }    

	void Start () {
		text = GetComponent<Text> ();	
	}
	
}

