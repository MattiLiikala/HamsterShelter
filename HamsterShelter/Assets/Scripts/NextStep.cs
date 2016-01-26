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
			teksti.text = "From the top of the screen you can drag and drop horizontal and vertical building blocks into the ground as well as hamsters. Click to continue";
		}
		if (counter == 2) {
			teksti.text = "On the left bottom corner you see how many blocks you have used and on right bottom you see how many hamsters you have alive. Click to continue";
		}
		if (counter == 3) {
			teksti.text = "Next, try dragging and dropping a hamster to the ground and a few blocks to cover it. The armageddon will start in 30 seconds once you close this window. Click to close this window";
		}
		if (counter == 4) {
			Instantiate(sade, paikka, Quaternion.identity);
			Object.Destroy(this.gameObject);
		}
	}

}