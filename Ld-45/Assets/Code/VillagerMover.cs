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
    private Animator animator;
    
    // Init 

    private void Awake() {
        animator = gameObject.GetComponent<Animator>();
    }

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
        if (destination.y > transform.position.y) {
            animator.SetInteger("runningState", 2);
        }
        else {
            animator.SetInteger("runningState", 1);
        }
    }
    
    // Private Functions

    private void checkIfArrived() {
        if (Vector3.Distance(transform.position, destination) < .5f) {
            isMoving = false;
            animator.SetInteger("runningState", 0);
        }
    }
}
