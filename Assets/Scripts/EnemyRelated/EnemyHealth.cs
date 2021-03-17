using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour{


    //Parameters
    [SerializeField] int maxHitPoints = 5;
    [SerializeField] int difficultyRamp = 1;

    //cached references
    Enemy enemy;

    //states
    int currentHitPoints = 0;

    private void Start() {
        enemy = GetComponent<Enemy>();
    }

    private void OnEnable() {
        currentHitPoints = maxHitPoints;
    }


    void OnParticleCollision(GameObject other) {
        ProcessHit();
    }

    void ProcessHit() {

        currentHitPoints--;

        //code to get the damage amount from the particle System

        if (currentHitPoints <=0) {
            enemy.DropGoldFromDeath();
            gameObject.SetActive(false);
            maxHitPoints += difficultyRamp;
        }
    }
}
