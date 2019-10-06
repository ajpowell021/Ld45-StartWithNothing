using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupController : MonoBehaviour {

    // Public State

    public GameObject popupPanel;
    private bool showPanel;
    private TextMeshProUGUI text;
    
    // Init

    private void Awake() {
        text = popupPanel.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update

    private void Update() {
        if (showPanel) {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.y += 2;
            position.z = 0;
            popupPanel.transform.position = position;
        }
    }
    
    // Public Functions

    public void showPopup(string message) {
        showPanel = true;
        popupPanel.SetActive(true);
        text.text = message;
    }

    public void hidePopup() {
        showPanel = false;
        popupPanel.SetActive(false);
    }
}
