using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour {
    
    // Public State

    public InputMode inputMode;
    public bool fishingRadialActive;

    public GameObject fishingRadial;

    // Classes

    private SelectionManager selectionManager;
    private PrefabManager prefabManager;
    public CursorManager cursorManager;
    public CharacterUiManager characterUiManager;
    public ResourceManager resourceManager;
    public GraveyardManager graveyardManager;

    // Init

    private void Awake() {
        setInputMode(InputMode.PeopleControl);
    }

    private void Start() {
        selectionManager = ClassManager.instance.selectionManager;
        prefabManager = ClassManager.instance.prefabManager;
        cursorManager = ClassManager.instance.cursorManager;
        characterUiManager = ClassManager.instance.characterUiManager;
        resourceManager = ClassManager.instance.resourceManager;
        graveyardManager = ClassManager.instance.graveyardManager;
    }
    
    // Update

    private void Update() {
        if (Input.GetMouseButton(1)) {
            selectionManager.unselectAllVillagers();
            selectionManager.unselectAllBuildings();
            cursorManager.cancelBuild();
            inputMode = InputMode.PeopleControl;
            characterUiManager.unselectAll();
            if (fishingRadialActive) {
                Destroy(fishingRadial);
                fishingRadialActive = false;
            }
        } 
        else if (Input.GetKeyDown(KeyCode.P)) {
            resourceManager.adjustResource(ResourceType.Food, 100);
            resourceManager.adjustResource(ResourceType.Wood, 100);
            resourceManager.adjustResource(ResourceType.Stone, 100);
            resourceManager.adjustResource(ResourceType.Cotton, 100);
        }
        else if (graveyardManager.lost) {
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene("TitleScene");
            }
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
        if (fishingRadialActive) {
            Destroy(fishingRadial);
            fishingRadialActive = false;
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
            selectionManager.unselectAllVillagers();
            characterUiManager.unselectAll();
        }
        if (fishingRadialActive) {
            Destroy(fishingRadial);
            fishingRadialActive = false;
        }
    }

    public void sleepHouseClicked(BuildingController controller) {
        if (!controller.beingWorkedOn) {
            int selectedVillagerCount = selectionManager.getSelectedVillagerCount();
            if (selectedVillagerCount == 1) {
                controller.beingWorkedOn = true;
                List<VillagerMover> movers = selectionManager.getSelectedVillagerMovers();
                for (int i = 0; i < movers.Count; i++) {
                    movers[i].moveToSleep(controller.gameObject.transform.position);
                    movers[i].GetComponent<VillagerGather>().setBuildingController(controller);
                }
                selectionManager.unselectAllVillagers();
                characterUiManager.unselectAll();
                selectionManager.unselectAllBuildings();
                controller.setSleeping(true);
            }    
        }
        if (fishingRadialActive) {
            Destroy(fishingRadial);
            fishingRadialActive = false;
        }
    }

    public void eatHouseClicked(BuildingController controller) {
        if (!controller.beingWorkedOn) {
            int selectedVillagerCount = selectionManager.getSelectedVillagerCount();
            if (selectedVillagerCount == 1 && resourceManager.food > 0) {
                controller.beingWorkedOn = true;
                resourceManager.adjustResource(ResourceType.Food, -1);
                List<VillagerMover> movers = selectionManager.getSelectedVillagerMovers();
                for (int i = 0; i < movers.Count; i++) {
                    movers[i].moveToEat(controller.gameObject.transform.position);
                    movers[i].GetComponent<VillagerGather>().setBuildingController(controller);
                }
                selectionManager.unselectAllVillagers();
                characterUiManager.unselectAll();
                selectionManager.unselectAllBuildings();
                controller.setEating(true);
            }     
        }
        if (fishingRadialActive) {
            Destroy(fishingRadial);
            fishingRadialActive = false;
        }
    }

    public void fishingButtonClicked(Vector3 position) {
        if (inputMode == InputMode.PeopleControl) {
            List<VillagerMover> movers = selectionManager.getSelectedVillagerMovers();
            for (int i = 0; i < movers.Count; i++) {
                movers[i].move(position, toOcean: true);
            }

            Instantiate(prefabManager.groundClick, position, Quaternion.identity);    
        }
        if (fishingRadialActive) {
            Destroy(fishingRadial);
            fishingRadialActive = false;
        }
    }

    public void setFarmType(CropType type, BuildingController controller) {
        selectionManager.unselectAllBuildings();
        controller.setFarmType(type);
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
            selectionManager.unselectAllVillagers();
            characterUiManager.unselectAll();
        }
        if (fishingRadialActive) {
            Destroy(fishingRadial);
            fishingRadialActive = false;
        }
    }

    public void treeClicked(TreeController controller) {
        int selectedVillagerCount = selectionManager.getSelectedVillagerCount();
        if (selectedVillagerCount > 0) {
            // Harvest the tree
            List<VillagerMover> movers = selectionManager.getSelectedVillagerMovers();
            for (int i = 0; i < movers.Count; i++) {
                movers[i].move(controller.gameObject.transform.position, true);
                movers[i].GetComponent<VillagerGather>().setTreeController(controller);
            }
            selectionManager.unselectAllVillagers();
            characterUiManager.unselectAll();
        }
        if (fishingRadialActive) {
            Destroy(fishingRadial);
            fishingRadialActive = false;
        }
    }

    public void boulderClicked(RockController controller) {
        int selectedVillagerCount = selectionManager.getSelectedVillagerCount();
        if (selectedVillagerCount > 0) {
            // Harvest the rock
            List<VillagerMover> movers = selectionManager.getSelectedVillagerMovers();
            for (int i = 0; i < movers.Count; i++) {
                movers[i].move(controller.gameObject.transform.position, true);
                movers[i].GetComponent<VillagerGather>().setRockController(controller);
            }
            selectionManager.unselectAllVillagers();
            characterUiManager.unselectAll();
        }
        if (fishingRadialActive) {
            Destroy(fishingRadial);
            fishingRadialActive = false;
        }
    }

    public void setInputMode(InputMode mode) {
        inputMode = mode;
    }
}
