using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftController : MonoBehaviour {

    // Starts at x = 20
    // Ends at x = 0
    
    // Classes

    private DataHolder dataHolder;
    private PrefabManager prefabManager;
    private CharacterUiManager characterUiManager;
    
    // Init

    private void Start() {
        dataHolder = ClassManager.instance.dataHolder;
        prefabManager = ClassManager.instance.prefabManager;
        characterUiManager = ClassManager.instance.characterUiManager;
    }
    
    // Update

    private void Update() {
        transform.Translate(dataHolder.boatSpeed * Time.deltaTime * Vector3.left);
        checkArrival();
    }
    
    // Private Functions

    private void checkArrival() {
        if (Vector3.Distance(transform.position, new Vector3(0, -7, 0)) < .5f) {
            GameObject person = Instantiate(prefabManager.villager, new Vector3(-.5f, -6.5f, 0), Quaternion.identity);
            person.GetComponent<VillagerStats>().id = dataHolder.nextPersonId;
            characterUiManager.newCharacterArrived(dataHolder.nextPersonId);
            dataHolder.nextPersonId++;
            Destroy(gameObject);
        }
    }
}
