using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomResourceSpawner : MonoBehaviour {

    public int treesToSpawnAtStart;
    
    // Classes

    private PrefabManager prefabManager;
    
    // Init

    private void Start() {
        prefabManager = ClassManager.instance.prefabManager;
        spawnTreesAtStart();
        spawnBouldersAtStart();
        
        InvokeRepeating("resourceCheck", 60f, 60f);
    }

    // top: 8
    // bottom: -7
    // left: -6
    // right: 4


    // Public Functions

    public void spawnTreesAtStart() {
        for (int i = 0; i < treesToSpawnAtStart; i++) {
            List<Vector3> emptySpots = getListOfEmptySpots();
            int roll = Random.Range(0, emptySpots.Count);
            GameObject tree = Instantiate(prefabManager.treeZero, emptySpots[roll], Quaternion.identity);
            tree.GetComponent<SpriteRenderer>().sprite = prefabManager.getRandomTreeSprite();
        }
    }

    public void spawnBouldersAtStart() {
        for (int i = 0; i < treesToSpawnAtStart; i++) {
            List<Vector3> emptySpots = getListOfEmptySpots();
            int roll = Random.Range(0, emptySpots.Count);
            GameObject rock = Instantiate(prefabManager.rockOne, emptySpots[roll], Quaternion.identity);
            rock.GetComponent<SpriteRenderer>().sprite = prefabManager.getRandomRockSprite();
        }
    }

    public int getTreeCount() {
        List<GameObject> trees = GameObject.FindGameObjectsWithTag("Tree").ToList();
        return trees.Count;
    }
    
    public int getBoulderCount() {
        List<GameObject> boulders = GameObject.FindGameObjectsWithTag("Boulder").ToList();
        return boulders.Count;
    }

    private void resourceCheck() {
        int treesToSpawn = treesToSpawnAtStart - getTreeCount();
        for (int i = 0; i < treesToSpawn; i++) {
            List<Vector3> emptySpots = getListOfEmptySpots();
            int roll = Random.Range(0, emptySpots.Count);
            GameObject tree = Instantiate(prefabManager.treeZero, emptySpots[roll], Quaternion.identity);
            tree.GetComponent<SpriteRenderer>().sprite = prefabManager.getRandomTreeSprite();
        }

        int bouldersToSpawn = treesToSpawnAtStart - getBoulderCount();
        for (int i = 0; i < bouldersToSpawn; i++) {
            List<Vector3> emptySpots = getListOfEmptySpots();
            int roll = Random.Range(0, emptySpots.Count);
            GameObject rock = Instantiate(prefabManager.rockOne, emptySpots[roll], Quaternion.identity);
            rock.GetComponent<SpriteRenderer>().sprite = prefabManager.getRandomRockSprite();
        }
    }
    
    // Private Functions

    private List<Vector3> getListOfEmptySpots() {
        List<Vector3> emptySpots = new List<Vector3>();
        List<GameObject> allBuildings = GameObject.FindGameObjectsWithTag("Building").ToList();
        List<Vector3> buildingSpots = new List<Vector3>();

        allBuildings.AddRange(GameObject.FindGameObjectsWithTag("Tree"));
        allBuildings.AddRange(GameObject.FindGameObjectsWithTag("Boulder"));
        buildingSpots.AddRange(spotsToRemove());
        
        for (int i = 0; i < allBuildings.Count; i++) {
            buildingSpots.Add(allBuildings[i].transform.position);
        }
        
        for (int i = -7; i < 9; i++) {
            for (int j = -6; j < 4; j++) {
                emptySpots.Add(new Vector3(j, i, 0));
            }
        }

        for (int i = 0; i < buildingSpots.Count; i++) {
            emptySpots.Remove(buildingSpots[i]);
        }

        return emptySpots;
    }

    private List<Vector3> spotsToRemove() {
        List<Vector3> remove = new List<Vector3>();
        
        // Top Left Corner
        
        remove.Add(new Vector3(-6, 8, 0));
        remove.Add(new Vector3(-6, 7, 0));
        remove.Add(new Vector3(-6, 6, 0));
        remove.Add(new Vector3(-6, 5, 0));
        remove.Add(new Vector3(-5, 6, 0));
        remove.Add(new Vector3(-5, 7, 0));
        remove.Add(new Vector3(-5, 8, 0));
        remove.Add(new Vector3(-5, 7, 0));
        remove.Add(new Vector3(-5, 8, 0));
        remove.Add(new Vector3(-4, 7, 0));
        remove.Add(new Vector3(-4, 8, 0));
        remove.Add(new Vector3(-3, 7, 0));
        remove.Add(new Vector3(-3, 8, 0));

        // Upper Right
        
        remove.Add(new Vector3(1, 8, 0));
        remove.Add(new Vector3(2, 8, 0));
        remove.Add(new Vector3(2, 7, 0));
        remove.Add(new Vector3(3, 8, 0));
        remove.Add(new Vector3(3, 7, 0));
        remove.Add(new Vector3(3, 6, 0));
        remove.Add(new Vector3(4, 8, 0));
        remove.Add(new Vector3(4, 7, 0));
        remove.Add(new Vector3(4, 6, 0));
        remove.Add(new Vector3(4, 5, 0));
        remove.Add(new Vector3(4, 4, 0));
        
        
        // Lower Right
        
        remove.Add(new Vector3(4, -3, 0));
        remove.Add(new Vector3(4, -4, 0));
        remove.Add(new Vector3(4, -5, 0));
        remove.Add(new Vector3(4, -6, 0));
        remove.Add(new Vector3(4, -7, 0));
        remove.Add(new Vector3(3, -3, 0));
        remove.Add(new Vector3(3, -4, 0));
        remove.Add(new Vector3(3, -5, 0));
        remove.Add(new Vector3(3, -6, 0));
        remove.Add(new Vector3(3, -7, 0));
        remove.Add(new Vector3(2, -4, 0));
        remove.Add(new Vector3(2, -5, 0));
        remove.Add(new Vector3(2, -6, 0));
        remove.Add(new Vector3(2, -7, 0));
        remove.Add(new Vector3(1, -4, 0));
        remove.Add(new Vector3(1, -5, 0));
        remove.Add(new Vector3(1, -6, 0));
        remove.Add(new Vector3(1, -7, 0));
        remove.Add(new Vector3(0, -5, 0));
        remove.Add(new Vector3(0, -6, 0));
        remove.Add(new Vector3(0, -7, 0));
        
        // Lower Left
        remove.Add(new Vector3(-3, -4, 0));
        remove.Add(new Vector3(-3, -5, 0));
        remove.Add(new Vector3(-3, -6, 0));
        remove.Add(new Vector3(-3, -7, 0));
        
        remove.Add(new Vector3(-4, -3, 0));
        remove.Add(new Vector3(-4, -4, 0));
        remove.Add(new Vector3(-4, -5, 0));
        remove.Add(new Vector3(-4, -6, 0));
        remove.Add(new Vector3(-4, -7, 0));
        
        remove.Add(new Vector3(-5, -2, 0));
        remove.Add(new Vector3(-5, -3, 0));
        remove.Add(new Vector3(-5, -4, 0));
        remove.Add(new Vector3(-5, -5, 0));
        remove.Add(new Vector3(-5, -6, 0));
        remove.Add(new Vector3(-5, -7, 0));
        
        remove.Add(new Vector3(-6, 0, 0));
        remove.Add(new Vector3(-6, -1, 0));
        remove.Add(new Vector3(-6, -2, 0));
        remove.Add(new Vector3(-6, -3, 0));
        remove.Add(new Vector3(-6, -4, 0));
        remove.Add(new Vector3(-6, -5, 0));
        remove.Add(new Vector3(-6, -6, 0));
        remove.Add(new Vector3(-6, -7, 0));
        
        return remove;
    }
}
