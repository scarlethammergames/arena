using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public MainMenu2 CurrentMenu;

	public void Start() {
		ShowMenu (CurrentMenu);
	}

	public void ShowMenu(MainMenu2 menu) {
		if (CurrentMenu != null) {
			CurrentMenu.IsOpen = false;
		}
		CurrentMenu = menu;
		CurrentMenu.IsOpen = true;
	}
	public void PlayGame(string sceneName) {
		Application.LoadLevel (sceneName);
	}
	public void QuitGame() {
		Application.Quit();
	}
}
