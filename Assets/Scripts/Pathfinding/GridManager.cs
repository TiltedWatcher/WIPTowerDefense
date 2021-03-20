using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour{


    //parameters
    [Tooltip("The size of a point in the grid. This should match the UnityEditor snap settings.")]
    [SerializeField] int unityGridPointSize = 10;
    [SerializeField] Vector2Int gridSize;
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();


    public Dictionary<Vector2Int, Node> Grid {
        get => grid;
    }

    public int UnityGridPointSize {
        get => unityGridPointSize;
    }

    private void Awake() {
        CreateGrid();
    }

    public Node GetNode(Vector2Int coordinates) {
        if (grid.ContainsKey(coordinates)) {
            return grid[coordinates];
        }
        return null;
    }

    private void CreateGrid() {

        for (int x = 0; x < gridSize.x; x++) {
            for (int y = 0; y < gridSize.y; y++ ) {
                Vector2Int coordinates = new Vector2Int(x,y);
                grid.Add(coordinates, new Node(coordinates, true));
               
            }

        }
        
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position) {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / unityGridPointSize);
        coordinates.y = Mathf.RoundToInt(position.z / UnityGridPointSize);

        return coordinates;
    }

    public Vector3 GetWorldPositionFromCoordinates(Vector2Int coordinates) {
        Vector3 position = new Vector3();
        position.x = coordinates.x * unityGridPointSize;
        position.z = coordinates.y * unityGridPointSize;

        return position;
    }

    public void BlockNode(Vector2Int coordinates) {

        if (grid.ContainsKey(coordinates)) {
            grid[coordinates].canBeWalkedOn = false;
        }

    }
}
