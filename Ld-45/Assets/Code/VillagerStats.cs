using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerStats : MonoBehaviour {

    // Public State

    public string villagerName;
    public bool selected;
    public int id;
    
    // Private State

    private GameObject marqueeObject;
    
    // Init

    private void Awake() {
        marqueeObject = gameObject.transform.GetChild(0).gameObject;
    }
    
    // Public Functions

    public void setSelected(bool newSelected) {
        selected = newSelected;
        if (selected) {
            marqueeObject.SetActive(true);
        }
        else {
            marqueeObject.SetActive(false);
        }
    }

    public void toggleSelected() {
        setSelected(!selected);
    }
}
