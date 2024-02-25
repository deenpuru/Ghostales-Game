using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TeleportSystem : MonoBehaviour
{
    [SerializeField] private PunkKid_MainPlayerControls _kid;

    [SerializeField] private Animator _doorAnimator;

    [SerializeField] private PlayableDirector _playableDirector;
    [SerializeField] private PlayableAsset _show;
    [SerializeField] private PlayableAsset _hide;

    [SerializeField] private GameObject _transition_Container;

    [SerializeField] private float _animDelay = 3;

    [SerializeField] private bool _walkUp;

    [SerializeField] private float _gizmosSize = 1;
    [SerializeField] private Vector3 _offsetPosition;
    [SerializeField] private Color _gizmosColor;

    public Transform _pointToGo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PunkKid_Tag"))
        {
           if (_doorAnimator != null)
            {
                _doorAnimator.SetTrigger("Door_Opens");
            }
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

        if (_walkUp)
        {
            _kid.movement.y = 1;
        }
        else
        {
            _kid.movement.y = -1;
        }
        
        _kid.animator.SetFloat("Vertical", _kid.movement.y);
        _kid.animator.SetFloat("Speed", 1);

        _playableDirector.playableAsset = _hide;
        _playableDirector.Play();

        yield return new WaitForSeconds((float)_hide.duration + _animDelay );

        _kid.transform.position = new Vector3(_pointToGo.position.x + _offsetPosition.x, _pointToGo.position.y + _offsetPosition.y, _pointToGo.position.z + _offsetPosition.z);
        _kid.IsAbleToMove = true;

        _playableDirector.playableAsset = _show;
        _playableDirector.Play();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PunkKid_Tag"))
        {
            if (_doorAnimator != null)
            {
                _doorAnimator.SetTrigger("Door_Closes");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmosColor;
        Gizmos.DrawSphere(new Vector3(_pointToGo.position.x + _offsetPosition.x, _pointToGo.position.y + _offsetPosition.y, _pointToGo.position.z + _offsetPosition.z), _gizmosSize);
    }
}
