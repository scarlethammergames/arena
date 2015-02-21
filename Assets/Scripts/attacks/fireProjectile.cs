//Reference: http://docs.unity3d.com/ScriptReference/Object.Instantiate.html

using UnityEngine;
using System.Collections;
using GamepadInput;

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
	
	Vector3 forward;
	float shoot_timer = 3;
	
	//public string inputName = "Fire1";
	public bool isProjectile = true;
	//public bool isBomb = false;
	GamePad.Index pad_index = GamePad.Index.One;
	
	//	public enum TrajectoryType {Straight, Lob, Drop, Attach};
	//	public TrajectoryType style = TrajectoryType.Straight;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		shoot_timer += Time.deltaTime;
		/*if ( Input.GetButtonDown( inputName ) ){
			Fire ();
			shoot_timer = 0;
		}*/

		if (shoot_timer > 1.1f) {
			if (isProjectile) {
				if (GamePad.GetTrigger (GamePad.Trigger.RightTrigger, pad_index) > 0.20f) {
					Fire ();
					shoot_timer = 0;
				}
			} else {
				if (GamePad.GetTrigger (GamePad.Trigger.LeftTrigger, pad_index) > 0.20f) {
					Fire ();
					shoot_timer = 0;
				}
			}
		}

	}
	
	void Fire(){
		GameObject clone;
		clone = Instantiate( projectile, transform.position + offset, transform.rotation ) as GameObject;
		//clone.rigidbody.velocity = transform.TransformDirection( trajectory * magnitude );
		
		forward = Camera.main.transform.TransformDirection(Vector3.forward);
		forward = forward.normalized;
		clone.rigidbody.velocity = (new Vector3(forward.x * magnitude,0,forward.z * magnitude));
		
		if( makeChild ){
			clone.transform.parent = this.transform;
		}
	}
}
