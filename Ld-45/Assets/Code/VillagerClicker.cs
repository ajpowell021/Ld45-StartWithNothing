using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerClicker : MonoBehaviour {
    
    // Classes

    private VillagerStats villagerStats;
    
    // Init

    private void Awake() {
        villagerStats = gameObject.GetComponent<VillagerStats>();
    }

    // On Mouse Down
    
    private void OnMouseDown() {
        villagerStats.toggleSelected();
    }
}
