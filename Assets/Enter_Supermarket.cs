using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter_Supermarket : MonoBehaviour
{
    [SerializeField] private PunkKid_MainPlayerControls kid;

    [SerializeField] private Animator _supermarketAnimation;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PunkKid_Tag"))
        {
            _supermarketAnimation.SetTrigger("Door_Opens");

            StartCoroutine(WalkInside());
        }

    }

    IEnumerator WalkInside()
    {
        kid.IsAbleToMove = false;
        kid.movement.x = 0;
        kid.movement.y = 0;
        kid.animator.SetFloat("Speed", 0);
        yield return new WaitForSeconds(1.5f);

        kid.movement.x = 0;
        kid.movement.y = 1;
        kid.animator.SetFloat("Vertical", kid.movement.y);
        kid.animator.SetFloat("Speed", 1);
        yield return new WaitForSeconds(2f);
        kid.IsAbleToMove = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PunkKid_Tag"))
        {
            _supermarketAnimation.SetTrigger("Door_Closes");
        }
    }

}
