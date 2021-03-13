using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[ExecuteAlways]
public class CoordinateLabel : MonoBehaviour{

    TextMeshPro label;
    Vector2Int coordinates;

    void Awake() {
        label = GetComponent<TextMeshPro>();
        DisplayCurrentCoordinates();
    }

    void Update(){
        if (!Application.isPlaying) {
            DisplayCurrentCoordinates();
            
        }
    }

    private void DisplayCurrentCoordinates() {

        //getting the current coordinates as int, divided by the move distance of the snap, to adjust for the fact that our objects are bigger than size 1
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x); 
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        label.text = $"{coordinates.x},{coordinates.y}";
        UpdateObjectName();

    }

    private void UpdateObjectName() {
        transform.parent.name = coordinates.ToString();
    }
}
