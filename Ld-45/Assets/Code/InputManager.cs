using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    
    // Public State

    public InputMode inputMode;

    // Classes

    private SelectionManager selectionManager;
    private PrefabManager prefabManager;
    public CursorManager cursorManager;

    // Init

    private void Awake() {
        setInputMode(InputMode.PeopleControl);
    }

    private void Start() {
        selectionManager = ClassManager.instance.selectionManager;
        prefabManager = ClassManager.instance.prefabManager;
        cursorManager = ClassManager.instance.cursorManager;
    }
    
    // Update

    private void Update() {
        if (Input.GetMouseButton(1)) {
            selectionManager.unselectAllVillagers();
            selectionManager.unselectAllBuildings();
            cursorManager.cancelBuild();
            inputMode = InputMode.PeopleControl;
        } 
    }

    // Public Functions

    // Move any selected villagers to this position.
    public void islandClicked(Vector3 position) {
        if (inputMode == InputMode.PeopleControl) {
            List<VillagerMover> movers = selectionManager.getSelectedVillagerMovers();
            for (int i = 0; i < movers.Count; i++) {
                movers[i].move(position);
            }

            Instantiate(prefabManager.groundClick, position, Quaternion.identity);    
        }
        else if (inputMode == InputMode.Build) {
            cursorManager.buildTheBuildingOnCursor();
        }
    }

    public void buildingClicked(BuildingController building) {
        int selectedVillagerCount = selectionManager.getSelectedVillagerCount();
        if (selectedVillagerCount > 0) {
            List<VillagerMover> movers = selectionManager.getSelectedVillagerMovers();
            for (int i = 0; i < movers.Count; i++) {
                movers[i].move(building.gameObject.transform.position, true);
                movers[i].GetComponent<VillagerGather>().setBuildingController(building);
            }
        }
        else {
            if (building.selected) {
                building.toggleBuildingSelect();
            }
            else {
                selectionManager.unselectAllBuildings();
                building.toggleBuildingSelect();
            }
            inputMode = !building.selected ? InputMode.PeopleControl : InputMode.BuidlingSelected;
        }
    }

    public void buildingSiteClicked(BuildingSiteController controller) {
        int selectedVillagerCount = selectionManager.getSelectedVillagerCount();
        if (selectedVillagerCount > 0) {
            // Work on the site
            List<VillagerMover> movers = selectionManager.getSelectedVillagerMovers();
            for (int i = 0; i < movers.Count; i++) {
                movers[i].move(controller.gameObject.transform.position, true);
                movers[i].GetComponent<VillagerGather>().setBuildingSiteController(controller);
            }
        }
        else {
            
        }
    }

    public void setInputMode(InputMode mode) {
        inputMode = mode;
    }
}
