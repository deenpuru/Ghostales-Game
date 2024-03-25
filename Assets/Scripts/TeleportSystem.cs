using Cinemachine;
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
    [SerializeField] private float _movementDelay;

    [SerializeField] private bool _PlayerWalkingUp;
    [SerializeField] private bool _PlayerWalkingDown;
    [SerializeField] private bool _PlayerWalkingRight;
    [SerializeField] private bool _PlayerWalkingLeft;

    [SerializeField] private float _gizmosSize = 1;
    [SerializeField] private Vector3 _offsetPosition;
    [SerializeField] private Color _gizmosColor;

    [SerializeField] private CinemachineConfiner2D _confiner2D;
    [SerializeField] private PolygonCollider2D _MapBounding;


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
        yield return new WaitForSeconds(_movementDelay);

        if (_PlayerWalkingUp)
        {
            _kid.movement.y = 1;
        }
        else if (_PlayerWalkingDown)
        {
            _kid.movement.y = -1;
        }
        else if (_PlayerWalkingRight)
        {
            _kid.movement.x = 1;
        }
        else if (_PlayerWalkingLeft)
        {
            _kid.movement.x = -1;
        }


        _kid.animator.SetFloat("Vertical", _kid.movement.y);
        _kid.animator.SetFloat("Horizontal", _kid.movement.x);
        _kid.animator.SetFloat("Speed", 1);

        _playableDirector.playableAsset = _hide;
        _playableDirector.Play();

        yield return new WaitForSeconds((float)_hide.duration + _animDelay);

        _confiner2D.m_BoundingShape2D = _MapBounding;

        _kid.transform.position = new Vector3(_pointToGo.position.x + _offsetPosition.x, _pointToGo.position.y + _offsetPosition.y, _pointToGo.position.z + _offsetPosition.z);
        _kid.IsAbleToMove = true;

        yield return new WaitForSeconds(0.2f);

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
