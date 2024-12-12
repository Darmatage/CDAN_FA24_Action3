using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class NPC_PatrolSequencePoints : MonoBehaviour {
       // private Animator anim;
       public float speed = 10f;
       private float waitTime;
       public float startWaitTime = 2f;

       public Transform[] moveSpots;
       public int startSpot = 0;
       public bool moveForward = true;

       // Turning
       private int nextSpot;
       private int previousSpot;
       public bool faceRight = false;

        public Animator anim;
        public Rigidbody2D rb2D;
        public AudioSource SlimySFX;
        private Transform target;
        public int damage = 10;

        public bool inRange = false;
        public int EnemyLives = 3;
        private GameHandler gameHandler;

        public float attackRange = 10;
        public bool isAttacking = false;
        private float scaleX;

	public bool isWebbed = false;
       
	 float knockBackForce = 6f; 

	void Start(){
              waitTime = startWaitTime;
              nextSpot = startSpot;
              //anim = gameObject.GetComponentInChildren<Animator>();

              anim = GetComponentInChildren<Animator> ();
              rb2D = GetComponent<Rigidbody2D> ();
              scaleX = gameObject.transform.localScale.x;

            if (GameObject.FindGameObjectWithTag ("Player") != null) {
                 target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
            }

            if (GameObject.FindWithTag ("GameHandler") != null) {
              gameHandler = GameObject.FindWithTag ("GameHandler").GetComponent<GameHandler> ();
            }
       }

	void Update(){

              float DistToPlayer = Vector3.Distance(transform.position, target.position);
              
                if ((target != null) && (DistToPlayer <= attackRange) && (!isWebbed)){
                    
                    transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
                    //anim.SetBool("Walk", true);
                    //flip enemy to face player direction. Wrong direction? Swap the * -1.

                    if (target.position.x > gameObject.transform.position.x){
                    
                        gameObject.transform.localScale = new Vector2(scaleX, gameObject.transform.localScale.y);
                    } else {
                        gameObject.transform.localScale = new Vector2(scaleX * -1, gameObject.transform.localScale.y);
                    }

                }

		else if (!isWebbed){
                     transform.position = Vector2.MoveTowards(transform.position, moveSpots[nextSpot].position, speed * Time.deltaTime);

                     if (Vector2.Distance(transform.position, moveSpots[nextSpot].position) < 0.2f){
                            if (waitTime <= 0){
                                   if (moveForward == true){ previousSpot = nextSpot; nextSpot += 1; }
                                   else if (moveForward == false){ previousSpot = nextSpot; nextSpot -= 1; }
                                   waitTime = startWaitTime;
                            } else {
                                   waitTime -= Time.deltaTime;
                            }
                     }

                     //switch movement direction
                     if (nextSpot == 0) {moveForward = true; }
                     else if (nextSpot == (moveSpots.Length -1)) { moveForward = false; }

                     //turning the enemy
                     if (previousSpot > 0){ previousSpot = moveSpots.Length -1; }
                     else if (previousSpot < moveSpots.Length -1){ previousSpot = 0; }

                     if ((previousSpot == 0) && (faceRight)){ NPCTurn(); }
                     else if ((previousSpot == (moveSpots.Length -1)) && (!faceRight)) { NPCTurn(); }
                     // NOTE1: If faceRight does not change, try reversing !faceRight, above
                     // NOTE2: If NPC faces the wrong direction as it moves, set the sprite Scale X = -1.
              }

	}


	private void NPCTurn(){
		// NOTE: Switch player facing label (avoids constant turning)
		faceRight = !faceRight;

		// NOTE: Multiply player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Web"){
			isWebbed = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Web"){
			isWebbed = false;
		}
	}

        public void OnCollisionEnter2D(Collision2D other){
                if (other.gameObject.tag == "Player") {
                    isAttacking = true;
                    //anim.SetBool("Attack", true);
                    gameHandler.playerGetHit(damage);

					 //Tell the player to STOP getting knocked back before getting knocked back:
					other.gameObject.GetComponent<Player_EndKnockBack>().EndKnockBack();
					StartCoroutine(EndKnockBack());
                     //Add force to the player, pushing them back without teleporting:
                    Rigidbody2D pushRB = other.gameObject.GetComponent<Rigidbody2D>();
                    Vector2 moveDirectionPush = rb2D.transform.position - other.transform.position;
                    pushRB.AddForce(moveDirectionPush.normalized * knockBackForce * - 1f, ForceMode2D.Impulse);
					//push myself:
					rb2D.AddForce(moveDirectionPush.normalized * knockBackForce/2, ForceMode2D.Impulse);
                }
        }

        public void OnCollisionExit2D(Collision2D other){
                if (other.gameObject.tag == "Player") {
                     isAttacking = false;
                     //anim.SetBool("Attack", false);
                }
        }

        //DISPLAY the range of enemy's attack when selected in the Editor
        void OnDrawGizmosSelected(){
                Gizmos.DrawWireSphere(transform.position, attackRange);
       }

	   IEnumerator EndKnockBack(){
              yield return new WaitForSeconds(0.2f);
              rb2D.velocity= new Vector3(0,0,0);
       }

}