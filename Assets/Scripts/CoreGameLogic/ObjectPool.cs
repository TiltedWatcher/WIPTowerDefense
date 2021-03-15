using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour{

    //parameters
    [SerializeField] [Min(1f)] float minTimeBetweenSpawns = 1f;
    [SerializeField][Min(1f)] float maxTimeBetweenSpawns = 2f;
    [SerializeField] GameObject enemyPrefab;


    //states
    float waitBeforeNextSpawn;
    bool gameIsRunning = true;
    

    void Start(){
        StartCoroutine(SpawnEnemy(enemyPrefab));
    }

    // Update is called once per frame
    void Update(){
        
    }

    IEnumerator SpawnEnemy(GameObject attackerPrefab) {

        while (gameIsRunning) {
            waitBeforeNextSpawn = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
            yield return new WaitForSeconds(waitBeforeNextSpawn);
            Instantiate(attackerPrefab, transform);
        }

    }

}
