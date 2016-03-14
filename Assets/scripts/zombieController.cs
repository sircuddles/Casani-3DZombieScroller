using UnityEngine;
using System.Collections;

public class zombieController : MonoBehaviour {
    public GameObject flipModel;

    public AudioClip[] idleSounds;
    public float idleSoundTime;
    AudioSource enemyMovementAS;
    float nextIdleSound = 0f;

    public float detectionTime;
    float startRun;
    bool firstDetection;

    public float runSpeed;
    public float walkSpeed;
    public bool facingRight = true;

    float moveSpeed;
    bool running;

    Rigidbody mRigid;
    Animator mAnimator;
    Transform mDetectedPlayer;

    bool detected;

	// Use this for initialization
	void Start () {
        mRigid = GetComponentInParent<Rigidbody>();
        mAnimator = GetComponentInParent<Animator>();
        enemyMovementAS = GetComponent<AudioSource>();

        running = false;
        detected = false;
        firstDetection = false;
        moveSpeed = walkSpeed;

        if (Random.Range(0, 10) > 5) Flip();
	}

    void FixedUpdate() {
        if (detected) {
            if (mDetectedPlayer.position.x < transform.position.x && facingRight) {
                Flip();
            }
            else if (mDetectedPlayer.position.x > transform.position.x && !facingRight) {
                Flip();
            }

            if (!firstDetection) {
                startRun = Time.time + detectionTime;
                firstDetection = true;
            }
        }

        if (detected && !facingRight) mRigid.velocity = new Vector3(moveSpeed * -1, mRigid.velocity.y, 0);
        else if (detected && facingRight) mRigid.velocity = new Vector3(moveSpeed, mRigid.velocity.y, 0);

        if (!running && detected) {
            if (startRun < Time.time) {
                moveSpeed = runSpeed;
                mAnimator.SetTrigger("run");
                running = true;
            }
        }

        //idle and walk sounds
        if (!running) {
            if (Random.Range(0, 10) > 5 && nextIdleSound < Time.time) {
                AudioClip tmp = idleSounds[Random.Range(0, idleSounds.Length)];
                enemyMovementAS.clip = tmp;
                enemyMovementAS.Play();
                nextIdleSound = idleSoundTime + Time.time;
            }
        }
    }
	
	void OnTriggerEnter(Collider o) {
        if (o.tag == "Player" && !detected) {
            detected = true;
            mDetectedPlayer = o.transform;

            mAnimator.SetBool("detected", detected);

            if(mDetectedPlayer.position.x < transform.position.x && facingRight) {
                Flip();
            }
            else if (mDetectedPlayer.position.x > transform.position.x && !facingRight) {
                Flip();
            }

        }
    }

    void OnTriggerExit(Collider o) {
        if (o.tag == "Player") {
            firstDetection = false;
            if (running) {
                mAnimator.SetTrigger("run");
                moveSpeed = walkSpeed;
                running = false;
            }
        }
    }

    void Flip() {
        facingRight = !facingRight;

        Vector3 theScale = flipModel.transform.localScale;
        theScale.z *= -1;
        flipModel.transform.localScale = theScale;
    }
}
