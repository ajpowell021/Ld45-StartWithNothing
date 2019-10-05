using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudController : MonoBehaviour {

    // Classes
    
    private InputManager inputManager;
    private CursorManager cursorManager;
    
    // Init
    
    private void Start() {
        inputManager = ClassManager.instance.inputManager;
        cursorManager = ClassManager.instance.cursorManager;
    }
    
    // Public Functions

    public void build(BuildingType type) {
        inputManager.setInputMode(InputMode.Build);
        cursorManager.setBuildCursor(type);
    }
}
