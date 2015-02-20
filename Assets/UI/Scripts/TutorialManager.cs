using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class TutorialManager : MonoBehaviour {

	public GameObject eventSystemObject;
	public GameObject tutorialStartButton;
	public List<GameObject> tutorials;
	private EventSystem eventSystem;

	// Use this for initialization
	void Start () {
		eventSystem = eventSystemObject.GetComponent<EventSystem> ();
		foreach (GameObject tut in tutorials) {
			tut.SetActive(false);
		}
		//Start first tutorial panel
		tutorials[0].SetActive (true);
		eventSystem.SetSelectedGameObject(tutorialStartButton);
	}
	public void NextTutorial() {
		//Disable current menu and remove it from list
		tutorials [0].SetActive (false);
		tutorials.RemoveAt (0);
		//Check if there exists next tutorial
		if (tutorials.Count>0) {
			tutorials [0].SetActive (true);
			//Activate button on that panel so that the xbox controller can access it
			try {
				GameObject button = (tutorials[0].transform.FindChild("Panel")).FindChild ("YesButton").gameObject;
				eventSystem.SetSelectedGameObject(button);
			}
			catch(System.NullReferenceException e) {

			}
		}
	}
	public void ExitTutorial() {
		tutorials[0].SetActive (false);
	}
}
