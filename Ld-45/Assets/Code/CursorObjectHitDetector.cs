using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        if (other.gameObject.CompareTag("Building") || other.gameObject.CompareTag("Tree") || other.gameObject.CompareTag("Boulder")) {
            cursorManager.adjustCursorColor(false);
        }
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.CompareTag("Building") || other.gameObject.CompareTag("Tree") || other.gameObject.CompareTag("Boulder")) {

            bool hit = false;
            List<GameObject> allBuildings = GameObject.FindGameObjectsWithTag("Building").ToList();
            List<Vector3> positions = new List<Vector3>();
            for (int i = 0; i < allBuildings.Count; i++) {
                positions.Add(allBuildings[i].transform.position);
            }

            for (int i = 0; i < positions.Count; i++) {
                if (gameObject.transform.position == positions[i]) {
                    hit = true;
                }
            }

            if (!hit) {
                cursorManager.adjustCursorColor(true);    
            }
        }
    }
}
