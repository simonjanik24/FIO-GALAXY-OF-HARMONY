using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentHarmonicSymphony : Collectable
{
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("It was the player!");
            CollectMe();
        }
    }

    public override void CollectMe()
    {
        Destroy(gameObject);
    }
}
