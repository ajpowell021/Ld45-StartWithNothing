using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandClicker : MonoBehaviour {

    // Classes

    private InputManager inputManager;
    
    // Init

    private void Start() {
        inputManager = ClassManager.instance.inputManager;
    }
    
    // Clicker

    private void OnMouseDown() {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        inputManager.islandClicked(position);
    }
}
