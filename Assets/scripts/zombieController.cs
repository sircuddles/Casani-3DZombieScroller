using UnityEngine;
using System.Collections;

public class zombieController : MonoBehaviour {
    public GameObject flipModel;
    public GameObject ragdollDead;

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

    public void RagdollDeath() {
        GameObject ragdoll = (GameObject)Instantiate(ragdollDead, transform.root.position, Quaternion.identity);

        Transform ragdollMaster = ragdoll.transform.Find("master");
        Transform zombieMaster = transform.root.Find("master");

        bool wasFacingRight = true;
        if (!facingRight) {
            wasFacingRight = false;
            Flip();
        }

        Transform[] ragdollJoints = ragdollMaster.GetComponentsInChildren<Transform>();
        Transform[] currentJoints = zombieMaster.GetComponentsInChildren<Transform>();

        for (int i = 0; i < ragdollJoints.Length; i++) {
            for (int j = 0; j < currentJoints.Length; j++) {
                if (currentJoints[j].name.CompareTo(ragdollJoints[i].name) == 0) {
                    ragdollJoints[i].position = currentJoints[j].position;
                    ragdollJoints[i].rotation = currentJoints[j].rotation;
                    break;
                }
            }
        }

        if (wasFacingRight) {
            Vector3 rotVector = new Vector3(0, 0, 0);
            ragdoll.transform.rotation = Quaternion.Euler(rotVector);
        }
        else {
            Vector3 rotVector = new Vector3(0, 90, 0);
            ragdoll.transform.rotation = Quaternion.Euler(rotVector);
        }

        Transform zombieMesh = transform.root.transform.Find("zombieSoldier");
        Transform ragdollMesh = ragdoll.transform.Find("zombieSoldier");

        ragdollMesh.GetComponent<Renderer>().material = zombieMesh.GetComponent<Renderer>().material;
    }
}
