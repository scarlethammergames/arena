﻿using UnityEngine;
using System.Collections;

public class SnifferMover : MonoBehaviour {

	private NavMeshAgent agent;
	private string objectHit;
	private bool foundTarget;
	private Transform target;

	public float walkRadius;

	// Use this for initialization
	void Start () {

		foundTarget = false;

		agent = GetComponent<NavMeshAgent> ();

	}
	
	// Update is called once per frame
	void Update () {

		if(!foundTarget){

			Vector3 randDirection = Random.insideUnitSphere * walkRadius;
			randDirection.y = 0.0f;

			randDirection += transform.position;

			NavMeshHit hit;
			NavMesh.SamplePosition (randDirection, out hit, walkRadius, 1);
			Vector3 finalPos = hit.position;

			agent.SetDestination (finalPos);

			return;

		}

        if (target) {
            agent.SetDestination(target.position);
        }
	}

	public void setTarget(Transform chaseTarget)
	{

		foundTarget = true;
		target = chaseTarget;

	}


	void OnCollisionEnter(Collision other)
	{

		objectHit = other.gameObject.tag;


		if(objectHit.Equals("Player"))
		{

			Debug.Log ("Hit");
			//TODO
			//Damage the player by taking health away from him/her

		}

		if(objectHit.Equals ("EnviroTile"))
		{

			Debug.Log ("NomNomNom");
			Destroy(other.gameObject);
			foundTarget = false;

		}

		if(objectHit.Equals ("ResouceDepot") || objectHit.Equals ("RemoteDepot"))
		{

			Debug.Log ("Taking Resoruce");
			//TODO
			//Remove x amount from the main resource pool and if hitting the remote depot then damage it as well

		}

	}
}
