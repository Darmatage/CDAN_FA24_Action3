using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler_Fungified : MonoBehaviour{

	private GameObject theDoor;
	public TMP_Text fungifiedText;
	private int fungusApplied = 0;
	public int fungusNeeded = 5;
	private bool doorIsOpen = false;

	//for the pulse:
	public Image fadeHighlight;
	private float pulseHold = 0.4f;
	private float pulseDelay = 2f;
	private float pulseSpeed = 0.02f;
	private bool timeToFadeOut = false;
	private bool timeToFadeIn = false;
	private float fadeAlpha = 0f;

    void Start(){
		theDoor = GameObject.FindWithTag("Door");
        UpdatedFungifyText();
		//for the pulse:
		fadeHighlight.color = new Color(2.5f, 2.2f, 0.3f, 0f);
		StartCoroutine(RandomDelay());
    }

	public void AddFungus(){
		fungusApplied += 1;	
		UpdatedFungifyText();
		if (fungusApplied >= fungusNeeded){
			theDoor.GetComponent<InteractableDoor>().OpenTheDoor();
			doorIsOpen = true;
		}
	}

	public void UpdatedFungifyText(){

		fungifiedText.text = "FUNGIFIED: " + fungusApplied + " / " + fungusNeeded;
	}

	//PULSE BACKGROUND WHEN DOOR IS OPEN:
	void FixedUpdate(){
		if (doorIsOpen){
            if (timeToFadeIn){
                  fadeAlpha += pulseSpeed;
                  fadeHighlight.color = new Color(2.5f, 2.2f, 0.3f, fadeAlpha);
                  if (fadeAlpha >= 0.5f){
                       fadeAlpha = 0.5f;
                        StartCoroutine(PulseFull());
                  }
            }
           else if (timeToFadeOut){
                  fadeAlpha -= pulseSpeed;
                  fadeHighlight.color = new Color(2.5f, 2.2f, 0.3f, fadeAlpha);
                  if (fadeAlpha <= 0f){
                        fadeAlpha = 0f;
                        StartCoroutine(PulsePause());
                  }
            }
		}
      }

//delay start of pulsing, so neighboring pickups do not pulse the same
      IEnumerator RandomDelay(){
            float randDelay = Random.Range(0.1f, 2.0f);
            yield return new WaitForSeconds(randDelay);
            timeToFadeIn = true;
      }

      //stay at full strength before fading away
      IEnumerator PulseFull(){
            yield return new WaitForSeconds(pulseHold);
            timeToFadeIn = false;
            timeToFadeOut = true;
      }

      //pause before next pulse
      IEnumerator PulsePause(){
            yield return new WaitForSeconds(pulseDelay);
            timeToFadeOut = false;
            timeToFadeIn = true;
      }


}
