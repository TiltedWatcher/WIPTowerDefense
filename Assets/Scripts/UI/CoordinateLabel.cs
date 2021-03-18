using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabel : MonoBehaviour{

    //parameters
    [SerializeField] Color defaultColour = Color.white;
    [SerializeField] Color blockedColour = Color.grey;
    [SerializeField] Color exploredColour = Color.blue;
    [SerializeField] Color pathColour = Color.yellow;

    [Header("Controlls")]
    [SerializeField] InputAction labelToggleKeybind;

    //cached references
    TextMeshPro label;
    Vector2Int coordinates;
    GridManager gridManager;
    

    //states
    [SerializeField]

    void Awake() {
        labelToggleKeybind.Enable();
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        DisplayCurrentCoordinates();
        UpdateCoordinateColour();

    }

    void Update(){
        if (!Application.isPlaying) {
            DisplayCurrentCoordinates();
            
            
        }
        ToggleLabels();
        UpdateCoordinateColour();
    }

    void OnDestroy() {
        labelToggleKeybind.Disable();
    }

    void UpdateCoordinateColour() {

        if (!gridManager) {
            return;
        }
        Node node = gridManager.GetNode(coordinates);
        if (node == null) {
            return;
        }

        if (!node.canBeWalkedOn) {
            label.color = blockedColour;
        } else if (node.isInPath) {
            label.color = pathColour;
        } else if (node.isExplored) {
            label.color = exploredColour;
        } else {
            label.color = defaultColour;
        }
        

    }


    void DisplayCurrentCoordinates() {

        //getting the current coordinates as int, divided by the move distance of the snap, to adjust for the fact that our objects are bigger than size 1
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x); 
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        label.text = $"{coordinates.x},{coordinates.y}";
        UpdateObjectName();

    }

    void ToggleLabels() {
        if (labelToggleKeybind.triggered) {
            label.enabled = !label.enabled;
        }
    }

    void UpdateObjectName() {
        transform.parent.name = coordinates.ToString();
    }

}
