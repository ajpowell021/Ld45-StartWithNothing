using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSiteController : MonoBehaviour {
    
    // Public State

    public BuildingType buildingType;
    public int donePercent;
    
    // Classes

    private InputManager inputManager;
    private PrefabManager prefabManager;
    
    // Init

    private void Start() {
        inputManager = ClassManager.instance.inputManager;
        prefabManager = ClassManager.instance.prefabManager;
    }

    // Public Function

    public void setBuildingType(BuildingType type) {
        buildingType = type;
    }

    public void finishedBuilding() {
        Instantiate(prefabManager.getPrefabFromBuildingType(buildingType), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void doWork() {
        donePercent += 10;
        if (donePercent == 100) {
            finishedBuilding();
        }
    }

    public void OnMouseDown() {
        inputManager.buildingSiteClicked(this);
    }
}
