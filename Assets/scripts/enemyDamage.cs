﻿using UnityEngine;
using System.Collections;

public class enemyDamage : MonoBehaviour {
    public float damage;
    public float damageRate;
    public float pushBackForce;

    float nextDamage;

    private bool playerInRange = false;

    GameObject thePlayer;
    playerHealth thePlayerHealth;

	// Use this for initialization
	void Start () {
        nextDamage = Time.time;

        thePlayer = GameObject.FindGameObjectWithTag("Player");
        thePlayerHealth = thePlayer.GetComponent<playerHealth>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (playerInRange) {
            Attack();
        }
	}

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            playerInRange = true;
        }
    }
    void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            playerInRange = false;
        }
    }

    void Attack() {
        if (nextDamage <= Time.time) {
            thePlayerHealth.addDamage(damage);
            nextDamage = Time.time + damageRate;
            pushBack(thePlayer.transform);
        }
    }

    void pushBack(Transform t) {
        Vector3 pushDir = new Vector3(0, (t.position.y - transform.position.y), 0).normalized;
        pushDir *= pushBackForce;

        Rigidbody pushedRB = t.GetComponent<Rigidbody>();
        pushedRB.velocity = Vector3.zero;
        pushedRB.AddForce(pushDir, ForceMode.Impulse);
    }
}
