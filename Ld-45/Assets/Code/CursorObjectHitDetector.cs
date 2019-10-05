using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorObjectHitDetector : MonoBehaviour {

    // Classes

    private CursorManager cursorManager;
    
    // Init

    private void Start() {
        cursorManager = ClassManager.instance.cursorManager;
    }
    
    // Collisions

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Building")) {
            cursorManager.adjustCursorColor(false);
        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.CompareTag("Building")) {
            cursorManager.adjustCursorColor(true);
        }
    }
}
