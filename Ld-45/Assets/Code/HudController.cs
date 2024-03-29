﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour {

    // Public State

    public TextMeshProUGUI woodText;
    public TextMeshProUGUI stoneText;
    public TextMeshProUGUI cottonText;
    public TextMeshProUGUI foodText;

    // Classes
    
    private InputManager inputManager;
    private CursorManager cursorManager;
    private ResourceManager resourceManager;
    
    // Init
    
    private void Start() {
        inputManager = ClassManager.instance.inputManager;
        cursorManager = ClassManager.instance.cursorManager;
        resourceManager = ClassManager.instance.resourceManager;
    }
    
    // Public Functions

    public void build(BuildingType type) {
        inputManager.setInputMode(InputMode.Build);
        cursorManager.setBuildCursor(type);
    }

    public void updateResourceUi() {
        woodText.text = "x" + resourceManager.wood;
        stoneText.text = "x" + resourceManager.stone;
        cottonText.text = "x" + resourceManager.cotton;
        foodText.text = "x" + resourceManager.food;
    }
}
