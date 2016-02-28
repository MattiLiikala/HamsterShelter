using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NextStep : MonoBehaviour {

    private Vector3 paikka = new Vector3(0.48f, 6.54f, 0.0f);
    public GameObject sade;
    public Text teksti;
    private int counter = 1;

    public void onClick() {

		counter++;
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