using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCrackerScript : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    void OnBecameVisible()
    {
        anim.SetBool("fire", true);
    }
}
