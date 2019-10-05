using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerClicker : MonoBehaviour {
    
    // Classes

    private VillagerStats villagerStats;
    private VillagerGather gather;
    private InputManager inputManager;
    
    // Init

    private void Awake() {
        villagerStats = gameObject.GetComponent<VillagerStats>();
        gather = gameObject.GetComponent<VillagerGather>();
    }

    private void Start() {
        inputManager = ClassManager.instance.inputManager;
    }

    // On Mouse Down
    
    private void OnMouseDown() {
        if (!gather.gathering && inputManager.inputMode == InputMode.PeopleControl) {
            villagerStats.toggleSelected();    
        }
    }
}
