using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanClicker : MonoBehaviour {

    private float mouseDownTime;
    private DataHolder dataHolder;
    private InputManager inputManager;
    private PrefabManager prefabManager;
    private SelectionManager selectionManager;
    
    private void Start() {
        inputManager = ClassManager.instance.inputManager;
        dataHolder = ClassManager.instance.dataHolder;
        prefabManager = ClassManager.instance.prefabManager;
        selectionManager = ClassManager.instance.selectionManager;
    }

    private void OnMouseDown() {
        mouseDownTime = Time.time;
    }

    private void OnMouseUp() {
        if (mouseDownTime + dataHolder.mouseDragCooldown > Time.time) {
            if (selectionManager.getSelectedVillagerCount() > 0) {
                // Did not drag
                Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                position.z = 0;
                GameObject radial = Instantiate(prefabManager.fishingRadial, position, Quaternion.identity);
                if (!inputManager.fishingRadial) {
                    inputManager.fishingRadialActive = true;
                    inputManager.fishingRadial = radial;    
                }
                else {
                    Destroy(inputManager.fishingRadial);
                    inputManager.fishingRadial = radial;
                }
            }
        }
        else {
            // Drag logic.
        }
    }
}
