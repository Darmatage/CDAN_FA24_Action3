using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebDestroy : MonoBehaviour{
    
    public bool isWebbed = false;

    void Start(){
         
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Web"){
            Debug.Log("No web allowed here!");
            isWebbed = true;
            StartCoroutine(DestroyTheWebbing(other.gameObject));
        }
    }

	void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.tag == "Web"){
            isWebbed = false;
        }
    }

	IEnumerator DestroyTheWebbing(GameObject web){
		web.GetComponentInChildren<Animator>().SetBool("Destroy", true);
		//web.GetComponent<WebBridge_Effects>().DissolveWeb();
		yield return new WaitForSeconds(0.2f);  
		Destroy(web);
	}


}
   