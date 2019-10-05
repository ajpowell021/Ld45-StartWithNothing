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
    }
}
