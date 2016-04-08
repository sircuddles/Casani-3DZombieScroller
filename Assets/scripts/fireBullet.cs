using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class fireBullet : MonoBehaviour {
    [SerializeField] private float timeBetweenBullets;
    [SerializeField] private GameObject projectile;
    private float nextBullet;
    private playerController myPC;

    public int maxRounds;
    public int startRounds;
    int currentRounds;
    public Slider playerAmmoSlider;

    AudioSource gunMuzzleAS;
    public AudioClip shootSound;
    public AudioClip reloadSound;

    public Sprite weaponSprite;
    public Image weaponImage;

    // Use this for initialization
    void Awake () {
        nextBullet = 0f;
        myPC = transform.root.GetComponent<playerController>();
        gunMuzzleAS = GetComponent<AudioSource>();

        currentRounds = startRounds;
        playerAmmoSlider.maxValue = maxRounds;
        playerAmmoSlider.value = currentRounds;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetAxisRaw("Fire1") > 0 && nextBullet < Time.time && currentRounds > 0) {
            nextBullet = Time.time + timeBetweenBullets;
            Vector3 rot;

            if (myPC) {
                if (!myPC.facingRight) {
                    rot = new Vector3(0, -90, 0);
                }
                else {
                    rot = new Vector3(0, 90, 0);
                }


                Instantiate(projectile, transform.position, Quaternion.Euler(rot));
                currentRounds--;
                playerAmmoSlider.value = currentRounds;
                PlaySound(shootSound);
            }
        }
	}

    public void Reload() {
        currentRounds = maxRounds;
        playerAmmoSlider.value = currentRounds;
        PlaySound(reloadSound);
    }

    void PlaySound(AudioClip sound) {
        gunMuzzleAS.clip = sound;
        gunMuzzleAS.Play();
    }

    public void InitializeWeapon() {
        PlaySound(reloadSound);
        nextBullet = 0;
        playerAmmoSlider.maxValue = maxRounds;
        playerAmmoSlider.value = currentRounds;
        weaponImage.sprite = weaponSprite;
    }
}
