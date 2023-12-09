using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunkKid_MainPlayerControls : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Rigidbody2D rb;
    public Vector2 movement;
    public Animator animator;
    public bool IsAbleToMove;
    

    // Update is called once per frame
    void Update()
    {
        if (IsAbleToMove)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            
            animator.SetFloat("Horizonal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }  
        
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
