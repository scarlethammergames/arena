//Reference: http://docs.unity3d.com/ScriptReference/Object.Instantiate.html

using UnityEngine;
using System.Collections;
using GamepadInput;

public enum ProjectileAction { LAUNCH, BEAM, ATTACH_TO_SELF, REMOTE_CTRL }
public enum ProjectileTriggerButton { LEFT, RIGHT }

/**
* Spawn a rigid body GameObject with an initial velocity when triggered. 
* Constraints: The projectile must contain a rigid body.
*/
public class fireProjectile: MonoBehaviour {
	public GameObject _projectile;
	public Vector3 _offset;
	public Vector3 _trajectory = Vector3.forward;
	public float _magnitude = 50;
	public float _drag = 5;
	public bool _makeChild = false;

	public bool _isControllable = false;
	
	Vector3 forward;
	public float _cooldown = 3;
	float _cooldownTimer;
	float _triggerThreshold = 0.20f;

	bool _leftTriggerHeld;
	bool _rightTriggerHeld;
	bool _alreadyFired = false;
	GameObject _controllable;

	//public string inputName = "Fire1";
	public ProjectileAction _projectileAction = ProjectileAction.LAUNCH;
	public ProjectileTriggerButton _projectileButton = ProjectileTriggerButton.LEFT;
	//public bool isBomb = false;
	GamePad.Index _padIndex = GamePad.Index.One;
	
	//	public enum TrajectoryType {Straight, Lob, Drop, Attach};
	//	public TrajectoryType style = TrajectoryType.Straight;
	
	// Use this for initialization
	void Start () {
		_cooldownTimer = _cooldown;
	}
	
	// Update is called once per frame
	void Update () {
		_cooldownTimer -= Time.deltaTime;
		/*if ( Input.GetButtonDown( inputName ) ){
			Fire ();
			cooldown = 0;
		}*/
		_leftTriggerHeld = (GamePad.GetTrigger (GamePad.Trigger.LeftTrigger, _padIndex) > _triggerThreshold);
		_rightTriggerHeld = (GamePad.GetTrigger (GamePad.Trigger.RightTrigger, _padIndex) > _triggerThreshold);
		if (_cooldownTimer <= 0.0f) {
			if ( (_leftTriggerHeld && _projectileButton == ProjectileTriggerButton.RIGHT) 
			|| (_rightTriggerHeld && _projectileButton == ProjectileTriggerButton.LEFT) ){
				if (_projectileAction == ProjectileAction.LAUNCH) {
					networkView.RPC("LaunchProjectile", RPCMode.All, _offset, _magnitude, _makeChild);
				} else if (_projectileAction == ProjectileAction.BEAM) {
					BeamAttack ();
				} else if (_projectileAction == ProjectileAction.REMOTE_CTRL) {
					if( _isControllable ){
						if(!_alreadyFired){
							LaunchControllable();
							_alreadyFired = true;
							// freeze the player, make this more efficientefficient? (i.e. don't use  findgameobject method)
							GameObject.FindGameObjectWithTag("Player").GetComponent<DeftPlayerController>().enabled = false;
						}
						// TODO: control the ball
				}
				_cooldownTimer = _cooldown;
			}
		}

		// exited the remote control mode
		if (!_leftTriggerHeld && _alreadyFired) {
			_alreadyFired = false;
			Destroy(_controllable, 0);
			
			// unfreeze player, make this more efficient? (i.e. don't use  findgameobject method)
			GameObject.FindGameObjectWithTag("Player").GetComponent<DeftPlayerController>().enabled = true;
		}
	}
	}

	[RPC]
	void LaunchProjectile(Vector3 offset, float magnitude, bool makeChild){
		GameObject clone;
		clone = Instantiate( _projectile, transform.position + offset, transform.rotation ) as GameObject;
		//clone.rigidbody.velocity = transform.TransformDirection( trajectory * magnitude );

		Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
		forward = forward.normalized;
		clone.rigidbody.velocity = (new Vector3(forward.x * magnitude, 0, forward.z * magnitude));
		
		if( makeChild ){
			clone.transform.parent = this.transform;
		}
	}

	[RPC]
	void BeamAttack(){
				GameObject clone;
				clone = Instantiate (_projectile, transform.position + _offset, transform.rotation) as GameObject;
				//clone.rigidbody.velocity = transform.TransformDirection( trajectory * magnitude );

		forward = Camera.main.transform.TransformDirection(Vector3.forward);
		forward = forward.normalized;
		clone.rigidbody.velocity = (new Vector3(forward.x * _magnitude,0,forward.z * _magnitude));
		
		if( _makeChild ){
			clone.transform.parent = this.transform;
		}
	}

	void LaunchControllable(){
		GameObject clone;
		clone = Instantiate( _projectile, transform.position + _offset, transform.rotation ) as GameObject;
		//clone.rigidbody.velocity = transform.TransformDirection( trajectory * magnitude );
		
		if (_isControllable) {
			_controllable = clone;
			_controllable.GetComponent<TimedLifespan> ().enabled = false;
		}
		
		forward = Camera.main.transform.TransformDirection(Vector3.forward);
		forward = forward.normalized;
		clone.rigidbody.velocity = (new Vector3(forward.x * _magnitude,0,forward.z * _magnitude));
		
		if( _makeChild ){
			clone.transform.parent = this.transform;
		}
	}
}
