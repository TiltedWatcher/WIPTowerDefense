using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTargetLock : MonoBehaviour{


    //parameters
    [SerializeField] Transform weapon;
    
    //cached
    Transform target; //Serialized for Debugging Purpose
    Enemy[] enemies;

    //states


    void Start(){

        
    }

    // Update is called once per frame
    void Update(){
        FindClosestTarget();
        AimWeapon();
    }

    private void FindClosestTarget() {
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
        if (target) {
            weapon.LookAt(target.position);
        }
    }
}
