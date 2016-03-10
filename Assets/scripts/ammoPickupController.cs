using UnityEngine;
using System.Collections;

public class ammoPickupController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            other.GetComponentInChildren<fireBullet>().Reload();
        }
        Destroy(transform.root.gameObject);
    }
}
