using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PickUp_Pulse : MonoBehaviour{

      public GameObject fadeHighlight;
      private float pulseHold = 0.4f;
      private float pulseDelay = 2f;
      private float pulseSpeed = 0.02f;
      private Renderer fadeHighlightRend;
      private bool timeToFadeOut = false;
      private bool timeToFadeIn = false;
      private float fadeAlpha = 0f;

      void Awake(){
            fadeHighlightRend = fadeHighlight.GetComponent<SpriteRenderer> ();
            fadeHighlightRend.material.color = new Color(2.5f, 2.2f, 0.3f, 0f);
      }

      void Start(){
            StartCoroutine(RandomDelay());
      }

      void FixedUpdate(){
            if (timeToFadeIn){
                  fadeAlpha += pulseSpeed;
                  fadeHighlightRend.material.color = new Color(2.5f, 2.2f, 0.3f, fadeAlpha);
                  if (fadeAlpha >= 0.5f){
                       fadeAlpha = 0.5f;
                        StartCoroutine(PulseFull());
                  }
            }
           else if (timeToFadeOut){
                  fadeAlpha -= pulseSpeed;
                  fadeHighlightRend.material.color = new Color(2.5f, 2.2f, 0.3f, fadeAlpha);
                  if (fadeAlpha <= 0f){
                        fadeAlpha = 0f;
                        StartCoroutine(PulsePause());
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