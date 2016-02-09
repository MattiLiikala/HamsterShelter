using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NextStep : MonoBehaviour {

	private Vector3 paikka = new Vector3(0.48f, 6.54f, 0.0f);
	public GameObject sade;
	public Text teksti;
	private int counter;



	public void onClick() {

		counter++;
		if (counter == 1) {
			teksti.text = "From the top of the screen you can drag and drop hamsters to the ground. Drag a hamster to the ground and click here to continue";
		}
		if (counter == 2) {
			teksti.text = "You can also drag and drop building blocks from top menu. Drag blocks to build a simple shelter for your hamster and click here to continue";
		}
		if (counter == 3) {
			teksti.text = "If you are happy with your shelter click here and armageddon will start in 5 secods";
		}
		if (counter == 4) {
			Instantiate(sade, paikka, Quaternion.identity);
			Object.Destroy(this.gameObject);
		}
	}


}