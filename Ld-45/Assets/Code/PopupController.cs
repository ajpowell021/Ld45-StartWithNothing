using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupController : MonoBehaviour {

    // Public State

    public GameObject popupPanel;
    public GameObject winConditionPanel;
    private bool showPanel;
    private bool showWinPanel;
    private bool won;
    private TextMeshProUGUI text;
    private TextMeshProUGUI winText;

    // Init

    private void Awake() {
        text = popupPanel.GetComponentInChildren<TextMeshProUGUI>();
        winText = winConditionPanel.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update

    private void Update() {
        if (showPanel) {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.y += 2;
            position.z = 0;
            popupPanel.transform.position = position;
        }
        else if (showWinPanel) {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.y += 3;
            position.z = 0;
            winConditionPanel.transform.position = position;
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

    public void showWin(string message) {
        if (!won) {
            showWinPanel = true;
            winConditionPanel.SetActive(true);
            winText.text = message;    
        }
    }

    public void hideWin(bool finalHide = false) {
        if (finalHide) {
            won = true;
        }
        showWinPanel = false;
        winConditionPanel.SetActive(false);
    }
}
