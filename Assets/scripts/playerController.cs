﻿using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {
    [SerializeField] private float runSpeed;
    [SerializeField] private float walkSpeed;

    private Rigidbody mRigid;
    private Animator mAnimator;

    private bool facingRight;

    // Jumping
    private bool grounded = false;
    private Collider[] groundCollisions;
    private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float jumpHeight;

	// Use this for initialization
	void Start () {
        mRigid = GetComponent<Rigidbody>();
        mAnimator = GetComponent<Animator>();

        facingRight = true;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        // Handle jump input and animation 
        if (grounded && Input.GetAxis("Jump") > 0) {
            grounded = false;
            mAnimator.SetBool("grounded", grounded);
            mRigid.AddForce(new Vector3(0, jumpHeight, 0));
        }
        
        // Ground collision
        groundCollisions = Physics.OverlapSphere(groundCheck.position, groundCheckRadius, groundLayer);
        if (groundCollisions.Length > 0) grounded = true;
        else grounded = false;

        mAnimator.SetBool("grounded", grounded);
        mAnimator.SetFloat("verticalSpeed", mRigid.velocity.y);

        // Handle Walk/Running 
        float horizontalMove = Input.GetAxis("Horizontal");
        mAnimator.SetFloat("speed", Mathf.Abs(horizontalMove));

        // Handle Sneaking
        float sneaking = Input.GetAxisRaw("Fire3");
        mAnimator.SetFloat("sneaking", sneaking);

        // Move player
        if (grounded && sneaking > 0) {
            mRigid.velocity = new Vector3(horizontalMove * walkSpeed, mRigid.velocity.y, 0);
        }
        else {
            mRigid.velocity = new Vector3(horizontalMove * runSpeed, mRigid.velocity.y, 0);
        }

        // Flip player if necessary
        if (horizontalMove > 0 && !facingRight)
            Flip();
        else if (horizontalMove < 0 && facingRight)
            Flip();
    }

    void Flip() {
        facingRight = !facingRight;

        Vector3 newScale = transform.localScale;
        newScale.z = -newScale.z;
        transform.localScale = newScale;
    }
}
