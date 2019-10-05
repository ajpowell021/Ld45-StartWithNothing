using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerGather : MonoBehaviour {

    // Private State

    private BuildingController buildingController;
    private Animator animator;
    private float timeGatheringStarted;
    public bool gathering;
    
    // Classes

    private DataHolder dataHolder;
    private ResourceManager resourceManager;
    private VillagerStats stats;

    // Init

    private void Awake() {
        animator = gameObject.GetComponent<Animator>();
        stats = gameObject.GetComponent<VillagerStats>();
    }

    private void Start() {
        dataHolder = ClassManager.instance.dataHolder;
        resourceManager = ClassManager.instance.resourceManager;
    }

    // Update

    private void Update() {
        if (gathering) {
            if (Time.time > timeGatheringStarted + dataHolder.workerGatherTime) {
                doneGathering();
            }
        }
    }

    // Public Functions

    public void setBuildingController(BuildingController controller) {
        buildingController = controller;
    }

    public void arrivedAtBuilding() {
        if (buildingController.currentResourcesHeld > 0) {
            buildingController.currentResourcesHeld--;
            gathering = true;
            timeGatheringStarted = Time.time;
            animator.SetBool("working", true);
            stats.setSelected(false);
        }
    }
    
    // Private Functions

    private void doneGathering() {
        animator.SetBool("working", false);
        gathering = false;
        resourceManager.adjustResource(getResourceTypeFromBuildingType(buildingController.buildingType), 1);
        buildingController.updateResourceCountUi();
    }

    private ResourceType getResourceTypeFromBuildingType(BuildingType buildingType) {
        switch (buildingType) {
            case BuildingType.LumberYard:
                return ResourceType.Wood;
            case BuildingType.Farm:
                return ResourceType.Food;
            case BuildingType.Mine:
                return ResourceType.Stone;
            case BuildingType.House:
                return ResourceType.Food;
            default:
                throw new ArgumentOutOfRangeException(nameof(buildingType), buildingType, null);
        }
    }
}
