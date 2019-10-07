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

    public void move(Vector3 newDestination, bool gathering = false, bool toOcean = false) {
        if (!toOcean) {
            destination = newDestination;
        }
        else {
            destination = getOceanDestination(newDestination);
        }

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

    private Vector3 getOceanDestination(Vector3 position) {
        Vector3 returnPosition = new Vector3();
        if (position.x > -1.5f) {
            // Right side
            if (position.y > 7.29) {
                returnPosition = new Vector3(.66f, 7.29f, 0);
            }
            else if (position.y > 6) {
                returnPosition = new Vector3(3.77f, 6, 0);
            }
            else if (position.y > 5) {
                returnPosition = new Vector3(4.7f, 5, 0);
            }
            else if (position.y > 2.5f) {
                returnPosition = new Vector3(5.57f, 4.21f, 0);
            } 
            else if (position.y > 0) {
                returnPosition = new Vector3(5.57f, 1.5f, 0);
            }
            else if (position.y > -1.59f) {
                returnPosition = new Vector3(5.57f, -1.59f, 0);
            }
            else if (position.y > -3.51f) {
                returnPosition = new Vector3(4.62f, -3.51f, 0);
            }
            else if (position.y > -4.47f) {
                returnPosition = new Vector3(3.5f, -4.47f, 0);
            }
            else if (position.y > -5.53f) {
                returnPosition = new Vector3(2.64f, -5.53f, 0);
            }
            else if (position.y > -6.55f) {
                returnPosition = new Vector3(.52f, -6.55f, 0);
            }
            else {
                returnPosition = new Vector3(-.48f, -7.41f, 0);
            }
        }
        else {
            // Left side
            if (position.y > 6.3f) {
                returnPosition = new Vector3(-4.59f, 6.3f, 0);
            }
            else if (position.y > 4.57f) {
                returnPosition = new Vector3(-6.54f, 4.57f, 0);
            }
            else if (position.y > 2f) {
                returnPosition = new Vector3(-6.54f, 2f, 0);
            }
            else if (position.y > .55f) {
                returnPosition = new Vector3(-6.54f, .55f, 0);
            }
            else if (position.y > -2.38f) {
                returnPosition = new Vector3(-4.61f, -2.38f, 0);
            }
            else if (position.y > -4.15f) {
                returnPosition = new Vector3(-2.48f, -4.15f, 0);
            }
            else {
                returnPosition = new Vector3(-2.48f, -7.38f, 0);
            }
        }

        return returnPosition;
    }
}
