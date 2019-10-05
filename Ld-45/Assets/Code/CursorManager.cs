using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour {

    // Public State

    public Color goodColor;
    public Color badColor;
    
    // Private State

    private bool onCursor;
    public GameObject cursorObject;
    private SpriteRenderer cursorObjectSprite;
    private bool canBuild;
    private BuildingType typeOnCursor;
    
    // Classes

    private PrefabManager prefabManager;
    private InputManager inputManager;
    
    // Init

    private void Awake() {
        cursorObjectSprite = cursorObject.GetComponent<SpriteRenderer>();
    }

    private void Start() {
        prefabManager = ClassManager.instance.prefabManager;
        inputManager = ClassManager.instance.inputManager;
    }
    
    // Update

    private void Update() {
        if (onCursor) {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            Vector3 gridPosition = setToGrid(position);
            cursorObject.transform.position = gridPosition;
        }
    }

    // Public Functions

    public void setBuildCursor(BuildingType type) {
        typeOnCursor = type;
        cursorObject.SetActive(true);
        cursorObjectSprite.sprite = prefabManager.getPrefabFromBuildingType(type).GetComponent<SpriteRenderer>().sprite;

        adjustCursorColor(true);
        Cursor.visible = false;
        onCursor = true;
        canBuild = true;
    }

    public void adjustCursorColor(bool good) {
        if (good) {
            cursorObjectSprite.color = goodColor;
            canBuild = true;
        }
        else {
            cursorObjectSprite.color = badColor;
            canBuild = false;
        }
    }

    public void buildTheBuildingOnCursor() {
        if (canBuild) {
            // Create building at point that cursor object is.
            //Instantiate(prefabManager.getPrefabFromBuildingType(typeOnCursor), cursorObject.transform.position, Quaternion.identity);
            GameObject buildingSite = Instantiate(prefabManager.getPrefabFromBuildingType(BuildingType.BuildingSite), cursorObject.transform.position, Quaternion.identity);
            BuildingSiteController controller = buildingSite.GetComponent<BuildingSiteController>();
            controller.setBuildingType(typeOnCursor);
            // Hide cursor object.
            cursorObject.SetActive(false);
            // Reset vars
            canBuild = false;
            onCursor = false;
            inputManager.setInputMode(InputMode.PeopleControl);
            // Show mouse
            Cursor.visible = true;
        }
    }

    public void cancelBuild() {
        if (onCursor) {
            canBuild = false;
            cursorObject.SetActive(false);
            Cursor.visible = true;
            onCursor = false;
        }
    }
    
    // Private Functions

    private Vector3 setToGrid(Vector3 position) {
        float xPos = Mathf.Round(position.x);
        float yPos = Mathf.Round(position.y);
        
        return new Vector3(xPos, yPos, 0);
    }
}
