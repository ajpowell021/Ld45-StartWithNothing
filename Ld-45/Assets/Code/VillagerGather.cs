using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerGather : MonoBehaviour {

    // Private State

    private BuildingController buildingController;
    private BuildingSiteController buildingSiteController;
    private TreeController treeController;
    private RockController rockController;
    private Animator animator;
    private float timeGatheringStarted;
    private float timeBuildingStarted;
    public bool gathering;
    public bool building;
    public bool choppingTree;
    public bool hittingRock;
    public bool enrouteToBuilding;
    public bool enrouteToTree;
    public bool enrouteToBoulder;
    private ProgressBar progressBar;
    private GameObject progressBarObject;

    // Classes

    private DataHolder dataHolder;
    private ResourceManager resourceManager;
    private VillagerStats stats;

    // Init

    private void Awake() {
        animator = gameObject.GetComponent<Animator>();
        stats = gameObject.GetComponent<VillagerStats>();
        progressBarObject = gameObject.transform.GetChild(1).gameObject;
        progressBar = progressBarObject.GetComponent<ProgressBar>();
    }

    private void Start() {
        dataHolder = ClassManager.instance.dataHolder;
        resourceManager = ClassManager.instance.resourceManager;
    }

    // Update

    private void Update() {
        if (gathering) {
            
            // Set progress bar
            float percent = (1 - (timeGatheringStarted + dataHolder.workerGatherTime - Time.time) / 2) * 100;
            progressBar.setPercent(Mathf.RoundToInt(percent));

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
        else if (choppingTree) {
            // Set progress bar
            float percent = (1 - (timeGatheringStarted + dataHolder.workerGatherTime - Time.time) / 2) * 100;
            progressBar.setPercent(Mathf.RoundToInt(percent));

            if (Time.time > timeGatheringStarted + dataHolder.workerGatherTime) {
                // Finish a round of gathering from tree
                resourceManager.adjustResource(ResourceType.Wood, 1);
                if (treeController.resourcesLeft == 0) {
                    doneGathering();
                }
                else {
                    timeGatheringStarted = Time.time;
                    treeController.resourcesLeft--;
                    treeController.checkIfEmpty();
                }
            }
        }
        else if (hittingRock) {
            // Set Progress bar
            float percent = (1 - (timeGatheringStarted + dataHolder.workerGatherTime - Time.time) / 2) * 100;
            progressBar.setPercent(Mathf.RoundToInt(percent));

            if (Time.time > timeGatheringStarted + dataHolder.workerGatherTime) {
                // Finish a round of gathering from rock
                resourceManager.adjustResource(ResourceType.Stone, 1);
                if (rockController.resourcesLeft == 0) {
                    doneGathering();
                }
                else {
                    timeGatheringStarted = Time.time;
                    rockController.resourcesLeft--;
                    rockController.checkIfEmpty();
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

    public void setTreeController(TreeController controller) {
        treeController = controller;
        enrouteToTree = true;
    }

    public void setRockController(RockController controller) {
        rockController = controller;
        enrouteToBoulder = true;
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
        else if (enrouteToTree) {
            // Harvest Tree
            if (treeController.resourcesLeft > 0) {
                treeController.resourcesLeft--;
                treeController.checkIfEmpty();
                progressBarObject.SetActive(true);
                choppingTree = true;
                timeGatheringStarted = Time.time;
                animator.SetBool("working", true);
                stats.setSelected(false);
                enrouteToTree = false;
            }
        }
        else if (enrouteToBoulder) {
            // Harvest Boulder
            if (rockController.resourcesLeft > 0) {
                rockController.resourcesLeft--;
                rockController.checkIfEmpty();
                progressBarObject.SetActive(true);
                hittingRock = true;
                timeGatheringStarted = Time.time;
                animator.SetBool("working", true);
                stats.setSelected(false);
                enrouteToBoulder = false;
            }
        }
        else {
            // Gather resources
            if (buildingController.currentResourcesHeld > 0) {
                buildingController.currentResourcesHeld--;
                progressBarObject.SetActive(true);
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
        choppingTree = false;
        hittingRock = false;
        progressBarObject.SetActive(false);
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
