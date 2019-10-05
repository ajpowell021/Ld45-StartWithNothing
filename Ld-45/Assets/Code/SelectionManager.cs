using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectionManager : MonoBehaviour {
    
    // Public Functions

    public List<GameObject> getAllVillagerObjects() {
        return GameObject.FindGameObjectsWithTag("Villager").ToList();
    }
    
    public List<GameObject> getSelectedVillagerObjects() {
        List<GameObject> allVillagers = getAllVillagerObjects();
        List<GameObject> selected = new List<GameObject>();
        for (int i = 0; i < allVillagers.Count; i++) {
            if (allVillagers[i].GetComponent<VillagerStats>().selected) {
                selected.Add(allVillagers[i]);
            }
        }

        return selected;
    }
}
