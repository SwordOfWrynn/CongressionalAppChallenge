﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile_Scripts : MonoBehaviour {

    public GameObject townHallPrefab;

    public GameObject buildMenu;
    public bool buildMenuActive;

    public GameObject structureTypeSelectMenu;
    public GameObject productionStructuresMenu;
    public GameObject villageStructureMenu;
    public GameObject attackStructureMenu;
    public GameObject defenseStructureMenu;

    public GameObject GameManager;
    public Sprite EmptyTileIndicator;
    public GameObject childStructure;
    public Vector2 originalLocation;
    public bool spaceOccupied;

    // 1 = 1x1 tile occupation ; 2 = 2x2 tile occupation ; 
    public int sizeID;

    // Building IDs: 0 - Empty, 1 - Town Hall, 2 - Quarry, 3 - Sawmill, 4 - Mine, 5 - Forge, 
    public int buildingID;

    void Start()
    {
        originalLocation = gameObject.transform.position;
    }

    

    void Update()
    {
        ShowTilePlacement();
    }

    // This is for PC only, will have to create a different function for mobile
    void OnMouseUpAsButton()
    {
        MenuDisplayFunction();

        GameManager.GetComponent<GameManager>().selectedTile = gameObject;

        if (GameManager.GetComponent<GameManager>().cancelTileInteraction)
        {
            Debug.Log("I've canceled tile interaction. You're welcome.");
            GameManager.GetComponent<GameManager>().cancelTileInteraction = false;
            return;
        }
        
        if (spaceOccupied == true)
        {
            return;
        }

        if (GameManager.GetComponent<GameManager>().editMode == false)
        {
            Debug.Log("editMode isn't active!");
            return;
        }

        switch (buildingID)
        {   

            case 0:
                Debug.Log("This is an empty tile");
                break;

            case 1:
                Debug.Log("This is an occupied tile; buildingType1");
                break;

            case 2:
                Debug.Log("This is an occupied tile; buildingType2");
                break;

            case 3:
                Debug.Log("This is an occupied tile; buildingType3");
                break;

            case 4:
                Debug.Log("This is an occupied tile; buildingType4");
                break;

            case 5:
                Debug.Log("This is an occupied tile; buildingType5");
                break;
        }
    }

    public void SpawnBuilding(GameObject structureType, int stoneConsumed, int woodConsumed, int steelConsumed, int setID)
    {
        Debug.Log("I PUSHED ANOTHER BUTTON");

        if (stoneConsumed < GameManager.GetComponent<GameManager>().stoneAcquired && woodConsumed < GameManager.GetComponent<GameManager>().woodAcquired &&
                steelConsumed < GameManager.GetComponent<GameManager>().steelAcquired)
        {
            GameManager.GetComponent<GameManager>().stoneAcquired -= stoneConsumed;
            GameManager.GetComponent<GameManager>().woodAcquired -= woodConsumed;
            GameManager.GetComponent<GameManager>().steelAcquired -= steelConsumed;

            Instantiate(structureType, GetComponentInParent<Transform>());

            buildingID = setID;

            Destroy(GameObject.Find("temporaryUI"));
        }

        else
        {

            return;
        }
    }

    public void SpawnTownHall()
    {

        Instantiate(townHallPrefab, GetComponent<Transform>());
        townHallPrefab.transform.position = new Vector3 (0.5f, 0.5f, 0);
        spaceOccupied = true;

        GameObject.Find("Tile(" + (originalLocation.x + 1) + ", " + originalLocation.y + ")").GetComponent<Tile_Scripts>().spaceOccupied = true;
        GameObject.Find("Tile(" + originalLocation.x + ", " + (originalLocation.y + 1) + ")").GetComponent<Tile_Scripts>().spaceOccupied = true;
        GameObject.Find("Tile(" + (originalLocation.x + 1) + ", " + (originalLocation.y + 1) + ")").GetComponent<Tile_Scripts>().spaceOccupied = true;
    }

    public void MenuDisplayFunction()
    {
        if (GameManager.GetComponent<GameManager>().editMode == false)
        {
            buildMenu.SetActive(false);
            return;
        }

        if (!buildMenuActive)
        {
            buildMenu.SetActive(true);
            buildMenuActive = true;
            
        }

        else if (buildMenuActive)
        {
            buildMenu.SetActive(false);
            buildMenuActive = false;
        }
    }

    public void ShowTilePlacement()
    {
        if (GameManager.GetComponent<GameManager>().editMode == true && spaceOccupied == false)
        {
            GetComponent<SpriteRenderer>().sprite = EmptyTileIndicator;
        }
        else if (GameManager.GetComponent<GameManager>().editMode == false || spaceOccupied)
        {
            GetComponent<SpriteRenderer>().sprite = null;
        }

        else
        {
            Debug.Log("Something's amiss here...");
        }
    }
}
