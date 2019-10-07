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
    public bool sleeping;
    public bool eating;
    public bool fishing;
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
    private SpriteRenderer spriteRenderer;
    private VillagerMover mover;
    
    // Init

    private void Awake() {
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        stats = gameObject.GetComponent<VillagerStats>();
        progressBarObject = gameObject.transform.GetChild(1).gameObject;
        progressBar = progressBarObject.GetComponent<ProgressBar>();
        mover = gameObject.GetComponent<VillagerMover>();
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
                resourceManager.adjustResource(getResourceTypeFromBuildingType(buildingController.buildingType, buildingController.farmType), 1);
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
        else if (fishing) {

            // Set progress bar
            float percent = (1 - (timeGatheringStarted + dataHolder.fishingTime - Time.time) / 2) * 100;
            progressBar.setPercent(Mathf.RoundToInt(percent));
            
            if (Time.time > timeGatheringStarted + dataHolder.fishingTime) {
                // Caught a fish
                resourceManager.adjustResource(ResourceType.Food, 1);
                animator.SetBool("fishing", false);
                fishing = false;
                progressBarObject.SetActive(false);
                
                spriteRenderer.flipX = false;
                if (mover.fishingToRight) {
                    Vector3 newPosition = gameObject.transform.position;
                    newPosition.x -= 1;
                    gameObject.transform.position = newPosition;    
                }
                else {
                    Vector3 newPosition = gameObject.transform.position;
                    newPosition.x += 1;
                    gameObject.transform.position = newPosition; 
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
                
                timeGatheringStarted = Time.time;
                treeController.resourcesLeft--;
                treeController.checkIfEmpty();
                if (treeController.resourcesLeft == 0) {
                    doneGathering();
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
                
                timeGatheringStarted = Time.time;
                rockController.resourcesLeft--;
                rockController.checkIfEmpty();
                if (rockController.resourcesLeft == 0) {
                    doneGathering();
                }
            }
        }
        else if (eating) {
            stats.ateFood();
            if (Time.time > timeGatheringStarted + dataHolder.eatTime) {
                doneEatingSleeping();
            }
        }
        else if (sleeping) {
            stats.slept();
            if (Time.time > timeGatheringStarted + dataHolder.sleepTime) {
                doneEatingSleeping();
            }
        }
        else if (building) {
            if (Time.time > timeBuildingStarted + dataHolder.workerGatherTime) {
                // Finish a round of building
                timeBuildingStarted = Time.time;
                buildingSiteController.doWork();
                if (buildingSiteController.donePercent == 100) {
                    doneBuilding();
                }
            }
        }
    }

    // Public Functions

    public void setBuildingController(BuildingController controller) {
        buildingController = controller;
        controller.beingWorkedOn = true;
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
                if (buildingSiteController.gameObject.transform.position.x < transform.position.x) {
                    spriteRenderer.flipX = true;
                }
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
                if (treeController.gameObject.transform.position.x < transform.position.x) {
                    spriteRenderer.flipX = true;
                }
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
                if (rockController.gameObject.transform.position.x < transform.position.x) {
                    spriteRenderer.flipX = true;
                }
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
                if (buildingController.gameObject.transform.position.x < transform.position.x) {
                    spriteRenderer.flipX = true;
                }
            }
        }
    }

    public void arrivedAtSleep() {
        sleeping = true;
        timeGatheringStarted = Time.time;
        spriteRenderer.enabled = false;
        // Play sleep anim for house.
        stats.setSelected(false);
    }

    public void arrivedAtEat() {
        eating = true;
        timeGatheringStarted = Time.time;
        spriteRenderer.enabled = false;
        // Play sleep anim for house.
        stats.setSelected(false);
    }

    public void arrivedAtFishing() {
        fishing = true;
        timeGatheringStarted = Time.time;
        animator.SetBool("fishing", true);
        if (mover.fishingToRight) {
            spriteRenderer.flipX = false;
            Vector3 newPosition = gameObject.transform.position;
            newPosition.x += 1;
            gameObject.transform.position = newPosition;
        }
        else {
            spriteRenderer.flipX = true;
            Vector3 newPosition = gameObject.transform.position;
            newPosition.x -= 1;
            gameObject.transform.position = newPosition;
        }
        stats.setSelected(false);
        progressBarObject.SetActive(true);
    }

    public bool isWorkerBusy() {
        if (!sleeping && !eating && !gathering && !building && !hittingRock && !choppingTree && !stats.dead) {
            return false;
        }

        return true;
    }
    
    // Private Functions

    private void doneGathering() {
        animator.SetBool("working", false);
        if (gathering) {
            buildingController.beingWorkedOn = false;
        }
        gathering = false;
        choppingTree = false;
        hittingRock = false;
        progressBarObject.SetActive(false);
        spriteRenderer.flipX = false;
    }

    private void doneEatingSleeping() {
        spriteRenderer.enabled = true;
        buildingController.beingWorkedOn = false;
        buildingController.setEating(false);
        buildingController.setSleeping(false);
        sleeping = false;
        eating = false;
    }

    private void doneBuilding() {
        animator.SetBool("working", false);
        building = false;
        spriteRenderer.flipX = false;
    }

    private ResourceType getResourceTypeFromBuildingType(BuildingType buildingType, CropType cropType) {
        switch (buildingType) {
            case BuildingType.LumberYard:
                return ResourceType.Wood;
            case BuildingType.Farm:
                return cropType == CropType.Cotton ? ResourceType.Cotton : ResourceType.Food;
            case BuildingType.Mine:
                return ResourceType.Stone;
            case BuildingType.House:
                return ResourceType.Food;
            default:
                throw new ArgumentOutOfRangeException(nameof(buildingType), buildingType, null);
        }
    }
}
