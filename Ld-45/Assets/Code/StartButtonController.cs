using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonController : MonoBehaviour {
    
    // Public State

    public enum Type {
        Start,
        Exit
    }

    public Type type;
    

    public Sprite regularSprite;
    public Sprite blownUpSprite;

    private SpriteRenderer sr;

    private void Awake() {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnMouseOver() {
        sr.sprite = blownUpSprite;
    }

    private void OnMouseExit() {
        sr.sprite = regularSprite;
    }

    private void OnMouseDown() {
        if (type == Type.Start) {
            SceneManager.LoadScene("SampleScene");
        }
        else {
            Application.Quit();
        }
    }
}
