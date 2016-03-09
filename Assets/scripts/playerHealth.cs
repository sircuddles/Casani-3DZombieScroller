using UnityEngine;
using System.Collections;

public class playerHealth : MonoBehaviour {
    public float maxHealth;
    public float currentHealth;

    public GameObject playerDeathFX;

    void Start() {
        currentHealth = maxHealth;
    }

    public void addDamage(float dmg) {
        currentHealth -= dmg;

        if (currentHealth <= 0) {
            makeDead();
        }
    }

    public void makeDead() {
        Instantiate(playerDeathFX, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        Destroy(gameObject);

    }
}
