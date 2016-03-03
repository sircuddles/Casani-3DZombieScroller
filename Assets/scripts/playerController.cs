using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {
    [SerializeField] private float runSpeed;
    private Rigidbody mRigid;
    private Animator mAnimator;

    private bool facingRight;

	// Use this for initialization
	void Start () {
        mRigid = GetComponent<Rigidbody>();
        mAnimator = GetComponent<Animator>();

        facingRight = true;
    }
	
	// Update is called once per frame
	void Update () {
        float horizontalMove = Input.GetAxis("Horizontal");
        mAnimator.SetFloat("speed", Mathf.Abs(horizontalMove));

        mRigid.velocity = new Vector3(horizontalMove * runSpeed, mRigid.velocity.y, 0);

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
