using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {
 
    // Public State

    public int wood;
    public int stone;
    public int cotton;
    public int food;

    public int mineCost;
    public int lumberYardCost;
    public int farmCost;
    public int houseCost;
    
    // Classes

    private HudController hudController;
    
    // Init

    private void Start() {
        hudController = ClassManager.instance.hudController;
        StartCoroutine(LateStart());
    }

    private IEnumerator LateStart() {
        yield return new WaitForFixedUpdate();
        hudController.updateResourceUi();
    }

    // Public Functions

    public void adjustResource(ResourceType type, int amount) {
        switch (type) {
            case ResourceType.Wood:
                wood += amount;
                break;
            case ResourceType.Stone:
                stone += amount;
                break;
            case ResourceType.Food:
                food += amount;
                break;
            case ResourceType.Cotton:
                cotton += amount;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
        
        hudController.updateResourceUi();
    }

    public bool canAffordMine() {
        if (wood > mineCost) {
            adjustResource(ResourceType.Wood, -mineCost);
            return true;
        }

        return false;
    }

    public bool canAffordLumberYard() {
        if (stone > lumberYardCost) {
            adjustResource(ResourceType.Stone, -lumberYardCost);
            return true;
        }

        return false;
    }
}
