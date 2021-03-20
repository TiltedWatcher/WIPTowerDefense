using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;

[ExecuteAlways]
public class CoordinateLabel : MonoBehaviour{

    //parameters
    [SerializeField] Color defaultColour = Color.white;
    [SerializeField] Color blockedColour = Color.black;

    [Header("Controlls")]
    [SerializeField] InputAction labelToggleKeybind;

    //cached references
    TextMeshPro label;
    Vector2Int coordinates;
    Tile waypoint;

    //states
    [SerializeField]

    void Awake() {
        labelToggleKeybind.Enable();

        label = GetComponent<TextMeshPro>();
        label.enabled = false;
        waypoint = GetComponentInParent<Tile>();
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
        if (waypoint.TowerCanBePlacedHere) {
            label.color = defaultColour;
        } else {
            label.color = blockedColour;
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
