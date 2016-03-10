using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour {
    public float maxHealth;
    public float currentHealth;

    public GameObject playerDeathFX;
    AudioSource playerAS;

    //HUD
    public Slider playerHealthSlider;
    public Image damageOverlay;
    Color flashColor = new Color(255f, 255f, 255f, 1f);
    float flashSpeed = 5f;
    bool damaged = false;


    void Start() {
        currentHealth = maxHealth;
        playerHealthSlider.maxValue = maxHealth;
        playerHealthSlider.value = currentHealth;
        playerHealthSlider.minValue = 0;

        playerAS = GetComponent<AudioSource>();
    }

    void Update() {
        if (damaged) {
            damageOverlay.color = flashColor;
        }
        else {
            damageOverlay.color = Color.Lerp(damageOverlay.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
    }

    public void addDamage(float dmg) {
        currentHealth -= dmg;
        playerHealthSlider.value = currentHealth;
        damaged = true;

        playerAS.Play();

        if (currentHealth <= 0) {
            makeDead();
        }
    }

    public void addHealth(float amount) {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        playerHealthSlider.value = currentHealth; 
    }

    public void makeDead() {
        damageOverlay.color = flashColor;
        Instantiate(playerDeathFX, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        Destroy(gameObject);

    }
}
