using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSystem_Water : MonoBehaviour{
	//Animator anim;
	public float current = 5f;

    void Start(){
		//anim = gameObject.GetComponentInChildre<Animator>().
    }

    void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>().playerGetHit(10);
			//wash the player along the water current direction
			other.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * current *-1, ForceMode2D.Impulse);
		}
    }

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			//zero out the Rigidbody velocity
			other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
		}
    }

}
