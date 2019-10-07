using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerMover : MonoBehaviour {

    // Classes

    private DataHolder dataHolder;
    private VillagerStats stats;
    private VillagerGather gatherScript;
    
    // Private State

    private Vector3 destination;
    private bool isMoving;
    private Animator animator;
    private bool onWayToGather;
    private bool onWayToSleep;
    private bool onWayToEat;
    
    // Init 

    private void Awake() {
        animator = gameObject.GetComponent<Animator>();
        stats = gameObject.GetComponent<VillagerStats>();
        gatherScript = gameObject.GetComponent<VillagerGather>();
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

    public void move(Vector3 newDestination, bool gathering = false) {
        destination = newDestination;
        onWayToGather = gathering;
        isMoving = true;
        if (destination.y > transform.position.y) {
            animator.SetInteger("runningState", 2);
        }
        else {
            animator.SetInteger("runningState", 1);
        }
    }

    public void moveToEat(Vector3 newDestination) {
        destination = newDestination;
        onWayToEat = true;
        isMoving = true;
        if (destination.y > transform.position.y) {
            animator.SetInteger("runningState", 2);
        }
        else {
            animator.SetInteger("runningState", 1);
        }
    }

    public void moveToSleep(Vector3 newDestination) {
        destination = newDestination;
        onWayToSleep = true;
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
        if (Vector3.Distance(transform.position, destination) < .5f + stats.id * .2f) {
            isMoving = false;
            animator.SetInteger("runningState", 0);
            if (onWayToGather) {
                gatherScript.arrivedAtBuilding();
                onWayToGather = false;
            }
            else if (onWayToEat) {
                gatherScript.arrivedAtEat();
                onWayToEat = false;
            }
            else if (onWayToSleep) {
                gatherScript.arrivedAtSleep();
                onWayToSleep = false;
            }
        }
    }
}
