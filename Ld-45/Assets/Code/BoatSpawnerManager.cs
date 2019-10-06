﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSpawnerManager : MonoBehaviour {

    // Classes

    private PrefabManager prefabManager;
    private DataHolder dataHolder;
    
    // Private State

    private float timeOfLastSpawn;
    
    // Init

    private void Start() {
        prefabManager = ClassManager.instance.prefabManager;
        dataHolder = ClassManager.instance.dataHolder;
        timeOfLastSpawn = Time.time;
    }

    private void Update() {
        if (dataHolder.nextPersonId < 6) {
            if (timeOfLastSpawn + dataHolder.timeBetweenBoats < Time.time) {
                Instantiate(prefabManager.boat, new Vector3(20, -7, 0), Quaternion.identity);
                timeOfLastSpawn = Time.time;
            }    
        }
    }
}