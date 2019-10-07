using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerStats : MonoBehaviour {

    // Public State
    
    public bool selected;
    public int id;
    public int hungerLevel;
    public int sleepLevel;
    public bool dead;
    
    // Private State

    private GameObject marqueeObject;
    private float lastTimeHungerAdvanced;
    private float lastTimeSleepAdvanced;

    // Classes

    private DataHolder dataHolder;
    private CharacterUiManager characterUiManager;
    private GraveyardManager graveyardManager;
    private SpriteRenderer sr;
    
    // Init

    private void Awake() {
        marqueeObject = gameObject.transform.GetChild(0).gameObject;
        sr = gameObject.GetComponent<SpriteRenderer>();
        lastTimeHungerAdvanced = Time.time;
        lastTimeSleepAdvanced = Time.time;
    }

    private void Start() {
        dataHolder = ClassManager.instance.dataHolder;
        characterUiManager = ClassManager.instance.characterUiManager;
        graveyardManager = ClassManager.instance.graveyardManager;
    }

    // Update

    private void Update() {
        if (!dead) {
            if (Time.time > lastTimeHungerAdvanced + dataHolder.timeBetweenHunger) {
                hungerLevel++;
                lastTimeHungerAdvanced = Time.time;
                deathCheck();
            }

            if (Time.time > lastTimeSleepAdvanced + dataHolder.timeBetweenTired) {
                sleepLevel++;
                lastTimeSleepAdvanced = Time.time;
                sleepCheck();
            }    
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

    public void slept() {
        sleepLevel = 0;
        characterUiManager.setSleepIcon(id, false);
    }
    
    // Private Functions

    private void deathCheck() {
        if (hungerLevel > 2) {
            characterUiManager.setHungerIcon(id, true);
        }
        if (hungerLevel > 5) {
            graveyardManager.died(gameObject);
            sr.enabled = false;
            dead = true;
            setSelected(false);
            characterUiManager.died(id);
        }
    }

    private void sleepCheck() {
        if (sleepLevel > 2) {
            characterUiManager.setSleepIcon(id, true);
        }

        if (hungerLevel > 5) {
            graveyardManager.died(gameObject);
            sr.enabled = false;
            dead = true;
            setSelected(false);
            characterUiManager.died(id);
        }
    }
}
