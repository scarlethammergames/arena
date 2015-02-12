using UnityEngine;
using System.Collections;

public class PauseScreen : MonoBehaviour {

	public GameObject pauseMenu;
	private bool isEnabled;

	// Use this for initialization
	void Start () {
		isEnabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			//Stop animations
			if (Time.timeScale == 1)
			{
				Time.timeScale = 0;
			}
			else
			{
				Time.timeScale = 1;
			}
			//Hide/display pause menu
			isEnabled = !isEnabled;
			pauseMenu.SetActive(isEnabled);
		}
	}
}
