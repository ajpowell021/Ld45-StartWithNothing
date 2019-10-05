﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingButton : MonoBehaviour {
    
    // Public State

    public BuildingType buttonType;
    
    // Classes

    private HudController hudController;

    // Init

    private void Start() {
        hudController = ClassManager.instance.hudController;
    }

    // Private Functions
    
    private void OnMouseDown() {
        hudController.build(buttonType);
    }

    private void OnMouseOver() {
        gameObject.transform.localScale = new Vector3(35, 35, 35);
    }

    private void OnMouseExit() {
        gameObject.transform.localScale = new Vector3(30, 30, 30);
    }
}
