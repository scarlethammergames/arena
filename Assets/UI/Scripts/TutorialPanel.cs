using UnityEngine;
using System.Collections;

public class TutorialPanel : MonoBehaviour {

	public float left = 0.0f;
	public float top = 0.0f;

	private RectTransform rect;
	private RectTransform arrow;

	void Start () {
		//Get and set panel size and position
		rect = gameObject.transform.GetChild (0).GetComponent<RectTransform> ();
		rect.anchoredPosition = new Vector2 (Screen.width * left, Screen.height * top);
//		rect.sizeDelta = new Vector2 (width, height);

		//Get and set arrow position
		arrow = gameObject.transform.GetChild (1).GetComponent<RectTransform>();
		arrow.sizeDelta = new Vector2(Screen.width*0.05f, Screen.width*0.05f);
		arrow.anchoredPosition = new Vector2 (Screen.width * left, (Screen.height * top) + arrow.sizeDelta.y);

		Debug.Log ("box anchor:" + rect.anchoredPosition);
		Debug.Log ("box size:" + rect.sizeDelta);
		Debug.Log ("arrow anchor:" + arrow.anchoredPosition);

	}


}
