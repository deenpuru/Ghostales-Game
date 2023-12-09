using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Cone_Trigger : MonoBehaviour
{
    
    [SerializeField] private PunkKid_MainPlayerControls kid;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PunkKid_Tag"))
        {
            StartCoroutine(WalkAway());
            
        }
    }
    

    IEnumerator WalkAway()
    {
        kid.IsAbleToMove = false;
        Debug.Log("Player is not walking");
        kid.movement.x = 0;
        kid.movement.y = 0;
        kid.animator.SetFloat("Speed", 0);
        yield return new WaitForSeconds(3f);

        kid.movement.x = 1;
        kid.movement.y = 0;
        kid.animator.SetFloat("Horizonal", kid.movement.x);
        kid.animator.SetFloat("Speed", 1);
        Debug.Log("walking right");
        yield return new WaitForSeconds(0.5f);

        kid.IsAbleToMove = true;
        Debug.Log("Player is walking");
    }

   
}
