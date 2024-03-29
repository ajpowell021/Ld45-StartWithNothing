﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PrefabManager : MonoBehaviour {
    
    // Public State

    public GameObject villager;
    public GameObject boat;

    public GameObject groundClick;
    public GameObject selectorBox;

    [Header("Buildings")] 
    public GameObject farm;
    public GameObject mine;
    public GameObject lumberYard;
    public GameObject house;
    public GameObject buildingSite;

    public GameObject treeZero;
    public GameObject treeOne;
    public GameObject treeTwo;
    public GameObject treeThree;

    public GameObject rockOne;
    public GameObject rockTwo;
    public GameObject rockThree;
    public GameObject rockFour;

    public GameObject fishingRadial;

    // Public Functions

    public GameObject getPrefabFromBuildingType(BuildingType type) {
        switch (type) {
            case BuildingType.LumberYard:
                return lumberYard;
            case BuildingType.Farm:
                return farm;
            case BuildingType.Mine:
                return mine;
            case BuildingType.House:
                return house;
            case BuildingType.BuildingSite:
                return buildingSite;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    public Sprite getRandomTreeSprite() {
        int roll = Random.Range(0, 4);
        switch (roll) {
            case 0:
                return treeZero.GetComponent<SpriteRenderer>().sprite;
            case 1:
                return treeOne.GetComponent<SpriteRenderer>().sprite;
            case 2:
                return treeTwo.GetComponent<SpriteRenderer>().sprite;
            case 3:
                return treeThree.GetComponent<SpriteRenderer>().sprite;
        }
        
        Debug.LogError("Random Tree out of range.");
        return treeZero.GetComponent<SpriteRenderer>().sprite;
    }

    public Sprite getRandomRockSprite() {
        int roll = Random.Range(0, 4);
        switch (roll) {
            case 0:
                return rockOne.GetComponent<SpriteRenderer>().sprite;
            case 1:
                return rockTwo.GetComponent<SpriteRenderer>().sprite;
            case 2:
                return rockThree.GetComponent<SpriteRenderer>().sprite;
            case 3:
                return rockFour.GetComponent<SpriteRenderer>().sprite;
        }
        
        Debug.LogError("Random Tree out of range.");
        return rockOne.GetComponent<SpriteRenderer>().sprite;
    }

}
