using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTargetLock : MonoBehaviour{


    //parameters
    [SerializeField] Transform weapon;
    
    //cached
    Transform target; //Serialized for Debugging Purpose
    

    void Start(){
        EnemyMover targetObject = FindObjectOfType<EnemyMover>();
        if (targetObject) {
            target = targetObject.transform;
        }
        
    }

    // Update is called once per frame
    void Update(){
        if (target) {
            AimWeapon();
        }
        
        
    }

    void AimWeapon() {
        weapon.LookAt(target.position);
    }
}
