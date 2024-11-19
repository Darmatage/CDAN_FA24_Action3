using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickAiming : MonoBehaviour{

	public Transform turnPoint;

    void Update(){
        //Vector3 angle = turnPoint.transform.localEulerAngles;
		float horizontalAxis = Input.GetAxis("HorizontalRightStick");
		float verticalAxis = Input.GetAxis("VerticalRightStick");
		float dirMult = 1;
		if (horizontalAxis != 0f || verticalAxis != 0f){
			if (gameObject.GetComponent<PlayerMoveAround>().FaceRight){dirMult=1;}
			else {dirMult=-1;}
		}
		turnPoint.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Atan2(horizontalAxis *dirMult,verticalAxis *-1) * Mathf.Rad2Deg);

	}

}

//twistPoint.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Atan2(horizontalAxis,verticalAxis)*-100/Mathf.PI -90);
