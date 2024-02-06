using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enter_Supermarket : MonoBehaviour
{
    [SerializeField] private PunkKid_MainPlayerControls _kid;

    [SerializeField] private Animator _supermarketAnimation;
    public Transform _pointToGo;

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
        _kid.IsAbleToMove = false;
        _kid.movement.x = 0;
        _kid.movement.y = 0;
        _kid.animator.SetFloat("Speed", 0);
        yield return new WaitForSeconds(1.5f);

        _kid.movement.x = 0;
        _kid.movement.y = 1;
        _kid.animator.SetFloat("Vertical", _kid.movement.y);
        _kid.animator.SetFloat("Speed", 1);
        yield return new WaitForSeconds(1f);
        _kid.transform.position = _pointToGo.position;
        _kid.IsAbleToMove = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PunkKid_Tag"))
        {
            _supermarketAnimation.SetTrigger("Door_Closes");
        }
    }
}
