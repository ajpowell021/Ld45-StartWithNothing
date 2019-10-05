using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour {
    
    // Public State

    public GameObject groundClick;
    public GameObject selectorBox;

    [Header("Buildings")] 
    public GameObject farm;
    public GameObject mine;
    public GameObject lumberYard;
    public GameObject house;
    public GameObject buildingSite;
    
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

}
