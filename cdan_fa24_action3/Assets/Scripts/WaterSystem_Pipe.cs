using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSystem_Pipe : MonoBehaviour{
	public GameObject pipeOpen;
	public GameObject pipeClosed;
	public GameObject water;

    void Start(){
		pipeOpen.SetActive(true);
		pipeClosed.SetActive(false);
		water.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Web"){
			pipeOpen.SetActive(false);
			pipeClosed.SetActive(true);
			//water.SetActive(false);
			water.GetComponent<WaterSystem_Water>().TurnOffWater();
			gameObject.GetComponent<Collider2D>().enabled=false;
		}
    }

}
