using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player" ) {
            if (ScoreManager.instance != null) {
                ScoreManager.instance.IncrementScore ();
                Destroy(gameObject);
            }
        }
    }
}
