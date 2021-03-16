using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour{


    //Parameters
    [SerializeField] int maxHitPoints = 5;
    
    //states
    [SerializeField] int currentHitPoints = 0;



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
            gameObject.SetActive(false);
        
        }
    }
}
