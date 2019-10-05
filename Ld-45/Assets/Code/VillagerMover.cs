using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerMover : MonoBehaviour {

    // Classes

    private DataHolder dataHolder;
    
    // Private State

    private Vector3 destination;

    // Init 

    private void Start() {
        dataHolder = ClassManager.instance.dataHolder;
    }
    
    // Public Functions

    public void move(Vector3 newDestination) {
        destination = newDestination;
    }
}
