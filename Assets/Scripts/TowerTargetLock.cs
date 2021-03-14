using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTargetLock : MonoBehaviour{


    //parameters
    [SerializeField] Transform weapon;
    
    //cached
    Transform target; //Serialized for Debugging Purpose
    

    void Start(){
        target = FindObjectOfType<EnemyMover>().transform;
    }

    // Update is called once per frame
    void Update(){
        AimWeapon();
        
    }

    void AimWeapon() {
        weapon.LookAt(target.position);
    }
}
