using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using GG.Infrastructure.Utils;
using System;

[System.Serializable]
public class WeightedListOfAttackers: WeightedList<GameObject> {}
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(WeightedListOfAttackers))]
public class ThisPropertyDrawer: WeightedListPropertyDrawer {
}
#endif


public class ObjectPool : MonoBehaviour{

    //parameters
    [SerializeField] [Min(1f)] int poolSize;
    [SerializeField] [Min(1f)] float minTimeBetweenSpawns = 1f;
    [SerializeField] [Min(1f)] float maxTimeBetweenSpawns = 2f;
    [SerializeField] WeightedListOfAttackers enemyPrefabs;


    //cached references
    GameObject[] pool;

    //states
    float waitBeforeNextSpawn;
    bool gameIsRunning = true;

    void Awake() {
        PopulatePool();    
    }

    void Start(){
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update(){
        
    }

    void PopulatePool() {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++) {
            pool[i] = Instantiate(enemyPrefabs.GetRandomByWeight(), transform);
            pool[i].SetActive(false);
        }
    
    }

    IEnumerator SpawnEnemy() {

        while (gameIsRunning) {
            EnableAttackerInPool();
            yield return new WaitForSeconds(waitBeforeNextSpawn);
           // Instantiate(pool[index], transform);
        }

    }

    private void EnableAttackerInPool() {
        for (int i = 0; i < pool.Length; i++) {
            if (!pool[i].activeInHierarchy) {
                pool[i].SetActive(true);
                return;
            }
        }
    }
}
