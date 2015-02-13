﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DepotStats : MonoBehaviour {

	public GameObject mainSupplyDepot; 
	private SupplyDepotBehaviour supplyDepotBehaviour;
	private Text stats;

	// Use this for initialization
	void Start () {
		supplyDepotBehaviour = mainSupplyDepot.GetComponent<SupplyDepotBehaviour> ();
		stats = this.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		stats.text = "Depot: " + supplyDepotBehaviour.getCurrentSize().ToString() + "/" + supplyDepotBehaviour.getSize().ToString();
	}
}
