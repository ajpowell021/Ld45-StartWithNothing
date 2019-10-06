using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterUiManager : MonoBehaviour {

    private List<CharacterClickerUi> uiScripts = new List<CharacterClickerUi>();

    private void Awake() {
        List<GameObject> uiObjects = GameObject.FindGameObjectsWithTag("CharacterUi").ToList();
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
}
