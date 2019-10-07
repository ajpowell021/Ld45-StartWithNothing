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
    private CharacterUiManager characterUiManager;
    private Vector3 unClickPos;
    private Vector3 initialClickPos;
    private bool spawnedSelector;
    private GameObject selector;
    private SpriteRenderer selectorSprite;
    
    private void Start() {
        inputManager = ClassManager.instance.inputManager;
        dataHolder = ClassManager.instance.dataHolder;
        prefabManager = ClassManager.instance.prefabManager;
        selectionManager = ClassManager.instance.selectionManager;
        characterUiManager = ClassManager.instance.characterUiManager;
    }

    private void OnMouseDown() {
        mouseDownTime = Time.time;
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        initialClickPos = position;
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
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            unClickPos = position;

            // I have coords here.
            List<VillagerStats> stats = selectionManager.getAllVillagerStatsInBounds(initialClickPos, unClickPos);
            for (int i = 0; i < stats.Count; i++) {
                stats[i].setSelected(true);
                characterUiManager.characterSelected(stats[i].id, true);
            }

            Destroy(selector);
            spawnedSelector = false;
        }
    }

    private void OnMouseDrag() {
        if (inputManager.inputMode == InputMode.PeopleControl) {
            if (Time.time > mouseDownTime + dataHolder.mouseDragCooldown) {
                if (!spawnedSelector) {
                    selector = Instantiate(prefabManager.selectorBox, initialClickPos, Quaternion.identity);
                    selectorSprite = selector.GetComponent<SpriteRenderer>();
                    spawnedSelector = true;
                }
                else {
                    Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    position.z = 0;
                    unClickPos = position;
                    float x = initialClickPos.x - unClickPos.x;
                    float y = initialClickPos.y - unClickPos.y;
                    selectorSprite.size = new Vector2(x, y);
                    selector.transform.position = new Vector3((initialClickPos.x + unClickPos.x) / 2, (initialClickPos.y + unClickPos.y) / 2, 0);
                }
            }    
        }
    }
}
