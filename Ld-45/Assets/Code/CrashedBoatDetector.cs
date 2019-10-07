using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashedBoatDetector : MonoBehaviour {

    private PopupController popupController;
    private ResourceManager resourceManager;
    private DataHolder dataHolder;
    public GameObject winPanel;
    public GameObject builtBoat;
    public GameObject crashedBoat;
    private WinManager winManager;
    
    private void Start() {
        popupController = ClassManager.instance.popupController;
        resourceManager = ClassManager.instance.resourceManager;
        dataHolder = ClassManager.instance.dataHolder;
        winManager = ClassManager.instance.winManager;
    }

    public void OnMouseOver() {
        string firstString = "Wood: " + resourceManager.wood + "/" + dataHolder.woodToWin;
        string secondString = "Stone: " + resourceManager.stone + "/" + dataHolder.stoneToWin;
        string thirdString = "Cotton: " + resourceManager.cotton + "/" + dataHolder.cottonToWin;
        string fourthString = "Food: " + resourceManager.food + "/" + dataHolder.foodToWin;
        string fifthString = "";
        if (canWin()) {
            fifthString = "You've done it! Click here to sail away!";    
        }
        else {
            fifthString = "Collect all the resources to win.";    
        }
        if (dataHolder.foodToWin > 0) {
            popupController.showWin(firstString + "\n" + secondString + "\n" + thirdString + "\n" + fourthString + "\n" + "\n" + fifthString);
        }
        else {
            popupController.showWin(firstString + "\n" + secondString + "\n" + thirdString + "\n" + "\n" + fifthString);
        }
    }

    public void OnMouseExit() {
        popupController.hideWin();
    }

    public void OnMouseDown() {
        if (canWin()) {
            winPanel.SetActive(true);
            builtBoat.SetActive(true);
            crashedBoat.SetActive(false);
            popupController.hideWin(true);
            winManager.winStarted(builtBoat);
        }
    }

    private bool canWin() {
        return resourceManager.wood >= dataHolder.woodToWin
               && resourceManager.stone >= dataHolder.stoneToWin
               && resourceManager.cotton >= dataHolder.cottonToWin
               && resourceManager.food >= dataHolder.foodToWin;
    }
}
