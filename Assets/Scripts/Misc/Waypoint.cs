using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour{

    [SerializeField] bool towerCanBePlacedHere;


    void OnMouseDown() {

        if (towerCanBePlacedHere) {
            Debug.Log(transform.name);
        }
           
    }
}
