using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerGather : MonoBehaviour {

    // Private State

    private BuildingController buildingController;
    private BuildingSiteController buildingSiteController;
    private Animator animator;
    private float timeGatheringStarted;
    private float timeBuildingStarted;
    public bool gathering;
    public bool building;
    public bool enrouteToBuilding;
    
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
                // Finish a round of gathering
                resourceManager.adjustResource(getResourceTypeFromBuildingType(buildingController.buildingType), 1);
                buildingController.updateResourceCountUi();
                if (buildingController.currentResourcesHeld == 0) {
                    doneGathering();    
                }
                else {
                    timeGatheringStarted = Time.time;
                    buildingController.currentResourcesHeld--;
                }
            }
        }
        else if (building) {
            if (Time.time > timeBuildingStarted + dataHolder.workerGatherTime) {
                // Finish a round of building
                if (buildingSiteController.donePercent == 100) {
                    doneBuilding();
                }
                else {
                    timeBuildingStarted = Time.time;
                    buildingSiteController.doWork();
                }
            }
        }
    }

    // Public Functions

    public void setBuildingController(BuildingController controller) {
        buildingController = controller;
    }

    public void setBuildingSiteController(BuildingSiteController controller) {
        buildingSiteController = controller;
        enrouteToBuilding = true;
    }

    public void arrivedAtBuilding() {
        if (enrouteToBuilding) {
            // Work on building
            if (buildingSiteController.donePercent < 100) {
                buildingSiteController.doWork();
                building = true;
                timeBuildingStarted = Time.time;
                animator.SetBool("working", true);
                stats.setSelected(false);
                enrouteToBuilding = false;
            }
        }
        else {
            // Gather resources
            if (buildingController.currentResourcesHeld > 0) {
                buildingController.currentResourcesHeld--;
                gathering = true;
                timeGatheringStarted = Time.time;
                animator.SetBool("working", true);
                stats.setSelected(false);
            }
        }
    }
    
    // Private Functions

    private void doneGathering() {
        animator.SetBool("working", false);
        gathering = false;
    }

    private void doneBuilding() {
        animator.SetBool("working", false);
        building = false;
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
