using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClickerUi : MonoBehaviour {

    public enum SlideState {
        Out,
        In,
        Nothing
    }
    
    public int id;
    public bool selected;
    public SlideState state = SlideState.Nothing;
    
    private Vector3 destination;
    
    private Vector3 outPosition;
    private Vector3 inPosition;
    
    // Classes

    private SelectionManager selectionManager;
    
    private void OnMouseOver() {
        if (!selected) {
            state = SlideState.Out;
            setDestination();       
        }
    }

    private void OnMouseExit() {
        if (!selected) {
            state = SlideState.In;
            setDestination();    
        }
    }

    private void Start() {
        selectionManager = ClassManager.instance.selectionManager;
        StartCoroutine(setPositions());
    }

    private IEnumerator setPositions() {
        yield return new WaitForSeconds(.1f);
        inPosition = transform.position;
        outPosition = new Vector3(-11.7f, inPosition.y, 0);
        if (id != 1) {
            gameObject.SetActive(false);
        }
    }

    private void Update() {
        if (state != SlideState.Nothing) {
            transform.position = Vector3.Lerp(transform.position, destination, 10f * Time.deltaTime);
            checkForArrival();
        }
    }

    private void checkForArrival() {

        if (Vector3.Distance(transform.position, destination) < .1f) {
            state = SlideState.Nothing;
            transform.position = destination;
        }
    }

    private void setDestination() {
        if (state == SlideState.In) {
            destination = inPosition;
        }
        else if (state == SlideState.Out) {
            destination = outPosition;
        }
        else {
            destination = transform.position;
        }
    }

    private void OnMouseDown() {
        GameObject person = selectionManager.getCharacterById(id);
        VillagerGather gather = person.GetComponent<VillagerGather>();

        if (!gather.isWorkerBusy()) {
            if (!selected) {
                setSelected(true);
                selectionManager.selectVillagerById(id);    
            }
            else {
                setSelected(false);
                selectionManager.unselectVillagerById(id);
            }    
        }
    }

    public void setSelected(bool value) {
        selected = value;
        if (transform.position != outPosition && selected) {
            state = SlideState.Out;
            setDestination();
        }
        else if (transform.position != inPosition && !selected) {
            state = SlideState.In;
            setDestination();
        }
    }
}
