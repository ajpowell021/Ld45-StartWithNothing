using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour {

    // Public State

    public Sprite zero;
    public Sprite one;
    public Sprite two;
    public Sprite three;
    public Sprite four;
    public Sprite five;
    public Sprite six;
    public Sprite seven;
    public Sprite eight;
    public Sprite nine;
    public Sprite ten;

    public SpriteRenderer renderer;

    public float percent;
    
    // Init

    private void Awake() {
        renderer = gameObject.GetComponent<SpriteRenderer>();
    }
    
    // Public Functions

    public void adjustPercent(int amount) {
        percent += amount;

        if (percent < 10) {
            renderer.sprite = zero;
        }
        else if (percent < 20) {
            renderer.sprite = one;
        }
        else if (percent < 30) {
            renderer.sprite = two;
        }
        else if (percent < 40) {
            renderer.sprite = three;
        }
        else if (percent < 50) {
            renderer.sprite = four;
        }
        else if (percent < 60) {
            renderer.sprite = five;
        }
        else if (percent < 70) {
            renderer.sprite = six;
        }
        else if (percent < 80) {
            renderer.sprite = seven;
        }
        else if (percent < 90) {
            renderer.sprite = eight;
        }
        else if (percent < 100) {
            renderer.sprite = nine;
        }
        else {
            renderer.sprite = ten;
        }
    }
}
