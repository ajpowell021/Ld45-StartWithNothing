using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    
    // Classes

    private SelectionManager selectionManager;
    private PrefabManager prefabManager;
    
    // Init

    private void Start() {
        selectionManager = ClassManager.instance.selectionManager;
        prefabManager = ClassManager.instance.prefabManager;
    }
    
    // Update

    private void Update() {
        if (Input.GetMouseButton(1)) {
            selectionManager.unselectAllVillagers();
        } 
    }

    // Public Functions

    // Move any selected villagers to this position.
    public void islandClicked(Vector3 position) {
        List<VillagerMover> movers = selectionManager.getSelectedVillagerMovers();
        for (int i = 0; i < movers.Count; i++) {
             movers[i].move(position);
        }

        Instantiate(prefabManager.groundClick, position, Quaternion.identity);
    }
}
