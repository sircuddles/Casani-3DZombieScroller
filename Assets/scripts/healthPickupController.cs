using UnityEngine;
using System.Collections;

public class healthPickupController : MonoBehaviour {
    public float healthAmount;

    public AudioClip healthSound;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            other.GetComponent<playerHealth>().addHealth(healthAmount);
            Destroy(transform.root.gameObject);
            AudioSource.PlayClipAtPoint(healthSound, transform.position, 0.4f);
        }

    }
}
