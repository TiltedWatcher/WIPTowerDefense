using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour{

    //parameters
    [SerializeField] int cost = 50;

    //constants
    const string TOWERS_PARENT_NAME = "Towers";

    //cached references
    GameObject towersParent;

    public int Cost {
        get => cost;
    }

    private void Awake() {
        GenerateTowerParent();
    }

    private void GenerateTowerParent() {
        towersParent = GameObject.Find(TOWERS_PARENT_NAME);
        if (!towersParent) {
            towersParent = new GameObject(TOWERS_PARENT_NAME);
        }
    }

    // Update is called once per frame
    void Update(){
        
    }

    public bool CreateTower(Tower towerPrefab, Vector3 position) {

        var thisTower = Instantiate(towerPrefab, position, Quaternion.identity);
        thisTower.transform.SetParent(towersParent.transform);
        return true;

    }
}
