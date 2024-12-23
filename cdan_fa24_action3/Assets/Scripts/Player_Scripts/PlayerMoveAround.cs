using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerMoveAround : MonoBehaviour {

      public Animator anim;
      public AudioSource WalkSFX;
      public Rigidbody2D rb2D;
      public bool FaceRight = false; // determine which way player is facing. Leave public for other scripts to access.
      public static float runSpeed = 5f;
      public float startSpeed = 5f;
      public bool isAlive = true;

		public bool walkUp = false;
		public bool walkDown = false;
		public bool walkSide = false;

      void Start(){
           anim = gameObject.GetComponentInChildren<Animator>();
           rb2D = transform.GetComponent<Rigidbody2D>();
      }

      void Update(){
            //NOTE: Horizontal axis: [a] / left arrow is -1, [d] / right arrow is 1
            //NOTE: Vertical axis: [w] / up arrow, [s] / down arrow
            Vector3 hvMove = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
           if (isAlive == true){
                  transform.position = transform.position + hvMove * runSpeed * Time.deltaTime;

                  if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0)){
					anim.SetBool ("Walk", true);

					if ((Input.GetAxis("Vertical") < 0.1f) && 
					((Input.GetAxis("Horizontal") >= 0.1f)||(Input.GetAxis("Horizontal") <= -0.1f))){
						anim.SetBool ("WalkDown", false);
						anim.SetBool ("WalkUp", false);
						anim.SetBool ("WalkSide", true);

						walkUp = false;
						walkDown = false;
						walkSide = true;
					}

					else if (Input.GetAxis("Vertical") < 0){
						anim.SetBool ("WalkDown", true);
						anim.SetBool ("WalkUp", false);
						anim.SetBool ("WalkSide", false);

						walkUp = false;
						walkDown = true;
						walkSide = false;
					}
					else if (Input.GetAxis("Vertical") > 0){
					//else {
						anim.SetBool ("WalkDown", false);
						anim.SetBool ("WalkUp", true);
						anim.SetBool ("WalkSide", false);

						walkUp = true;
						walkDown = false;
						walkSide = false;
					}
					
                  	
                        if (!WalkSFX.isPlaying){
                             WalkSFX.Play();
                        }

                  } else {
                   	anim.SetBool ("Walk", false);
					anim.SetBool ("WalkUp", false);
					anim.SetBool ("WalkDown", false);
					anim.SetBool ("WalkSide", false);
						walkUp = false;
						walkDown = false;
						walkSide = false;
                        WalkSFX.Stop();
                 }

                  // Turning. Reverse if input is moving the Player right and Player faces left.
                 if ((hvMove.x >0 && !FaceRight) || (hvMove.x <0 && FaceRight)){
                        playerTurn();
                  }
            }
      }

      private void playerTurn(){
            // NOTE: Switch player facing label
            FaceRight = !FaceRight;

            // NOTE: Multiply player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
      }
}