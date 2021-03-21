using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour{

    //parameters
    [SerializeField] int cost = 50;
    [SerializeField] float buildTimer = 2f;

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

    private void Start() {
        StartCoroutine(Build());   
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

    IEnumerator Build() {
        float delayPerPart;
        List<Transform> components = new List<Transform>();

        foreach (Transform part in transform) {
            Debug.Log(part.gameObject.name);
            foreach (Transform child in part ) {
                child.gameObject.SetActive(false);
                Debug.Log(child.gameObject.name);
                components.Add(child);
                foreach (Transform grandchild in child) {
                    Debug.Log(grandchild.gameObject.name);
                    grandchild.gameObject.SetActive(false);
                    components.Add(grandchild);
                }
            }
        }

        delayPerPart = buildTimer / components.Count;

        foreach (Transform part in transform) {
            foreach (Transform child in part) {
                child.gameObject.SetActive(true);
                yield return new WaitForSeconds(delayPerPart);
                foreach (Transform grandchild in child) {
                    grandchild.gameObject.SetActive(true);
                    yield return new WaitForSeconds(delayPerPart);
                }
            }
        }
    }
}
