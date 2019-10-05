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
    
    // Init

    private void Start() {
        dataHolder = ClassManager.instance.dataHolder;
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
}
