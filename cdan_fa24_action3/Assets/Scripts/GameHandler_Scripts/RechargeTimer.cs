using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RechargeTimer : MonoBehaviour {
	public float energyMax =3f;       //set the number of seconds here (both floats can be static)
	public float energyUsedInWeb = 1f; //cost of shooting a web
	public float energyTimer = 0f;
	public bool thingOn = false;

	public GameObject display;
	private Color startColor;

	void Start(){
		energyTimer = energyMax;
		startColor = display.GetComponent<Image>().color;
	}

	void Update(){
		if (Input.GetKeyDown("8")){
			thingOn = !thingOn;           //reverse the bool, so keypress is a toggle
		}
	}

	void FixedUpdate(){
		/*
		if (thingOn == true){
			energyTimer -= 0.01f;
			//Debug.Log("energy down: " + energyTimer);  //replace with the desired ability display
			display.GetComponent<Image>().fillAmount = energyTimer / energyMax; 
			if (energyTimer <= 0){
				thingOn = false;                                   // time has run out
			}
		} else if (thingOn == false){
		*/
		if (energyTimer < energyMax) {
			energyTimer += 0.003f;
			//Debug.Log("energy up: " + energyTimer);  //replace with the desired ability display
			TimerDisplay();
		}
	}

	public void UseWebEnergy(float usedAmt){
		energyTimer -= usedAmt;
		TimerDisplay();

	} 

	void TimerDisplay(){
			display.GetComponent<Image>().fillAmount = energyTimer / energyMax; 
			
			if (energyTimer < energyUsedInWeb){
				display.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 0.9f); 
			} else {
				display.GetComponent<Image>().color = startColor;
			}
	}

}