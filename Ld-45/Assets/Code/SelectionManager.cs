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

    public List<VillagerMover> getSelectedVillagerMovers() {
        List<VillagerMover> movers = new List<VillagerMover>();
        List<GameObject> objects = getSelectedVillagerObjects();

        for (int i = 0; i < objects.Count; i++) {
            movers.Add(objects[i].GetComponent<VillagerMover>());
        }

        return movers;
    }
}
