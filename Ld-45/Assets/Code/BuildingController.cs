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

    // Classes

    private DataHolder dataHolder;
    private InputManager inputManager;
    
    // Init

    private void Awake() {
        selectedSprite = gameObject.transform.GetChild(0).gameObject;
        resourceCountObject = gameObject.transform.GetChild(1).gameObject;
        radialOne = gameObject.transform.GetChild(2).gameObject;
        radialTwo = gameObject.transform.GetChild(3).gameObject;
        radialThree = gameObject.transform.GetChild(4).gameObject;
    }

    private void Start() {
        dataHolder = ClassManager.instance.dataHolder;
        inputManager = ClassManager.instance.inputManager;
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
        resourceCountObject.SetActive(selected);
        radialOne.SetActive(selected);
        radialTwo.SetActive(selected);
        radialThree.SetActive(selected);
        updateResourceCountUi();
    }

    public void unselect() {
        selected = false;
        selectedSprite.SetActive(selected);
        resourceCountObject.SetActive(selected);
        radialOne.SetActive(selected);
        radialTwo.SetActive(selected);
        radialThree.SetActive(selected);
        updateResourceCountUi();
    }
    
    public void updateResourceCountUi() {
        resourceCountObject.GetComponentInChildren<TextMeshProUGUI>().text = currentResourcesHeld + "/" + dataHolder.buildingResourceCapacity;
    }

    // On Click

    private void OnMouseDown() {
        inputManager.buildingClicked(this);
    }
}
