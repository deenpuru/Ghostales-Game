using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3D_Movement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _rotationSpeed;

    private Rigidbody rb;
    private Vector3 movement;



    // Update is called once per frame
    void Update()
    {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.z = Input.GetAxisRaw("Vertical");

            movement = new Vector3(movement.x, 0, movement.z);
            movement.Normalize(); // Fixes the diagonal speed ratio

            transform.Translate(movement * _moveSpeed * Time.deltaTime,Space.World);

            if (movement != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
            }

            /* animator.SetFloat("Horizonal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude); 

            if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Vertical") == -1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1)
            {
                animator.SetFloat("LastHorizontal", Input.GetAxisRaw("Horizontal"));
                animator.SetFloat("LastVertical", Input.GetAxisRaw("Vertical")); 
            } */
    }

    void FixedUpdate()
    {
       // rb.MovePosition(rb.position + movement * _moveSpeed * Time.fixedDeltaTime);
    }
}
