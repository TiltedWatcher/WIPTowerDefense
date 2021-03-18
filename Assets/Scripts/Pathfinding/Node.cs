using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node{

    public Vector2Int coordinates;
    public bool canBeWalkedOn;
    public bool isExplored;
    public bool isInPath;
    public Node parentNode; //the previous node in the branch this node is connected to.

    public Node(Vector2Int coordinates, bool canBeWalkedOn) {
        this.coordinates = coordinates;
        this.canBeWalkedOn = canBeWalkedOn;
    }

}
