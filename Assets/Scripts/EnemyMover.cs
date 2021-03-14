using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour{

    [SerializeField] List<Waypoint> path;
    [SerializeField][Range(0f, 5f)] float enemyMoveSpeed = 0.5f;


    void Start(){
        StartCoroutine(MoveEnemyAlongPath());
    }



    IEnumerator MoveEnemyAlongPath() {

        foreach (Waypoint waypoint in path) {

            Vector3 startPos = transform.position;
            Vector3 endPos = waypoint.transform.position;
            float travelPercent = 0;

            transform.LookAt(endPos);

            while (travelPercent < 1f) {
                travelPercent += Time.deltaTime * enemyMoveSpeed;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return new WaitForEndOfFrame();
            }

        }

    }
}
