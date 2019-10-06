using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingButton : MonoBehaviour {
    
    // Public State

    public BuildingType buttonType;
    
    // Classes

    private HudController hudController;
    private ResourceManager resourceManager;
    private PopupController popupController;

    // Init

    private void Start() {
        hudController = ClassManager.instance.hudController;
        resourceManager = ClassManager.instance.resourceManager;
        popupController = ClassManager.instance.popupController;
    }

    // Private Functions
    
    private void OnMouseDown() {
        if (buttonType == BuildingType.Mine) {
            if (resourceManager.canAffordMine()) {
                hudController.build(buttonType);
            }
        }
        else if (buttonType == BuildingType.LumberYard) {
            if (resourceManager.canAffordLumberYard()) {
                hudController.build(buttonType);
            }
        }
        else {
            hudController.build(buttonType);
        }
    }

    private void OnMouseOver() {
        gameObject.transform.localScale = new Vector3(35, 35, 35);
        popupController.showPopup(getMessage());
    }

    private void OnMouseExit() {
        gameObject.transform.localScale = new Vector3(30, 30, 30);
        popupController.hidePopup();
    }

    private string getMessage() {
        switch (buttonType) {
            case BuildingType.LumberYard:
                return "Build a lumber yard for 8 stones.";
            case BuildingType.Farm:
                return "Build a farm for free.";
            case BuildingType.Mine:
                return "Build a mine for 8 wood.";
            case BuildingType.House:
                return "Build a house for free";
        }

        return "Error!";
    }
}
