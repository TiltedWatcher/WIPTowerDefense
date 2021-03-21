using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour{

    //parameters
    const int amountOfDirections = 4;
    const float pathfinderStartingDelay = 0.1f;
    const string RECALCULATE_PATH_MESSAGE_METHOD_NAME = "RecalculatePath";
    [SerializeField] Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    [SerializeField] Vector2Int startCoordinates;
    public Vector2Int StartCoordinates {get => startCoordinates;}
    [SerializeField] Vector2Int destinationCoordinates;
    public Vector2Int DestinationCoordinates {get => destinationCoordinates;}


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
            startNode = grid[startCoordinates];
            destinationNode = grid[destinationCoordinates];
            
        }
        if (directions.Length > amountOfDirections) {
            directions = new Vector2Int[4] { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
        }


    }

    void Start(){

        StartCoroutine(DelayPathfinder());
    }

    public List<Node> GetNewPath() {
        return GetNewPath(StartCoordinates);
        ;
    }    
    
    public List<Node> GetNewPath(Vector2Int coordinates) {
        gridManager.ResetNodes();
        BreadthFirstSearch(coordinates);
        return BuildPath();
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
            //Debug.Log($"{neighbour.coordinates} can be walked on: {neighbour.canBeWalkedOn}");
            if (!reached.ContainsKey(neighbour.coordinates) && neighbour.canBeWalkedOn) {
                //Debug.Log($"{neighbour.coordinates} is the child node to {currentSearchNode.coordinates} in the path");
                neighbour.parentNode = currentSearchNode;
                reached.Add(neighbour.coordinates, neighbour);
                frontierExploredNodes.Enqueue(neighbour);
            }
        }

    }

    void BreadthFirstSearch(Vector2Int startCoords) {
        bool isRunning = true;
        frontierExploredNodes.Clear();
        reached.Clear();
        startNode.canBeWalkedOn = true;
        destinationNode.canBeWalkedOn = true;

        frontierExploredNodes.Enqueue(grid[startCoords]);
        reached.Add(startCoords, startNode);

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

    public bool WillBlockPath(Vector2Int coordinates) {
        if (grid.ContainsKey(coordinates)) {
            bool previousState = grid[coordinates].canBeWalkedOn;
            grid[coordinates].canBeWalkedOn = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].canBeWalkedOn = previousState;

            if (newPath.Count <=1) {
                GetNewPath();
                return true;
            }

        }
        return false;
    }


    IEnumerator DelayPathfinder() {
        yield return new WaitForEndOfFrame();
        GetNewPath();

    }

    public void NotifyReceivers() {
        BroadcastMessage(RECALCULATE_PATH_MESSAGE_METHOD_NAME, false, SendMessageOptions.DontRequireReceiver) ;
    }

}
