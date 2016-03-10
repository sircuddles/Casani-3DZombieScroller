using UnityEngine;
using System.Collections;

public class garageDoorController : MonoBehaviour {
    public bool firstTrigger = false;
    public bool resetable;
    public bool startOpen;

    public GameObject door;
    public GameObject gear;

    Animator doorAnim;
    Animator gearAnim;

    AudioSource doorAudio;

    bool open = true;

	// Use this for initialization
	void Start () {
        doorAnim = door.GetComponent<Animator>();
        gearAnim = gear.GetComponent<Animator>();
        doorAudio = GetComponent<AudioSource>();

        if (!startOpen) {
            open = false;
            doorAnim.SetTrigger("doorTrigger");
            doorAudio.Play();
        }
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && !firstTrigger) {
            if (!resetable) firstTrigger = true;
            doorAnim.SetTrigger("doorTrigger");
            doorAudio.Play();
            open = !open;
            gearAnim.SetTrigger("gearRotate");
        }
    }
}
