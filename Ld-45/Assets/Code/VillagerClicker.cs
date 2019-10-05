using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerClicker : MonoBehaviour {
    
    // Classes

    private VillagerStats villagerStats;
    private VillagerGather gather;
    
    // Init

    private void Awake() {
        villagerStats = gameObject.GetComponent<VillagerStats>();
        gather = gameObject.GetComponent<VillagerGather>();
    }

    // On Mouse Down
    
    private void OnMouseDown() {
        if (!gather.gathering) {
            villagerStats.toggleSelected();    
        }
    }
}
