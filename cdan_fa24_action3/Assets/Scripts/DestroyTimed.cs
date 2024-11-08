using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimed : MonoBehaviour{
	public float timeToDestroy;
	public GameObject objectArt;
	public bool fadeAway = true;

    void Start(){
		StartCoroutine(DestroyMeTime(timeToDestroy));
    }

    IEnumerator DestroyMeTime(float timeDestroy){
		yield return new WaitForSeconds((timeDestroy/4) * 3);
		if (fadeAway){
			StartCoroutine(FadeOut(objectArt));
		}
		yield return new WaitForSeconds(timeDestroy/4);
		Destroy(gameObject);
    }

	IEnumerator FadeOut(GameObject fadeArt){
		float alphaLevel = 1;
		fadeArt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alphaLevel);
		for(int i = 0; i < timeToDestroy*100; i++){
			alphaLevel -= timeToDestroy/800;
			yield return null;
			fadeArt.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alphaLevel);
			//Debug.Log("Alpha is: " + alphaLevel);
		}
	} 
}

//NOTE: This can be enhanced with a reference to the renderer that fades the opacity shortly before destroying 