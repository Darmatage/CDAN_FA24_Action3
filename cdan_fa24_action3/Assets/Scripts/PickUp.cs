using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour{

      private GameHandler gameHandler;
	  private RechargeTimer rechargeTimer;
      //public playerVFX playerPowerupVFX;
	  public bool isWebRecharge = true;
      public bool isHealthPickUp = false;
      public bool isSpeedBoostPickUp = false;

	public float webChargeBoost = 1f;
	public int healthBoost = 50;
	public float speedBoost = 2f;
	public float speedTime = 2f;

      void Start(){
            gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
			rechargeTimer = GameObject.FindWithTag("WebCharger").GetComponent<RechargeTimer>();
            //playerPowerupVFX = GameObject.FindWithTag("Player").GetComponent<playerVFX>();
      }

      public void OnTriggerEnter2D (Collider2D other){
            if (other.gameObject.tag == "Player"){
                  GetComponent<Collider2D>().enabled = false;
                  //GetComponent<AudioSource>().Play();
                  StartCoroutine(DestroyThis());

				if (isWebRecharge == true) {
                        rechargeTimer.UseWebEnergy(rechargeTimer.energyUsedInWeb *-1);;
                        //playerPowerupVFX.powerup();
                  }

                  if (isHealthPickUp == true) {
                        gameHandler.playerGetHit(healthBoost * -1);
                        //playerPowerupVFX.powerup();
                  }

                 // if (isSpeedBoostPickUp == true) {
                  //      other.gameObject.GetComponent<PlayerMoveAround>().speedBoost(speedBoost, speedTime);
                  //      playerPowerupVFX.powerup();
                 // }
            }
      }

      IEnumerator DestroyThis(){
            yield return new WaitForSeconds(0.3f);
            Destroy(gameObject);
      }

}