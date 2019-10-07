using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TipManager : MonoBehaviour {

    public GameObject bottomTip;
    public GameObject topTip;

    public int tipCount;

    private TextMeshProUGUI botText;
    private TextMeshProUGUI topText;

    private SoundManager soundManager;

    private void Awake() {
        botText = bottomTip.GetComponentInChildren<TextMeshProUGUI>();
        topText = topTip.GetComponentInChildren<TextMeshProUGUI>();

        topTip.SetActive(false);
        bottomTip.SetActive(false);
    }

    private void Start() {
        soundManager = ClassManager.instance.soundManager;
        addNewTip("You eat and sleep at houses");
        StartCoroutine(buildAHouse());
    }

    // Public Functions

    public void addNewTip(string message) {
        soundManager.playWhistleSound();
        tipCount++;
        if (tipCount > 2) {
            topText.text = botText.text;
            botText.text = message;
        }
        else if (tipCount == 2) {
            topTip.SetActive(true);
            topText.text = message;
        }
        else if (tipCount == 1) {
            bottomTip.SetActive(true);
            botText.text = message;
        }
    }

    private IEnumerator buildAHouse() {
        yield return new WaitForSeconds(10);
        addNewTip("You should build a house");
        StartCoroutine(chopAndCut());
    }
    
    private IEnumerator chopAndCut() {
        yield return new WaitForSeconds(10);
        addNewTip("Trees and rocks are useful");
        StartCoroutine(fishing());
    }

    private IEnumerator fishing() {
        yield return new WaitForSeconds(10);
        addNewTip("This game has fishing!");
    }
}
