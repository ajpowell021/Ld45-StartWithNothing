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

    // Init

    private void Start() {
        hudController = ClassManager.instance.hudController;
        resourceManager = ClassManager.instance.resourceManager;
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
    }

    private void OnMouseExit() {
        gameObject.transform.localScale = new Vector3(30, 30, 30);
    }
}
