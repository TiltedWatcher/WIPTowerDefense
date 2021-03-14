using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour{

    [SerializeField] bool towerCanBePlacedHere;
    [SerializeField] GameObject tower;

    public bool TowerCanBePlacedHere {
        get => towerCanBePlacedHere;
        set => towerCanBePlacedHere=value;
    }

    void OnMouseDown() {

        if (towerCanBePlacedHere) {
            Instantiate(tower, transform.position, Quaternion.identity);
            towerCanBePlacedHere = false;
        }
           
    }
}
