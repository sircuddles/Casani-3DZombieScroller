﻿using UnityEngine;
using System.Collections;

public class shootBullet : MonoBehaviour {
    [SerializeField] private float range;
    [SerializeField] private float damage;

    Ray shootRay;
    RaycastHit shootHit;
    int shootableMask;
    LineRenderer gunLine;

    void Awake() {
        shootableMask = LayerMask.GetMask("Shootable");
        gunLine = GetComponent<LineRenderer>();

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        gunLine.SetPosition(0, transform.position);

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask)) {
            if (shootHit.collider.tag == "Enemy") {
                enemyHealth eHP = shootHit.collider.GetComponent<enemyHealth>();
                if (eHP) {
                    eHP.addDamage(damage);
                    eHP.damageFX(shootHit.point, -shootRay.direction);
                }
                gunLine.SetPosition(1, shootHit.point);
            }
            else gunLine.SetPosition(1, shootHit.point);
        }
        else gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
    }

}
