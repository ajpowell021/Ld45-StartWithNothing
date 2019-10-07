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

    public List<VillagerStats> getAllSelectedVillagerStats() {
        List<GameObject> objects = getAllVillagerObjects();
        List<VillagerStats> stats = new List<VillagerStats>();
        List<VillagerStats> selectedStats = new List<VillagerStats>();

        
        for (int i = 0; i < objects.Count; i++) {
            stats.Add(objects[i].GetComponent<VillagerStats>());
        }

        for (int i = 0; i < stats.Count; i++) {
            if (stats[i].selected) {
                selectedStats.Add(stats[i]);
            }
        }

        return selectedStats;
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

    public void unselectAllBuildings() {
        List<GameObject> buildings = GameObject.FindGameObjectsWithTag("Building").ToList();
        for (int i = 0; i < buildings.Count; i++) {
            buildings[i].GetComponent<BuildingController>().unselect();
        }
        
        turnOnAllBuildingHitBoxes();
    }

    public void selectVillagerById(int id) {
        unselectAllBuildings();

        List<GameObject> people = getAllVillagerObjects();
        for (int i = 0; i < people.Count; i++) {
            VillagerStats stats = people[i].GetComponent<VillagerStats>();
            if (stats.id == id) {
                stats.setSelected(true);
                return;
            }
        }
    }

    public void unselectVillagerById(int id) {
        List<VillagerStats> stats = getAllSelectedVillagerStats();
        for (int i = 0; i < stats.Count; i++) {
            if (stats[i].id == id) {
                stats[i].setSelected(false);
                return;
            }
        }
    }

    public GameObject getCharacterById(int id) {
        List<GameObject> people = getAllVillagerObjects();
        for (int i = 0; i < people.Count; i++) {
            VillagerStats stats = people[i].GetComponent<VillagerStats>();
            if (stats.id == id) {
                return people[i];
            }
        }

        Debug.LogError("NO PERSON OF THIS ID FOUND");
        return people[0];
    }

    public void turnOffHitBoxOfOtherBuildings() {
        List<GameObject> buildings = GameObject.FindGameObjectsWithTag("Building").ToList();
        for (int i = 0; i < buildings.Count; i++) {
            if (!buildings[i].GetComponent<BuildingController>().selected) {
                buildings[i].GetComponent<BoxCollider>().enabled = false;
            }
        }

        List<GameObject> buildingSites = GameObject.FindGameObjectsWithTag("BuildingSite").ToList();
        for (int i = 0; i < buildingSites.Count; i++) {
            buildingSites[i].GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void turnOnAllBuildingHitBoxes() {
        List<GameObject> buildings = GameObject.FindGameObjectsWithTag("Building").ToList();
        for (int i = 0; i < buildings.Count; i++) {
            buildings[i].GetComponent<BoxCollider>().enabled = true;
        }
        
        List<GameObject> buildingSites = GameObject.FindGameObjectsWithTag("BuildingSite").ToList();
        for (int i = 0; i < buildingSites.Count; i++) {
            buildingSites[i].GetComponent<BoxCollider>().enabled = true;
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
                VillagerGather gather = villagers[i].GetComponent<VillagerGather>();
                if (!gather.isWorkerBusy()) {
                    stats.Add(villagers[i].GetComponent<VillagerStats>());    
                }
            }
        }

        return stats;
    }
}
