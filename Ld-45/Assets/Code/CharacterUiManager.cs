using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CharacterUiManager : MonoBehaviour {

    private List<CharacterClickerUi> uiScripts = new List<CharacterClickerUi>();
    public List<GameObject> uiObjects = new List<GameObject>();

    private void Awake() {
        for (int i = 0; i < uiObjects.Count; i++) {
            CharacterClickerUi script = uiObjects[i].GetComponent<CharacterClickerUi>();
            uiScripts.Add(script);
        }
    }

    public void characterSelected(int id, bool selected) {
        for (int i = 0; i < uiScripts.Count; i++) {
            if (uiScripts[i].id == id) {
                uiScripts[i].setSelected(selected);
                return;
            }
        }
    }

    public void unselectAll() {
        for (int i = 0; i < uiScripts.Count; i++) {
            uiScripts[i].setSelected(false);
        }
    }

    public void newCharacterArrived(int id) {
        uiObjects[id - 1].SetActive(true);
    }

    public void setHungerIcon(int id, bool value) {
        uiObjects[id - 1].transform.GetChild(1).gameObject.SetActive(value);
    }

    public void setSleepIcon(int id, bool value) {
        uiObjects[id - 1].transform.GetChild(2).gameObject.SetActive(value);
    }

    public void died(int id) {
        characterSelected(id, false);
        uiObjects[id - 1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "DIED";
        setSleepIcon(id, false);
        setHungerIcon(id, false);
    }
}
 
