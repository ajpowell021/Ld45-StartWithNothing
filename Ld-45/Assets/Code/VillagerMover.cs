using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerMover : MonoBehaviour {

    // Classes

    private DataHolder dataHolder;
    
    // Private State

    private Vector3 destination;
    private bool isMoving;

    // Init 

    private void Start() {
        dataHolder = ClassManager.instance.dataHolder;
    }

    // Update
    
    private void Update() {
        if (isMoving) {
            Vector3 heading = destination - transform.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;
            
            transform.Translate(dataHolder.villagerSpeed * Time.deltaTime * direction);
            checkIfArrived();
        }
    }

    // Public Functions

    public void move(Vector3 newDestination) {
        destination = newDestination;
        isMoving = true;
    }
    
    // Private Functions

    private void checkIfArrived() {
        if (Vector3.Distance(transform.position, destination) < .5f) {
            isMoving = false;
        }
    }
}
