using UnityEngine;
using System.Collections;

public class explosionController : MonoBehaviour {
    public Light exLight;
    public float power;
    public float radius;
    public float damage;

	// Use this for initialization
	void Start () {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

        foreach (Collider hit in colliders) {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb) {
                rb.AddExplosionForce(power, explosionPos, radius, 3.0f, ForceMode.Impulse);
            }
            if (hit.tag == "Player") {
                playerHealth playerHP = hit.gameObject.GetComponent<playerHealth>();
                playerHP.addDamage(damage);
            }
            else if (hit.tag == "Enemy") {
                enemyHealth enemyHP = hit.gameObject.GetComponent<enemyHealth>();
                enemyHP.addDamage(damage);
            }
        }

        exLight = GetComponentInChildren<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        exLight.intensity = Mathf.Lerp(exLight.intensity, 0f, 5 * Time.time);
	}
}