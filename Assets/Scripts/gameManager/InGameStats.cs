using UnityEngine;
using System.Collections;

public class InGameStats : MonoBehaviour {

	public GameObject gameOverWindow;
	public GameObject winText;
	public GameObject loseText;
	public int depotsFull=0;
	public int totalDepots=1;

	void Start() {
		gameOverWindow.SetActive (false);
		winText.SetActive (false);
		loseText.SetActive (false);
	}

	public void depotFull() {
		Debug.Log ("game manager called");
		depotsFull++;
		if (depotsFull == totalDepots) {
			Debug.Log ("YOU WIN.");
			gameOverWindow.SetActive (true);
			winText.SetActive (true);
		}
	}
	public void playerDied() {
		Debug.Log ("YOU DIED.");
		gameOverWindow.SetActive (true);
		loseText.SetActive (true);
	}


}
