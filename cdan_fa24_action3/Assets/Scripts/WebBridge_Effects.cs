using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebBridge_Effects : MonoBehaviour{

	public bool onWeb = false;

/*
    public void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag=="Player"){
			StartCoroutine(OnWeb(other.gameObject));
		}
    }
*/
	public void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.tag=="Player"){
			other.gameObject.GetComponent<Collider2D>().enabled=false;
			Debug.Log("I turned off player collider");
			onWeb = true;
		}
    }

	public void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.tag=="Player"){
			//other.gameObject.GetComponent<Collider2D>().enabled=true;
			//Debug.Log("player collider back on");
			StartCoroutine(OffWeb(other.gameObject));
			onWeb = false;
		}
    }
/*
	IEnumerator OnWeb(GameObject walker){
		walker.GetComponent<Collider2D>().enabled=false;
		Debug.Log("I turned off player collider");
		yield return new WaitForSeconds(0.1f);
	}
*/

	IEnumerator OffWeb(GameObject walker){
		yield return new WaitForSeconds(0.2f);
		walker.GetComponent<Collider2D>().enabled=true;
		Debug.Log("player collider back on");
		yield return new WaitForSeconds(0.1f);
		walker.GetComponent<Collider2D>().enabled=true;
	}


}
