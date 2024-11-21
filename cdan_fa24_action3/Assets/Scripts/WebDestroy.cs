using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebDestroy : MonoBehaviour{


    public bool isWebbed = false;

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Web"){
            Debug.Log("No web allowd here!");
            isWebbed = true;
            Destroy(other.gameObject);
        }
    }
}
   