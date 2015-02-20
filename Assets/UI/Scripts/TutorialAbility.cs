using UnityEngine;
using System.Collections;

public class TutorialAbility : MonoBehaviour {

	public string inputName = "P1_Fire1";
	public GameObject gameManager;
	private TutorialManager tutManager;

	void Start () {
		tutManager = gameManager.GetComponent<TutorialManager> ();
	}
	
	void Update () {
		if (Input.GetButtonDown (inputName)) {
			tutManager.NextTutorial();
		}
	}
}