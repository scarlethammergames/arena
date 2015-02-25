//Reference: http://docs.unity3d.com/ScriptReference/Object.Instantiate.html

using UnityEngine;
using System.Collections;
using GamepadInput;

public enum ProjectileAction { LAUNCH, BEAM, ATTACH_TO_SELF }
public enum ProjectileTriggerButton { LEFT, RIGHT }

/**
* Spawn a rigid body GameObject with an initial velocity when triggered. 
* Constraints: The projectile must contain a rigid body.
*/
public class fireProjectile: MonoBehaviour {
	public GameObject projectile;
	public Vector3 offset;
	public Vector3 trajectory = Vector3.forward;
	public float magnitude = 50;
	public float drag = 5;
	public bool makeChild = false;

	public bool isControllable = false;
	
	Vector3 forward;
	public float cooldown = 3;
	float cooldownTimer;
	float triggerThreshold = 0.20f;

	bool leftTriggerHeld;
	bool alreadyFired = false;
	GameObject controllable;

	//public string inputName = "Fire1";
	public ProjectileAction projectileAction = ProjectileAction.LAUNCH;
	public ProjectileTriggerButton projectileButton = ProjectileTriggerButton.LEFT;
	//public bool isBomb = false;
	GamePad.Index pad_index = GamePad.Index.One;
	
	//	public enum TrajectoryType {Straight, Lob, Drop, Attach};
	//	public TrajectoryType style = TrajectoryType.Straight;
	
	// Use this for initialization
	void Start () {
		cooldownTimer = cooldown;
	}
	
	// Update is called once per frame
	void Update () {
		cooldownTimer -= Time.deltaTime;
		/*if ( Input.GetButtonDown( inputName ) ){
			Fire ();
			cooldown = 0;
		}*/
		leftTriggerHeld = (GamePad.GetTrigger (GamePad.Trigger.LeftTrigger, pad_index) > triggerThreshold);
		if (cooldownTimer <= 0.0f) {
			if ( (GamePad.GetTrigger (GamePad.Trigger.RightTrigger, pad_index) > triggerThreshold && projectileButton == ProjectileTriggerButton.RIGHT) 
			    || (leftTriggerHeld && projectileButton == ProjectileTriggerButton.LEFT) ){
				if (projectileAction == ProjectileAction.LAUNCH) {
					if(isControllable && leftTriggerHeld){
						if(!alreadyFired){
							LaunchProjectile();
							alreadyFired = true;
							// freeze the player, make this more efficientefficient? (i.e. don't use  findgameobject method)
							GameObject.FindGameObjectWithTag("Player").GetComponent<DeftPlayerController>().enabled = false;
						}
						// TODO: control the ball

					} else{
						LaunchProjectile ();
					}
				} else if (projectileAction == ProjectileAction.BEAM) {
					BeamAttack ();
				}
					cooldownTimer = cooldown;
			}
		}

		// exited the remote control mode
		if (!leftTriggerHeld && alreadyFired) {
			alreadyFired = false;
			Destroy(controllable, 0);
			
			// unfreeze player, make this more efficient? (i.e. don't use  findgameobject method)
			GameObject.FindGameObjectWithTag("Player").GetComponent<DeftPlayerController>().enabled = true;
		}
	}
	
	void LaunchProjectile(){
		GameObject clone;
		clone = Instantiate( projectile, transform.position + offset, transform.rotation ) as GameObject;
		//clone.rigidbody.velocity = transform.TransformDirection( trajectory * magnitude );

		if (isControllable) {
			controllable = clone;
			controllable.GetComponent<TimedLifespan> ().enabled = false;
		}

		forward = Camera.main.transform.TransformDirection(Vector3.forward);
		forward = forward.normalized;
		clone.rigidbody.velocity = (new Vector3(forward.x * magnitude,0,forward.z * magnitude));
		
		if( makeChild ){
			clone.transform.parent = this.transform;
		}
	}

	void BeamAttack(){
				GameObject clone;
				clone = Instantiate (projectile, transform.position + offset, transform.rotation) as GameObject;
				//clone.rigidbody.velocity = transform.TransformDirection( trajectory * magnitude );

		forward = Camera.main.transform.TransformDirection(Vector3.forward);
		forward = forward.normalized;
		clone.rigidbody.velocity = (new Vector3(forward.x * magnitude,0,forward.z * magnitude));
		
		if( makeChild ){
			clone.transform.parent = this.transform;
		}
	}
}
