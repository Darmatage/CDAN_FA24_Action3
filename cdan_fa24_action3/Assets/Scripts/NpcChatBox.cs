using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NpcChatBox : MonoBehaviour
{
    public GameObject ChatOn;
        

    void Start(){
        ChatOn.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){ 
                ChatOn.SetActive(true);
            }
        }

    void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            ChatOn.SetActive(false); 
        }
    }

    

}