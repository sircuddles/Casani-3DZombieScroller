using UnityEngine;
using System.Collections;

public class elevatorController : MonoBehaviour {
    public float resetTime;
    float downtime;
    Animator mAnim;
    AudioSource eleAS;

    bool elevIsUp = false;
    

	// Use this for initialization
	void Start () {
        mAnim = GetComponent<Animator>();
        eleAS = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (downtime <= Time.time && elevIsUp) {
            mAnim.SetTrigger("activateElevator");
            elevIsUp = false;
            eleAS.Play();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            mAnim.SetTrigger("activateElevator");
            downtime = Time.time + resetTime;
            elevIsUp = true;
            eleAS.Play();
        }
    }
}
