﻿using UnityEngine;
using UnityEngine.UI;

public class StructureHP : MonoBehaviour
{
    [SerializeField]
    private Image healthBar;
    //public GameObject deathEffect;


    [Header("Building Type")]
    [SerializeField]
    private bool isVillageStructure;
    [SerializeField]
    private bool isTowerStructure;
    [SerializeField]
    private bool isProductionStructure;
    [SerializeField]
    private bool isDefenseStructure;

    [Header("For Production Buildings")]
    [SerializeField]
    private bool multiTileBuilding = true;
    
    private float health;
    public GameObject parentTile;
    private float startHealth = 100;




    //Unity destroy takes time and could cause problems
    private bool isDestroyed = false;

    void Start()
    {
        parentTile = GameObject.Find("Tile(" + gameObject.transform.position.x + ", " + gameObject.transform.position.y + ")");

        if (isVillageStructure)
            startHealth = GameObject.Find("GameManager").GetComponent<GameManagerScript>().VillageStuctureHP;
        else if (isTowerStructure)
            startHealth = GameObject.Find("GameManager").GetComponent<GameManagerScript>().TowerStuctureHP;
        else if (isProductionStructure)
            startHealth = GameObject.Find("GameManager").GetComponent<GameManagerScript>().ProductionStuctureHP;
        else if (isDefenseStructure)
            startHealth = GameObject.Find("GameManager").GetComponent<GameManagerScript>().DefenseStructureHP;
        else
            Debug.LogError("StructureHP -- Start: " + gameObject + "has not had it's tower type set!");

        health = startHealth;
    }

    public void TakeDamage(float _amount)
    {
        health -= _amount;
        if(healthBar != null)
            healthBar.fillAmount = health / startHealth;

        if (health <= 0 && !isDestroyed)
            Die();
    }
    void Die()
    {
        isDestroyed = true;

        //if it is a production structure with multipule tiles
        if (isProductionStructure && multiTileBuilding)
        {
            for (int i = 0; i < GetComponent<BaseStructureScript>().parentTiles.Count; i++)
            {
                GetComponent<BaseStructureScript>().parentTiles[i].GetComponent<Tile_Scripts>().buildingID = 0;
                GetComponent<BaseStructureScript>().parentTiles[i].GetComponent<Tile_Scripts>().spaceOccupied = false;
            }
        }
        else
        {
            parentTile.GetComponent<Tile_Scripts>().buildingID = 0;
            parentTile.GetComponent<Tile_Scripts>().spaceOccupied = false;
        }

        if (isDefenseStructure)
            GameObject.Find("GameManager").GetComponent<GameManagerScript>().WallTiles.Remove(gameObject);

        //GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        //Destroy(effect, .5f);

        Debug.Log(gameObject + "has been destroyed");
        Destroy(gameObject);
    }
}