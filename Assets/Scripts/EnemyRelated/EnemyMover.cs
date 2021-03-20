using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour{

    //parameters
    [SerializeField] List<Tile> path;
    [SerializeField][Range(0f, 5f)] float enemyMoveSpeed = 0.5f;
    const string PATH_OBJECT_TAG = "Path";


    //cached references
    Enemy enemy;

    private void Start() {
        enemy = GetComponent<Enemy>();
    }

    private void OnEnable() {
        FindPath();
        PlaceAtStart();
        StartCoroutine(MoveEnemyAlongPath());
    }

    private void FindPath() {
        path.Clear();
        GameObject waypoints = GameObject.FindGameObjectWithTag(PATH_OBJECT_TAG);

        foreach (Transform child in waypoints.transform) {

            var waypoint = child.GetComponent<Tile>();
            if (waypoint) {
                path.Add(waypoint);
            }
            
        }
    }

    IEnumerator MoveEnemyAlongPath() {

        foreach (Tile waypoint in path) {

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

        FinishPath();

    }

    private void FinishPath() {
        enemy.StealGold();
        gameObject.SetActive(false);
    }

    void PlaceAtStart() {
        transform.position = path[0].transform.position;
    }
}
