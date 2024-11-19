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

//target:
	public GameObject webTarget;
	public float maxDistance = 4f;
	private bool targetMoving = false;
	public float targetSpeed = 6f;


    // Start is called before the first frame update
    void Awake(){
		//assign rigidbody2D and camera to variables for AIMING:
		rb = GetComponent <Rigidbody2D>();
		cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>() as Camera;
		webTarget.SetActive(false);
    }

    // Update is called once per frame
    void Update(){
		//mouse location for AIMING
		mousePos = cam.ScreenToWorldPoint (Input.mousePosition);
        
		//add a player Input listener for setting aim distance:
		if (Input.GetButton("Fire1")){
			//activate the webTarget: 
			webTarget.SetActive(true);
			targetMoving = true;
		}

		//add a player Input listener for SHOOTING:
		if (Input.GetButtonUp("Fire1")){
			WebShoot();
			//reset the webTarget: 
			webTarget.transform.position = webShooter.position;
			webTarget.SetActive(false);
			targetMoving = false;
		}
    }

	//for AIMING:
	void FixedUpdate(){
		//target movement:
		if (targetMoving==true){
			Vector2 maxPos = webShooter.position + ((webShooter.position - webShooterBase.position).normalized * maxDistance);
			//Vector2 targetpos = Vector2.Lerp ((Vector2)webShooter.position, maxPos, targetSpeed * Time.fixedDeltaTime);
            //webTarget.transform.position = new Vector3 (targetpos.x, targetpos.y, transform.position.z);
			webTarget.transform.position = Vector2.MoveTowards ((Vector2)webTarget.transform.position, maxPos, targetSpeed * Time.fixedDeltaTime);
		}

		//joystick:
		float horizAxis = Input.GetAxis("HorizontalRightStick");
		float vertAxis = Input.GetAxis("VerticalRightStick");
		float dirMult = 1;
		//if joystick values are non-zero, use joystcik:
		if (horizAxis != 0f || vertAxis != 0f){
			//see if we re facing right or left:
			if (gameObject.GetComponent<PlayerMoveAround>().FaceRight){dirMult=-1;}
			else {dirMult=1;}
			//rotate to base by joystick input:
			webShooterBase.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Atan2(horizAxis *dirMult,vertAxis *-1) * Mathf.Rad2Deg);
		}
		//otherwise, use mouse:
		else {
  			//actual rotation uses vector math to calculate angle, then rotates firepoint to mouse
        	Vector2 lookDir = mousePos - rb.position;
        	float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg -90f;
    		//rb.rotation = angle;
			webShooterBase.rotation=Quaternion.FromToRotation(Vector3.up, lookDir);
		}
      }

	//for SHOOTING:
	public void WebShoot(){
	//Get the location of the shooter, the distance to the mouseclick, and the midpoint of that distance
		Vector2 shootPoint = webShooter.position;

		//NEW MOUSE-HOLD TO GET DISTANCE:
		float webLength = Vector2.Distance(shootPoint, webTarget.transform.position);
		Vector2 midPoint = ((shootPoint - (Vector2)webTarget.transform.position) * 0.5f) + (Vector2)webTarget.transform.position;

	//SPAWN the new web:
		GameObject newWeb = Instantiate(webPrefab, shootPoint, Quaternion.identity);
	//SCALE: set length to distance between the two points (assumes starting length=1):
		newWeb.transform.localScale = new Vector2(newWeb.transform.localScale.x, newWeb.transform.localScale.y * webLength);
	//POSITION: center web at midpoint between two points
		newWeb.transform.position = midPoint;
	//ROTATION: change orentation to the direction of the mouseclick from the player position 
		Vector2 direction = (Vector2)webTarget.transform.position - shootPoint;
        newWeb.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);


		/*
		//OLD MOUSECLICK:
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

		*/

	}

}

//apply this script to the WebShooter object on the player
//rotates the webshooter object based on mouse direction