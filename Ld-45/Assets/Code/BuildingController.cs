using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingController : MonoBehaviour {
    
    // Public State

    public bool selected;
    public BuildingType buildingType;
    public int currentResourcesHeld;

    // Private State

    private float timeOfLastHarvest;
    private GameObject selectedSprite;
    private GameObject resourceCountObject;
    private GameObject radialOne;
    private GameObject radialTwo;
    private GameObject radialThree;
    private GameObject radialAnim;
    private GameObject sleepAnim;
    private GameObject munchAnim;

    public bool beingWorkedOn;

    // Classes

    private DataHolder dataHolder;
    private InputManager inputManager;
    private SelectionManager selectionManager;
    
    // Init

    private void Awake() {
        selectedSprite = gameObject.transform.GetChild(0).gameObject;
        resourceCountObject = gameObject.transform.GetChild(1).gameObject;
        radialOne = gameObject.transform.GetChild(2).gameObject;
        radialTwo = gameObject.transform.GetChild(3).gameObject;
        radialThree = gameObject.transform.GetChild(4).gameObject;
        radialAnim = gameObject.transform.GetChild(5).gameObject;
        if (buildingType == BuildingType.House) {
            sleepAnim = gameObject.transform.GetChild(6).gameObject;
            munchAnim = gameObject.transform.GetChild(7).gameObject;
        }
    }

    private void Start() {
        dataHolder = ClassManager.instance.dataHolder;
        inputManager = ClassManager.instance.inputManager;
        selectionManager = ClassManager.instance.selectionManager;
        timeOfLastHarvest = Time.time;
    }
    
    // Update

    private void Update() {
        if (currentResourcesHeld < dataHolder.buildingResourceCapacity) {
            if (Time.time > timeOfLastHarvest + dataHolder.buildingResourceGatheringSpeed) {
                currentResourcesHeld++;
                timeOfLastHarvest = Time.time;
                updateResourceCountUi();
            }
        }
    }
    
    // Public Functions

    public void toggleBuildingSelect() {
        selected = !selected;
        selectedSprite.SetActive(selected);
        if (buildingType == BuildingType.Mine || buildingType == BuildingType.LumberYard) {
            resourceCountObject.SetActive(selected);    
        }
        radialOne.SetActive(selected);
        radialTwo.SetActive(selected);
        radialThree.SetActive(selected);
        radialAnim.SetActive(selected);
        updateResourceCountUi();
    }

    public void unselect() {
        selected = false;
        selectedSprite.SetActive(selected);
        resourceCountObject.SetActive(selected);
        radialOne.SetActive(selected);
        radialTwo.SetActive(selected);
        radialThree.SetActive(selected);
        radialAnim.SetActive(selected);
        updateResourceCountUi();
    }
    
    public void updateResourceCountUi() {
        resourceCountObject.GetComponentInChildren<TextMeshProUGUI>().text = currentResourcesHeld + "/" + dataHolder.buildingResourceCapacity;
    }

    public void setSleeping(bool value) {
        sleepAnim.SetActive(value);
    }

    public void setEating(bool value) {
        munchAnim.SetActive(value);
    }

    // On Click

    private void OnMouseDown() {
        if (!(buildingType == BuildingType.House && beingWorkedOn)) {
            if (selected) {
                toggleBuildingSelect();
            }
            else {
                selectionManager.unselectAllBuildings();
                toggleBuildingSelect();
            }
            inputManager.setInputMode(!selected ? InputMode.PeopleControl : InputMode.BuidlingSelected);    
        }
    }
}
