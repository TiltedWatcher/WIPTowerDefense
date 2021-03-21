using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour{

    //parameters
    [SerializeField] List<Node> path = new List<Node>();
    [SerializeField][Range(0f, 5f)] float enemyMoveSpeed = 0.5f;



    //cached references
    Enemy enemy;
    GridManager gridManager;
    Pathfinder pathfinder;

    private void Awake() {
        enemy = GetComponent<Enemy>();
        pathfinder = FindObjectOfType<Pathfinder>();
        gridManager = FindObjectOfType<GridManager>();
    }


    private void OnEnable() {
        PlaceAtStart();
        RecalculatePath(true);

    }

    private void RecalculatePath(bool resetPath) {
        Vector2Int coordinates = new Vector2Int();

        if (resetPath) {
            coordinates = pathfinder.StartCoordinates;
        } else {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(MoveEnemyAlongPath());

    }

    IEnumerator MoveEnemyAlongPath() {

        for (int i = 1; i< path.Count; i++) {

            Vector3 startPos = transform.position;
            Vector3 endPos = gridManager.GetWorldPositionFromCoordinates(path[i].coordinates);
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
        transform.position = gridManager.GetWorldPositionFromCoordinates(pathfinder.StartCoordinates);
    }
}
