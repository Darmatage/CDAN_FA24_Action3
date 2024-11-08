using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebShooting : MonoBehaviour{

//web AIMING:
	private Rigidbody2D rb;
	private Camera cam;
	private Vector2 mousePos;

//web SHOOTING:
	public GameObject webPrefab;
	public Transform webShooter;
	public Transform webShooterBase;

    // Start is called before the first frame update
    void Awake(){
		//assign rigidbody2D and camera to variables for AIMING:
		rb = GetComponent <Rigidbody2D>();
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
    }

    // Update is called once per frame
    void Update(){
		//mouse location for AIMING
		mousePos = cam.ScreenToWorldPoint (Input.mousePosition);
        
		//add a player Input listener for SHOOTING:
		if (Input.GetButtonDown("Fire1")){
			WebShoot();
		}
    }

	//for AIMING:
	void FixedUpdate(){
    //actual rotation uses vector math to calculate angle, then rotates character to mouse
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg -90f;
    //rb.rotation = angle;
		webShooterBase.rotation=Quaternion.FromToRotation(Vector3.up, lookDir);
      }

	//for SHOOTING:
	public void WebShoot(){
	//Get the location of the shooter, the distance to the mouseclick, and the midpoint of that distance
		Vector2 shootPoint = webShooter.position;
		float webLength = Vector2.Distance(shootPoint, mousePos);
		Vector2 midPoint = ((shootPoint - mousePos) * 0.5f) + mousePos;

	//SPAWN the new web:
		GameObject newWeb = Instantiate(webPrefab, shootPoint, Quaternion.identity);
	//SCALE: set length to distance between the two points (assumes starting length=1):
		newWeb.transform.localScale = new Vector2(newWeb.transform.localScale.x, newWeb.transform.localScale.y * webLength);
	//POSITION: center web at midpoint between two points
		newWeb.transform.position = midPoint;
	//ROTATION: change orentation to the direction of the mouseclick from the player position 
		Vector2 direction = mousePos - shootPoint;
        newWeb.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
	}



}

//apply this script to the WebShooter object on the player
//rotates the webshooter object based on mouse direction