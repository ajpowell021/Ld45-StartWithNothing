using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectionManager : MonoBehaviour {
    
    // Public Functions

    public List<GameObject> getAllVillagerObjects() {
        return GameObject.FindGameObjectsWithTag("Villager").ToList();
    }

    public List<VillagerStats> getAllVillagerStats() {
        List<GameObject> objects = getAllVillagerObjects();
        List<VillagerStats> stats = new List<VillagerStats>();

        for (int i = 0; i < objects.Count; i++) {
             stats.Add(objects[i].GetComponent<VillagerStats>());
        }

        return stats;
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

    public int getSelectedVillagerCount() {
        return getSelectedVillagerObjects().Count;
    }

    public List<VillagerMover> getSelectedVillagerMovers() {
        List<VillagerMover> movers = new List<VillagerMover>();
        List<GameObject> objects = getSelectedVillagerObjects();

        for (int i = 0; i < objects.Count; i++) {
            movers.Add(objects[i].GetComponent<VillagerMover>());
        }

        return movers;
    }

    public void unselectAllVillagers() {
        List<VillagerStats> stats = getAllVillagerStats();
        for (int i = 0; i < stats.Count; i++) {
            stats[i].setSelected(false);
        }
    }

    public List<VillagerStats> getAllVillagerStatsInBounds(Vector3 firstCorner, Vector3 secondCorner) {
        List<GameObject> villagers = getAllVillagerObjects();
        List<VillagerStats> stats = new List<VillagerStats>();
        float topLine;
        float botLine;
        float leftLine;
        float rightLine;
        
        if (firstCorner.y < secondCorner.y) {
            topLine = secondCorner.y;
            botLine = firstCorner.y;
        }
        else {
            topLine = firstCorner.y;
            botLine = secondCorner.y;
        }

        if (firstCorner.x < secondCorner.x) {
            leftLine = firstCorner.x;
            rightLine = secondCorner.x;
        }
        else {
            leftLine = secondCorner.x;
            rightLine = firstCorner.x;
        }

        for (int i = 0; i < villagers.Count; i++) {
            Vector3 position = villagers[i].transform.position;
            if (position.x >= leftLine && position.x <= rightLine && position.y <= topLine && position.y >= botLine) {
                stats.Add(villagers[i].GetComponent<VillagerStats>());
            }
        }

        return stats;
    }
}
