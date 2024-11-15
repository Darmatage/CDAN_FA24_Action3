using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint_Spores : MonoBehaviour{

	public GameObject sporeVFX;
	public GameObject normalMound;
	public GameObject moundMushrooms1;
	public GameObject moundMushrooms2;
	public GameObject moundMushrooms3;

	public bool isInfested = false;

    // Start is called before the first frame update
    void Start(){
		normalMound.SetActive(true);
		moundMushrooms1.SetActive(false);
		moundMushrooms2.SetActive(false);
		moundMushrooms3.SetActive(false);
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag=="Player"){
			if (isInfested==false){
				StartCoroutine(GrowShrooms(other.gameObject.transform.position));
			}
			isInfested = true;
		}
    }

	IEnumerator GrowShrooms(Vector3 playerPos){
		//instantiate the spores particle effect
		GameObject sporesBoom = Instantiate (sporeVFX, playerPos, Quaternion.identity);
		
		//after a second, destroy the partcle effect
		yield return new WaitForSeconds(1f);
		Destroy(sporesBoom);

		//after half a second, start to grow the mushrooms (swap the spite)
		yield return new WaitForSeconds(0.5f);
		normalMound.SetActive(false);
		moundMushrooms1.SetActive(true);

		yield return new WaitForSeconds(0.5f);
		moundMushrooms1.SetActive(false);
		moundMushrooms2.SetActive(true);

		yield return new WaitForSeconds(0.5f);
		moundMushrooms2.SetActive(false);
		moundMushrooms3.SetActive(true);
	}

}
