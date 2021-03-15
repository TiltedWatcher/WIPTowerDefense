using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour{

    //parameters
    [SerializeField] bool towerCanBePlacedHere;
    [SerializeField] GameObject tower;
    [SerializeField] const string TOWERS_PARENT_NAME = "Towers";

    //cached references
    GameObject towersParent;

    private void Awake() {
        GenerateTowerParent();
    }

    private void GenerateTowerParent() {
        towersParent = GameObject.Find(TOWERS_PARENT_NAME);
        if (!towersParent) {
            towersParent = new GameObject(TOWERS_PARENT_NAME);
        }
    }

    public bool TowerCanBePlacedHere {
        get => towerCanBePlacedHere;
        set => towerCanBePlacedHere=value;
    }

    void OnMouseDown() {

        if (towerCanBePlacedHere) {
            GameObject towerReference = Instantiate(tower, transform.position, Quaternion.identity);
            towerCanBePlacedHere = false;
            towerReference.transform.SetParent(towersParent.transform);
        }
           
    }
}
