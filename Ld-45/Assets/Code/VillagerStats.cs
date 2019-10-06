using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerStats : MonoBehaviour {

    // Public State
    
    public bool selected;
    public int id;
    public int hungerLevel;
    
    // Private State

    private GameObject marqueeObject;
    private float lastTimeHungerAdvanced;

    // Classes

    private DataHolder dataHolder;
    private CharacterUiManager characterUiManager;
    
    // Init

    private void Awake() {
        marqueeObject = gameObject.transform.GetChild(0).gameObject;
        lastTimeHungerAdvanced = Time.time;
    }

    private void Start() {
        dataHolder = ClassManager.instance.dataHolder;
        characterUiManager = ClassManager.instance.characterUiManager;
    }

    // Update

    private void Update() {
        if (Time.time > lastTimeHungerAdvanced + dataHolder.timeBetweenHunger) {
            hungerLevel++;
            lastTimeHungerAdvanced = Time.time;
            deathCheck();
        }
    }

    // Public Functions

    public void setSelected(bool newSelected) {
        selected = newSelected;
        if (selected) {
            marqueeObject.SetActive(true);
        }
        else {
            marqueeObject.SetActive(false);
        }
    }

    public void toggleSelected() {
        setSelected(!selected);
    }

    public void ateFood() {
        hungerLevel = 0;
        characterUiManager.setHungerIcon(id, false);
    }
    
    // Private Functions

    private void deathCheck() {
        if (hungerLevel > 2) {
            characterUiManager.setHungerIcon(id, true);
        }
        if (hungerLevel > 5) {
            // They die here!
        }
    }
}
