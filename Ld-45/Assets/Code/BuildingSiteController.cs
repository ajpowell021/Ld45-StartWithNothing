using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSiteController : MonoBehaviour {
    
    // Public State

    public BuildingType buildingType;
    public int donePercent;
    
    // Classes

    private InputManager inputManager;
    private PrefabManager prefabManager;
    private ProgressBar progressBar;
    private SpriteRenderer spriteRenderer;
    
    // Init

    private void Awake() {
        progressBar = gameObject.GetComponentInChildren<ProgressBar>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start() {
        inputManager = ClassManager.instance.inputManager;
        prefabManager = ClassManager.instance.prefabManager;
    }

    // Public Function

    public IEnumerator setBuildingType(BuildingType type) {
        yield return new WaitForFixedUpdate();
        buildingType = type;
        spriteRenderer.sprite = prefabManager.getPrefabFromBuildingType(buildingType).GetComponent<SpriteRenderer>().sprite;
    }

    public void finishedBuilding() {
        Instantiate(prefabManager.getPrefabFromBuildingType(buildingType), transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void doWork() {
        donePercent += 10;
        if (donePercent >= 100) {
            finishedBuilding();
        }
        else {
            progressBar.adjustPercent(10);
        }
    }

    public void OnMouseDown() {
        inputManager.buildingSiteClicked(this);
    }
}
