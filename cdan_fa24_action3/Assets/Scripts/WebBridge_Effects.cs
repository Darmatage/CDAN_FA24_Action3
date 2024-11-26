using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebBridge_Effects : MonoBehaviour{
	
	public bool onWeb = false;

/*
private Animator anim;

	void Start(){
		anim = gameObject.GetComponentInChildren<Animator>();
	}
*/

	public void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.tag=="Web"){
			gameObject.GetComponent<Collider2D>().enabled=false;
			Debug.Log("I turned off player collider");
			onWeb = true;
		}
    }

	public void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.tag=="Web"){
			StartCoroutine(OffWeb());
			onWeb = false;
		}
    }

	IEnumerator OffWeb(){
		yield return new WaitForSeconds(.2f);
		gameObject.GetComponent<Collider2D>().enabled=true;
		Debug.Log("player collider back on");
	}

}
