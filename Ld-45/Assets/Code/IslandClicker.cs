using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandClicker : MonoBehaviour {

    // Classes

    private InputManager inputManager;
    private SelectionManager selectionManager;
    
    // Private State

    private Vector3 initialClickPos;
    private Vector3 unClickPos;

    private float mouseDownTime;
    
    // Init

    private void Start() {
        inputManager = ClassManager.instance.inputManager;
        selectionManager = ClassManager.instance.selectionManager;
    }
    
    // Clicker

    private void OnMouseDown() {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        initialClickPos = position;
        mouseDownTime = Time.time;
    }

    private void OnMouseUp() {
        if (Time.time > mouseDownTime + .1f) {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            unClickPos = position;

            // I have coords here.
            List<VillagerStats> stats = selectionManager.getAllVillagerStatsInBounds(initialClickPos, unClickPos);
            for (int i = 0; i < stats.Count; i++) {
                stats[i].setSelected(true);
            }
        }
        else {
            inputManager.islandClicked(initialClickPos);
        }
    }
}
