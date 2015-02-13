using UnityEngine;
using System.Collections;

public class depotAbsorb : MonoBehaviour {
	public string supplyItemTag;

	public int capacity = 5000;
	public int resourceVal = 100;
	public int currentStock = 0;

	private Animator myAnim;

	void Start(){
		myAnim = this.GetComponent<Animator>();
		myAnim.SetBool("isStopped", true);
		myAnim.SetBool("isPlaying", false);
		myAnim.speed = 0.0f;
	}


	void OnTriggerEnter(Collider other){

		/*
		 * replace 10 with other.gameObject.value
		 * 
		 *
		 */
		if(other.tag == supplyItemTag){
			currentStock = currentStock + resourceVal;

			if ((this.currentStock + resourceVal) < capacity) {
				Debug.Log("Growing");
				this.currentStock += resourceVal;
				myAnim.SetBool("isPlaying", true);
				myAnim.SetBool("isStopped", false);
				myAnim.Play("depotGrow", 0, myAnim.GetCurrentAnimatorStateInfo(0).length * currentStock/capacity);
			}else{
				Debug.Log ("Resource Depot FuLL");
			}
			//this.transform.parent.transform.localScale += (Vector3.up * 0.1f);
			Destroy( other.gameObject );
		}

	}

}
