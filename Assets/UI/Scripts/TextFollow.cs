﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextFollow : MonoBehaviour {

	public GameObject followObject;
	public Vector3 offset = Vector3.up;
	public float viewDepth = 50f;//The max depth of object in scene before text is disabled
	private Camera camera;
	private Text text;

	// Use this for initialization
	void Start () {
		camera = Camera.main;
		text = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 viewport = camera.WorldToViewportPoint (followObject.transform.position + offset);
		Debug.Log (viewport);
		//If object is less than a certain distance, show its text
		if (viewport.z < viewDepth && viewport.x >=0 && viewport.y>=0 && viewport.z>=0) {
			transform.position = new Vector3 (viewport.x * Screen.width, viewport.y * Screen.height, 0f);
			text.enabled = true;
		} else {
			text.enabled = false;
		}
	}
}