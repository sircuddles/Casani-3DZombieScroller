using UnityEngine;
using System.Collections;

public class shootFireball : MonoBehaviour {
    public float damage;
    public float speed;

    private Rigidbody mRigid;

	// Use this for initialization
	void Start () {
        mRigid = GetComponentInParent<Rigidbody>();

        // Set the correct direction for the projectile
        if (transform.rotation.y > 0) mRigid.AddForce(Vector3.right * speed, ForceMode.Impulse);
        else mRigid.AddForce(Vector3.left * speed, ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy" || other.gameObject.layer == LayerMask.NameToLayer("Shootable")) {
            mRigid.velocity = Vector3.zero;
            enemyHealth eHP = other.GetComponent<enemyHealth>();
            if (eHP) {
                eHP.addDamage(damage);
                eHP.addFire();
            }

            Destroy(gameObject);
        }
    }
}
