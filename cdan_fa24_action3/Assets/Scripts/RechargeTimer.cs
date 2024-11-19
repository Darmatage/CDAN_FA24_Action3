using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RechargeTimer2 : MonoBehaviour {
       public float energyMax =3f;       //set the number of seconds here (both floats can be static)
       private float energyTimer = 0f;
       public bool thingOn = false;

       public GameObject display;

       void Start(){
           energyTimer = energyMax;
        }

       void Update(){
            if (Input.GetKeyDown("8")){
                  thingOn = !thingOn;           //reverse the bool, so keypress is a toggle
            }
        }

       void FixedUpdate(){
              if (thingOn == true){
                     energyTimer -= 0.01f;
                     Debug.Log("energy down: " + energyTimer);  //replace with the desired ability display

                     Color oldCol = display.GetComponent<Image>().color;
                     if (oldCol.a > 0.01f){
                             float r = oldCol.r - 0.005f;
                            float g = oldCol.g - 0.005f;
                             float b = oldCol.b - 0.005f;
                            float a = oldCol.a - 0.005f;
                             display.GetComponent<Image>().color = new Color(r,g,b,a);
                             //float x = display.localScale.x/1.005f;
                             //float y = display.localScale.y/1.005f;
                             //float z = display.localScale.z;
                             //display.localScale = new Vector3(x, y, z);
                     }

                     if (energyTimer <= 0){
                            thingOn = false;                                   // time has run out
                     }
              } else if (thingOn == false){
                     if (energyTimer < energyMax) {
                            energyTimer += 0.01f;
                            Debug.Log("energy up: " + energyTimer);  //replace with the desired ability display

                            Color oldCol = display.GetComponent<Image>().color;
                            if (oldCol.a < 1f) {
                                   float r = oldCol.r + 0.01f;
                                   float g = oldCol.g + 0.01f;
                                   float b = oldCol.b + 0.01f;
                                   float a = oldCol.a + 0.01f;
                                   display.GetComponent<Image>().color = new Color(r,g,b,a);
                                   //float x = display.localScale.x * 1.005f;
                                   //float y = display.localScale.y * 1.005f;
                                   //float z = display.localScale.z;
                                   //display.localScale = new Vector3(x, y, z);
                            }
                     }
              }
        }
}