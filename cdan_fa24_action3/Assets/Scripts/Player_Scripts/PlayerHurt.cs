using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerHurt : MonoBehaviour
{

    //private Animator anim;
    private Rigidbody2D rb2D;

    void Start()
    {
        //anim = gameObject.GetComponentInChildren<Animator>();
        rb2D = transform.GetComponent<Rigidbody2D>();
    }

    public void playerHit()
    {
        //anim.SetTrigger ("GetHurt");
    }

    public void playerDead()
    {
        rb2D.isKinematic = true;
        //anim.SetTrigger ("Dead");
    }
}