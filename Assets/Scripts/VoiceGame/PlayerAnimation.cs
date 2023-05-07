using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody2D _rb;

    private double _beforePosY;
    // Start is called before the first frame update
    void Start()
    {
        _beforePosY = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        _anim = GetComponent<Animator> ();
        _rb = GetComponent<Rigidbody2D> ();
    }

    private void OnTriggerExit2D(Collider2D target)
    {
        if (target.tag == "platform" ) {
            if (ScoreManager.instance != null) {
                _anim.SetBool("jump", true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "platform" ) {
            if (ScoreManager.instance != null) {
                _anim.SetBool("jump", false);
            }
        }
    }
}
