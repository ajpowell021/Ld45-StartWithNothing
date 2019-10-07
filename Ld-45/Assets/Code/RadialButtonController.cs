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
        SleepHouse
    }
    
    public Button buttonType;    
    
    // Classes

    private InputManager inputManager;

    private BuildingController buildingController;

    private void Awake() {
        buildingController = gameObject.transform.parent.gameObject.GetComponent<BuildingController>();
    }

    private void Start() {
        inputManager = ClassManager.instance.inputManager;
    }

    public void OnMouseDown() {
        switch (buttonType) {
            case Button.DemolishLumber:
            case Button.DemolishMine:
            case Button.DemolishHouse:
                if (!buildingController.beingWorkedOn) {
                    Destroy(gameObject.transform.parent.gameObject);    
                }
                break;
            case Button.HarvestLumber:
            case Button.HarvestMine:
                inputManager.buildingClicked(buildingController);
                break;
            case Button.EatHouse:
                inputManager.eatHouseClicked(buildingController);
                break;
            case Button.SleepHouse:
                inputManager.sleepHouseClicked(buildingController);
                break;
        }
    }
}
