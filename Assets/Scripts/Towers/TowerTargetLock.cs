using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Tower))]
public class TowerTargetLock : MonoBehaviour{


    //parameters
    [SerializeField] Transform weapon;
    [SerializeField] float towerRange = 15;
    
    //cached
    Transform target; //Serialized for Debugging Purpose
    Enemy[] enemies;
    ParticleSystem projectileLauncher;

    //states
    bool isAttacking;
    float distanceToCurrentTarget;

    void Awake() {
        projectileLauncher = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update(){
        FindClosestTarget();
        AimWeapon();
    }

    private void FindClosestTarget() {

        if (target) { //protecting against null reference if no target found yet
            if (target.gameObject.activeInHierarchy && distanceToCurrentTarget <= towerRange) {
                return;
            } 
        }


        enemies = FindObjectsOfType<Enemy>();
        float maxDistance = Mathf.Infinity;
        Transform currentClosestTarget = null;

        foreach (Enemy enemy in enemies) {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance) {
                maxDistance = targetDistance;
                currentClosestTarget = enemy.transform;
            }

        }
        target = currentClosestTarget;
    }

    void AimWeapon() {

        distanceToCurrentTarget = Vector3.Distance(transform.position, target.transform.position);

        if (target) {
            weapon.LookAt(target.position);
        }

        if (distanceToCurrentTarget <= towerRange) {
            Attack(true);
        } else {
            Attack(false);
        }
    }

    void Attack(bool isActive) {
        var emissionModule = projectileLauncher.emission;
        emissionModule.enabled = isActive;
    }
}
