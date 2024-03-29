﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandClicker : MonoBehaviour {

    // Classes

    private InputManager inputManager;
    private SelectionManager selectionManager;
    private PrefabManager prefabManager;
    private DataHolder dataHolder;
    private CharacterUiManager characterUiManager;
    
    // Private State

    private Vector3 initialClickPos;
    private Vector3 unClickPos;

    private GameObject selector;
    private SpriteRenderer selectorSprite;
    
    private float mouseDownTime;
    private bool spawnedSelector;
    
    // Init

    private void Start() {
        inputManager = ClassManager.instance.inputManager;
        selectionManager = ClassManager.instance.selectionManager;
        prefabManager = ClassManager.instance.prefabManager;
        dataHolder = ClassManager.instance.dataHolder;
        characterUiManager = ClassManager.instance.characterUiManager;
    }
    
    // Clicker

    private void OnMouseDown() {
        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        position.z = 0;
        initialClickPos = position;
        mouseDownTime = Time.time;
    }

    private void OnMouseUp() {
        if (Time.time > mouseDownTime + dataHolder.mouseDragCooldown && inputManager.inputMode == InputMode.PeopleControl) {
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
        else {
            inputManager.islandClicked(initialClickPos);
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
