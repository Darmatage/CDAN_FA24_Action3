using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerBottomRespawn : MonoBehaviour {

       public GameHandler gameHandler;
       public Transform playerPos;
       public Transform pSpawnFall;
       public int damage = 10;

       void Start() {
              playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
              gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
       }

       void Update() {
              if (playerPos != null){
                     //Capture Checkpoint changes:
                     pSpawnFall = playerPos.gameObject.GetComponent<PlayerRespawn>().pSpawn;

                     if (transform.position.y >= playerPos.position.y){
                            //instantiate a particle effect
                            Debug.Log("I am going back to the start");
                            gameHandler.playerGetHit(damage);
                            Vector3 pSpn2 = new Vector3(pSpawnFall.position.x, pSpawnFall.position.y, playerPos.position.z);
                            playerPos.position = pSpn2;
                     }
              }
       }
}
