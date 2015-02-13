using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InGameStats : MonoBehaviour {

	public GameObject eventSystemObject;
	public GameObject gameOverFirstSelected;
	public GameObject gameOverWindow;
	public GameObject winText;
	public GameObject loseText;
	public int depotsFull=0;
	public int totalDepots=1;
	private EventSystem eventSystem;

	void Start() {
		gameOverWindow.SetActive (false);
		winText.SetActive (false);
		loseText.SetActive (false);
		eventSystem = eventSystemObject.GetComponent<EventSystem> ();
	}

	public void depotFull() {
		Debug.Log ("game manager called");
		depotsFull++;
		if (depotsFull == totalDepots) {
			Debug.Log ("YOU WIN.");
			gameOverWindow.SetActive (true);
			winText.SetActive (true);
			eventSystem.SetSelectedGameObject(gameOverFirstSelected);
		}
	}
	public void playerDied() {
		Debug.Log ("YOU DIED.");
		gameOverWindow.SetActive (true);
		loseText.SetActive (true);
		eventSystem.SetSelectedGameObject(gameOverFirstSelected);
	}


}
