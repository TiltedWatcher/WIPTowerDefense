using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour{

    [SerializeField] List<Waypoint> path;
    [SerializeField] float timeBetweenEnemySteps = 1f;


    void Start(){
        StartCoroutine(MoveEnemyAlongPath());
    }



    IEnumerator MoveEnemyAlongPath() {

        foreach (Waypoint waypoint in path) {
            yield return new WaitForSeconds(timeBetweenEnemySteps);
            transform.position = waypoint.transform.position;

        }

    }
}
