using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour{

    //parameters
    const int amountOfDirections = 4;
    const float pathfinderStartingDelay = 0.1f;
    [SerializeField] Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int destinationCoordinates;


    //cached Nodes
    Node startNode;
    Node destinationNode;
    Node currentSearchNode; 

    //cached other
    GridManager gridManager;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    //states
    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();
    Queue<Node> frontierExploredNodes = new Queue<Node>();



    void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager) {
            grid = gridManager.Grid;
        }
        if (directions.Length > amountOfDirections) {
            directions = new Vector2Int[4] { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
        }


    }

    void Start(){
        startNode = gridManager.Grid[startCoordinates];
        destinationNode = gridManager.Grid[destinationCoordinates];
        StartCoroutine(DelayPathfinder());
    }

    private void ExploreNeighbours() {
        List<Node> neighbours = new List<Node>();

        if (!currentSearchNode.canBeWalkedOn) {
            return;
        }
        for (int i = 0; i < directions.Length; i++) {
            Vector2Int neighbourCoordinates = currentSearchNode.coordinates + directions[i];
           
            if (grid.ContainsKey(neighbourCoordinates)) {
                neighbours.Add(grid[neighbourCoordinates]);

            }
        }

        foreach (Node neighbour in neighbours) {
            Debug.Log($"{neighbour.coordinates} can be walked on: {neighbour.canBeWalkedOn}");
            if (!reached.ContainsKey(neighbour.coordinates) && neighbour.canBeWalkedOn) {
                Debug.Log($"{neighbour.coordinates} is the child node to {currentSearchNode.coordinates} in the path");
                neighbour.parentNode = currentSearchNode;
                reached.Add(neighbour.coordinates, neighbour);
                frontierExploredNodes.Enqueue(neighbour);
            }
        }

    }

    void BreadthFirstSearch() {
        bool isRunning = true;

        frontierExploredNodes.Enqueue(startNode);
        reached.Add(startCoordinates, startNode);

        while (frontierExploredNodes.Count > 0 && isRunning) {

            currentSearchNode = frontierExploredNodes.Dequeue();
            ExploreNeighbours();
            currentSearchNode.isExplored = true;

            if (currentSearchNode.coordinates == destinationCoordinates) {
                isRunning = false;
            }

        }
    }

    List<Node> BuildPath() {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isInPath = true;

        while (currentNode.parentNode != null){
            currentNode = currentNode.parentNode;
            path.Add(currentNode);
            currentNode.isInPath = true;
            
        }

        path.Reverse();
        return path;
    }

    IEnumerator DelayPathfinder() {
        yield return new WaitForEndOfFrame();
        BreadthFirstSearch();
        BuildPath();

    }
}
