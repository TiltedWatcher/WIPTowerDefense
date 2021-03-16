using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour{

    const string TOWERS_PARENT_NAME = "Towers";

    //cached references
    GameObject towersParent;
    Bank bank;


    private void Awake() {
        GenerateTowerParent();
    }

    private void Start() {
        bank = FindObjectOfType<Bank>();
    }

    private void GenerateTowerParent() {
        towersParent = GameObject.Find(TOWERS_PARENT_NAME);
        if (!towersParent) {
            towersParent = new GameObject(TOWERS_PARENT_NAME);
        }
    }

    // Update is called once per frame
    void Update() {

    }

    public bool CreateTower(Tower towerPrefab, Vector3 position) {

        if (!bank) {
            return false;
        }
        if (bank.CurrentBalance >= towerPrefab.Cost) {
            var thisTower = Instantiate(towerPrefab, position, Quaternion.identity);
            thisTower.transform.SetParent(towersParent.transform);
            bank.WithdrawGold(towerPrefab.Cost);
            return true;
        }

        return false;

    }
}
