#pragma strict
// https://www.youtube.com/playlist?list=PLVNVz3FV6VpKGpkIbugsVOLP1ePD0OiQ-

var pauseCanvas : Canvas;
var optionsCanvas : Canvas;

function Start() {
	Screen.lockCursor = true;
	Screen.showCursor = false;
}

function Update () {
	if (Input.GetKeyDown(KeyCode.Escape)) {
		//(gameObject.Find("First Person Controller").GetComponent("MouseLook") as MonoBehaviour).enabled = false;
		//(gameObject.Find("Main Camera").GetComponent("MouseLook") as MonoBehaviour).enabled = false;
		pauseCanvas.enabled = true;
		Time.timeScale = 0;//Stops all animations
		Screen.lockCursor = false;
		Screen.showCursor = true;
	}
}

function ResumeGame () {
	//(gameObject.Find("First Person Controller").GetComponent("MouseLook") as MonoBehaviour).enabled = true;
	//(gameObject.Find("Main Camera").GetComponent("MouseLook") as MonoBehaviour).enabled = true;
	pauseCanvas.enabled = false;
	Time.timeScale = 1;//Resumes animation
	Screen.lockCursor = true;
	Screen.showCursor = false;
}

function PauseOptions() {
	pauseCanvas.enabled = !pauseCanvas.enabled;
	optionsCanvas.enabled = !optionsCanvas.enabled;
}

function ExitGame() {
	Application.Quit();
}

function ExitToMainMenu() {
	Application.LoadLevel("MainMenu");
	pauseCanvas.enabled = false;
	Time.timeScale = 1;
}