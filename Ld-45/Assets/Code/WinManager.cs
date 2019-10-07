using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinManager : MonoBehaviour {

    private bool boatMoving;
    private GameObject boat;
    public GameObject canvas;
    public bool moveCam;
    
    // Public Functions

    public void winStarted(GameObject builtBoat) {
        boat = builtBoat;
        boatMoving = true;
        canvas.SetActive(false);
        moveCam = true;
    }

    public void Update() {
        if (boatMoving) {
            if (moveCam) {
                Camera.main.transform.Translate(3f * Time.deltaTime * Vector3.right);    
            }
            boat.transform.Translate(3f * Time.deltaTime * Vector3.right);
            if (boat.transform.position.x > 200f) {
                moveCam = false;
            }
        }
    }
}
