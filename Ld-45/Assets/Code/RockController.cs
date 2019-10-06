using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour {

    // Public State

    public int resourcesLeft;
    
    // Classes

    private InputManager inputManager;
    private DataHolder dataHolder;
    
    // Init

    private void Start() {
        inputManager = ClassManager.instance.inputManager;
        dataHolder = ClassManager.instance.dataHolder;
        resourcesLeft = dataHolder.treeWoodStart;
    }

    // On Click

    private void OnMouseDown() {
        inputManager.boulderClicked(this);
    }

    public void checkIfEmpty() {
        if (resourcesLeft <= 0) {
            Destroy(gameObject);
        }
    }
}
