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
			teksti.text = "Welcome to Hamster Shelter! Your task is to build a shelter for hamsters before the armageddon starts. Click to continue";
		}
		if (counter == 2) {
			teksti.text = "From top menu you can drag and drop building blocks to build a shelter. Build a simple shelter for the smaller hamster sitting on the ground and click here to continue";
		}
		if (counter == 3) {
			teksti.text = "If you are happy with your shelter click here and armageddon will start in 5 seconds";
		}
		if (counter == 4) {
			Instantiate(sade, paikka, Quaternion.identity);
			Object.Destroy(this.gameObject);
		}
	}


}