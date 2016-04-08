using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {
    public GameObject[] weapons;
    bool[] weaponAvailable;

    public Image weaponImage;
    int currentWeapon;

	// Use this for initialization
	void Start () {
        weaponAvailable = new bool[weapons.Length];
        for (int i = 0; i < weapons.Length; i++) {
            weaponAvailable[i] = true;
        }
        currentWeapon = 0;
        DeactivateWeapons();
        SetWeaponActive(currentWeapon);
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.X)) {
            for (int i = currentWeapon + 1; i < weapons.Length; i++) {
                if (weaponAvailable[i] == true) {
                    currentWeapon = i;
                    SetWeaponActive(currentWeapon);
                    return;
                }
            }

            for (int i = 0; i < currentWeapon; i++) {
                if (weaponAvailable[i] == true) {
                    currentWeapon = i;
                    SetWeaponActive(currentWeapon);
                    return;
                }
            }
        }
	}

    public void SetWeaponActive(int index) {
        if (!weaponAvailable[index]) return;
        DeactivateWeapons();

        weapons[index].SetActive(true);
        weapons[index].GetComponentInChildren<fireBullet>().InitializeWeapon();
    }


    void DeactivateWeapons() {
        for (int i = 0; i < weapons.Length; i++) {
            weapons[i].SetActive(false);
        }
    }

    public void ActivateWeapon(int weapon) {
        weaponAvailable[weapon] = true;
    }
     
}
