using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3D_Movement : MonoBehaviour
{ 
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotateSpeed = 10f;
    [SerializeField] private Animator _animator;
    private Vector3 _moveDirection;

    private void Update()
    {
        Vector2 _movement = new Vector2(0, 0);


            if (Input.GetKey(KeyCode.UpArrow))
            {
                _movement.y = -1;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                _movement.y = +1;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _movement.x = +1;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                _movement.x = -1;
            }

            _movement = _movement.normalized;

        if (_movement != Vector2.zero) 
        {
            _moveDirection = new Vector3(_movement.x, 0f, _movement.y);
            _animator.SetBool("isWalking", true);
            transform.position += _moveDirection * _moveSpeed * Time.deltaTime;
            transform.forward = Vector3.Slerp(transform.forward, _moveDirection, Time.deltaTime * _rotateSpeed);
        }

        else
        {
            _animator.SetBool("isWalking", false);
        }
    }
}

