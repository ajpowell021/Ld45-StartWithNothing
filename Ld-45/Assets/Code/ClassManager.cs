using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassManager : MonoBehaviour {
 
    // Instance

    public static ClassManager instance = null;
    
    // Objects
    
    // Classes

    public InputManager inputManager;
    public DataHolder dataHolder;
    public SelectionManager selectionManager;
    public PrefabManager prefabManager;
    public HudController hudController;
    public CursorManager cursorManager;
    public ResourceManager resourceManager;
    public RandomResourceSpawner randomResourceSpawner;
    
    // Init

    private void Awake() {
        setInstance();
        setObjects();
        setClasses();
    }
    
    // Private Functions

    private void setInstance() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this);
        }
    }

    private void setObjects() {
        
    }

    private void setClasses() {
        inputManager = gameObject.GetComponent<InputManager>();
        dataHolder = gameObject.GetComponent<DataHolder>();
        selectionManager = gameObject.GetComponent<SelectionManager>();
        prefabManager = gameObject.GetComponent<PrefabManager>();
        hudController = gameObject.GetComponent<HudController>();
        cursorManager = gameObject.GetComponent<CursorManager>();
        resourceManager = gameObject.GetComponent<ResourceManager>();
        randomResourceSpawner = gameObject.GetComponent<RandomResourceSpawner>();
    }
}
