using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableDoor : MonoBehaviour{

	public string NextLevel = "MainMenu";
	public GameObject msgPressE;
	public GameObject msgNotReady;
	public bool canPressE = false;

	public GameObject doorClosed;
	public GameObject doorOpen;

	public bool doorIsOpen = false;
		

	void Start(){
		msgPressE.SetActive(false);
		msgNotReady.SetActive(false);
		doorClosed.SetActive(true);
		doorOpen.SetActive(false);
	}

       void Update(){
              if ((canPressE == true) && (Input.GetKeyDown(KeyCode.E))){
                     EnterDoor();
              }
        }

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){ 
			if (doorIsOpen){
				msgPressE.SetActive(true);
				canPressE =true;
			}
			else {
				msgNotReady.SetActive(true);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			msgNotReady.SetActive(false);
			msgPressE.SetActive(false);
			canPressE = false;	
		}
	}

	public void EnterDoor(){
		SceneManager.LoadScene (NextLevel);
	}

	public void OpenTheDoor(){
		doorClosed.SetActive(false);
		doorOpen.SetActive(true);
		doorIsOpen = true;
	}

}