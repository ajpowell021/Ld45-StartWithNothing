using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialButtonController : MonoBehaviour {

    public enum Button {
        DemolishMine,
        HarvestMine,
        HarvestLumber,
        DemolishLumber,
        DemolishHouse,
        EatHouse,
        SleepHouse,
        PlantCorn,
        PlantStrawberry,
        PlantCotton,
        DemolishFarm,
        HarvestFarm,
        UpgradeFarm,
        Fish
    }
    
    public Button buttonType;    
    
    // Classes

    private InputManager inputManager;

    private BuildingController buildingController;
    private SelectionManager selectionManager;

    private void Awake() {
        if (buttonType != Button.Fish) {
            buildingController = gameObject.transform.parent.gameObject.GetComponent<BuildingController>();    
        }
    }

    private void Start() {
        inputManager = ClassManager.instance.inputManager;
        selectionManager = ClassManager.instance.selectionManager;
    }

    public void OnMouseDown() {
        switch (buttonType) {
            case Button.DemolishLumber:
            case Button.DemolishMine:
            case Button.DemolishHouse:
            case Button.DemolishFarm:
                if (!buildingController.beingWorkedOn) {
                    selectionManager.unselectAllBuildings();
                    Destroy(gameObject.transform.parent.gameObject);    
                }
                break;
            case Button.HarvestLumber:
            case Button.HarvestMine:
            case Button.HarvestFarm:
                inputManager.buildingClicked(buildingController);
                break;
            case Button.EatHouse:
                inputManager.eatHouseClicked(buildingController);
                break;
            case Button.SleepHouse:
                inputManager.sleepHouseClicked(buildingController);
                break;
            case Button.PlantCorn:
                inputManager.setFarmType(CropType.Corn, buildingController);
                break;
            case Button.PlantStrawberry:
                inputManager.setFarmType(CropType.Strawberry, buildingController);
                break;
            case Button.PlantCotton:
                inputManager.setFarmType(CropType.Cotton, buildingController);
                break;
            case Button.UpgradeFarm:
                break;
            case Button.Fish:
                inputManager.fishingButtonClicked(transform.position);
                break;
        }
    }
}
