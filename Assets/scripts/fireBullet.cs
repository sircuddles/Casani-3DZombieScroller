using UnityEngine;
using System.Collections;

public class fireBullet : MonoBehaviour {
    [SerializeField] private float timeBetweenBullets;
    [SerializeField] private GameObject projectile;
    private float nextBullet;
    private playerController myPC;

    // Use this for initialization
    void Awake () {
        nextBullet = 0f;
        myPC = transform.root.GetComponent<playerController>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetAxisRaw("Fire1") > 0 && nextBullet < Time.time) {
            nextBullet = Time.time + timeBetweenBullets;
            Vector3 rot;

            if (myPC.GetFacing() == -1)
                rot = new Vector3(0, -90, 0);
            else rot = new Vector3(0, 90, 0);

            Instantiate(projectile, transform.position, Quaternion.Euler(rot));
        }
	}
}
