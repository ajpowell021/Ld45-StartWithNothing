using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanClicker : MonoBehaviour {

    private float mouseDownTime;
    private DataHolder dataHolder;
    private InputManager inputManager;

    private void Start() {
        inputManager = ClassManager.instance.inputManager;
        dataHolder = ClassManager.instance.dataHolder;
    }

    private void OnMouseDown() {
        mouseDownTime = Time.time;
    }

    private void OnMouseUp() {
        if (mouseDownTime + dataHolder.mouseDragCooldown > Time.time) {
            // Did not drag
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            inputManager.oceanClicked(position);
        }
        else {
            // Drag logic.
        }
    }
}
