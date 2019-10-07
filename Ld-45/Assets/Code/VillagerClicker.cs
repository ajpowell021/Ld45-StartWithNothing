using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerClicker : MonoBehaviour {
    
    // Classes

    private VillagerStats villagerStats;
    private VillagerGather gather;
    private InputManager inputManager;
    private SelectionManager selectionManager;
    private CharacterUiManager characterUiManager;
    
    // Init

    private void Awake() {
        villagerStats = gameObject.GetComponent<VillagerStats>();
        gather = gameObject.GetComponent<VillagerGather>();
    }

    private void Start() {
        inputManager = ClassManager.instance.inputManager;
        selectionManager = ClassManager.instance.selectionManager;
        characterUiManager = ClassManager.instance.characterUiManager;
    }

    // On Mouse Down
    
    private void OnMouseDown() {
        if (!gather.isWorkerBusy()) {
            if (inputManager.inputMode == InputMode.PeopleControl) {
                villagerStats.toggleSelected();
            }
            else if (inputManager.inputMode == InputMode.BuidlingSelected) {
                selectionManager.unselectAllBuildings();
                villagerStats.toggleSelected();
            }   
            characterUiManager.characterSelected(villagerStats.id, villagerStats.selected);
        }
    }
}
