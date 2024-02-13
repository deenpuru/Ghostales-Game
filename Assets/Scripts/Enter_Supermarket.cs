using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Enter_Supermarket : MonoBehaviour
{
    [SerializeField] private PunkKid_MainPlayerControls _kid;

    [SerializeField] private Animator _supermarketAnimation;

    [SerializeField] private PlayableDirector _playableDirector;
    [SerializeField] private PlayableAsset _show;
    [SerializeField] private PlayableAsset _hide;

    [SerializeField] private GameObject _transition_Container;

    [SerializeField] private float _animDelay = 3;
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

        
        _playableDirector.playableAsset = _hide;
        _playableDirector.Play();

        yield return new WaitForSeconds((float)_hide.duration + _animDelay );

        

        _kid.transform.position = _pointToGo.position;
        _kid.IsAbleToMove = true;

        _playableDirector.playableAsset = _show;
        _playableDirector.Play();


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PunkKid_Tag"))
        {
            _supermarketAnimation.SetTrigger("Door_Closes");
        }
    }
}
