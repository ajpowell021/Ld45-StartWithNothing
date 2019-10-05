using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour {
    
    // Public State

    public BuildingType buildingType;
    public int currentResourcesHeld;
    
    // Private State

    private float timeOfLastHarvest;
    
    // Classes

    private DataHolder dataHolder;
    private InputManager inputManager;
    
    // Init

    private void Start() {
        dataHolder = ClassManager.instance.dataHolder;
        inputManager = ClassManager.instance.inputManager;
        timeOfLastHarvest = Time.time;
    }
    
    // Update

    private void Update() {
        if (currentResourcesHeld < dataHolder.buildingResourceCapacity) {
            if (Time.time > timeOfLastHarvest + dataHolder.buildingResourceGatheringSpeed) {
                currentResourcesHeld++;
                timeOfLastHarvest = Time.time;
            }
        }
    }
    
    // On Click

    private void OnMouseDown() {
        inputManager.buildingClicked(this);
    }
}
