using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class enemyHealth : MonoBehaviour {
    public float maxHealth;
    public float damageModifier;

    public GameObject damagePS;
    public GameObject drop;
    public bool drops;

    public AudioClip deathSound;
    public bool canBurn;
    public float burnDamage;
    public GameObject burnFX;

    bool onFire;
    public float burnTime;
    float burnInterval = 1f;
    float nextBurn;
    float endBurn;

    float currentHealth;
    public Slider enemyHealthIndicator;
    AudioSource enemyAS;

	// Use this for initialization
	void Awake () {
        currentHealth = maxHealth;
        enemyHealthIndicator.maxValue = maxHealth;
        enemyHealthIndicator.value = currentHealth;
        enemyAS = GetComponent<AudioSource>();
        if (burnFX) burnFX.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	    if (onFire && Time.time > nextBurn) {
            addDamage(burnDamage);
            nextBurn += burnInterval;
        }

        if (onFire && Time.time > endBurn) {
            onFire = false;
            burnFX.gameObject.SetActive(false);
        }
	}

    public void addDamage(float dmg) {
        enemyHealthIndicator.gameObject.SetActive(true);
        dmg = damageModifier * dmg;

        if (dmg <= 0) return;

        currentHealth -= dmg;
        UpdateSlider();
        enemyAS.Play();

        if (currentHealth <= 0) {
            makeDead();
        }
    }

    public void damageFX(Vector3 point, Vector3 rotation) {
        Instantiate(damagePS, point, Quaternion.Euler(rotation));
    }

    public void addFire() {
        if (!canBurn) return;

        onFire = true;
        burnFX.gameObject.SetActive(true);
        endBurn = Time.time + burnTime;
        nextBurn = Time.time + burnInterval;
    }

    void makeDead() {
        zombieController aZombie = GetComponentInChildren<zombieController>();
        if (aZombie) {
            aZombie.RagdollDeath();
        }


        Destroy(gameObject.transform.root.gameObject);
        AudioSource.PlayClipAtPoint(deathSound, transform.position, 0.15f);
        if (drops) {
            Instantiate(drop, transform.position, Quaternion.identity);
        }
    }

    void UpdateSlider() {
        enemyHealthIndicator.value = currentHealth;
    }
}
