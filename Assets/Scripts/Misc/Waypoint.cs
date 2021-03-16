using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour{

    //parameters
    [SerializeField] bool towerCanBePlacedHere;
    [SerializeField] Tower towerPrefab;
    [SerializeField] const string TOWERS_PARENT_NAME = "Towers";

    //cached references
    GameObject towersParent;
    TowerSpawner towerSpawner;

    private void Awake() {
        GenerateTowerParent();
    }

    private void Start() {
        towerSpawner = FindObjectOfType<TowerSpawner>();
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

            bool isPlaced = towerSpawner.CreateTower(towerPrefab, transform.position);
            TowerCanBePlacedHere = !isPlaced;

        }
           
    }
}
