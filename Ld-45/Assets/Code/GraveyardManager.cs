using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveyardManager : MonoBehaviour {

    public GameObject graveyard;

    public Sprite doubleDeath;
    public Sprite tripleDeath;
    public Sprite quadDeath;
    public GameObject losePanel;
    public bool lost;

    private SpriteRenderer sr;

    private TipManager tipManager;
    private InputManager inputManager;

    public int currentCharacters;
    public int deaths;


    private void Awake() {
        currentCharacters = 1;
    }

    private void Start() {
        sr = graveyard.GetComponent<SpriteRenderer>();
        tipManager = ClassManager.instance.tipManager;
        inputManager = ClassManager.instance.inputManager;
    }

    public void died(GameObject person) {
        deaths++;
        if (deaths == 1) {
            graveyard.SetActive(true);
        }
        else if (deaths == 2) {
            sr.sprite = doubleDeath;
        }
        else if (deaths == 3) {
            sr.sprite = tripleDeath;
        }
        else if (deaths == 4) {
            sr.sprite = quadDeath;
        }
        else if (deaths == 5) {
            losePanel.SetActive(true);
            lost = true;
            tipManager.addNewTip("Press R to restart");
            inputManager.setInputMode(InputMode.GameOver);
        }

        if (deaths >= currentCharacters) {
            tipManager.addNewTip("Press R to restart");
            losePanel.SetActive(true);
            lost = true;
            inputManager.setInputMode(InputMode.GameOver);
        }
    }
}
