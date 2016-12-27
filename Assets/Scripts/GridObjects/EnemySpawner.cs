﻿using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : KillableGridObject
{
    public GameObject enemy;
    public TileMap tileMap;
    public GameObject targetObj;
    public UnityEvent deathEvent;
    private Vector3 spawnPosition;

    //Keep track of spawns

    public float spawnDelay;
    public int maxSpawns;
    public bool spawnsOnce = false;
    private int currSpawns = 0;
    private List<GameObject> list = new List<GameObject>();
    private Animator animator;
    private Quaternion spawnRotation = Quaternion.identity;
    private PlayerGridObject player;

    System.Random randGen = new System.Random();
    private int randInt;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGridObject>();
        animator = GetComponent<Animator>();
        if (!spawnsOnce)
        {
            StartCoroutine(spawnRandomDir());
        }
        else
        {
            StartCoroutine(spawnsAtOnce());
        }
        
    }

    // Update is called once per frame
    void Update() {


        if (health <= 0)
        {
            StartCoroutine(waitForDeathAnim());
        }
        
        foreach(GameObject obj in list)
        {
            if (obj == null)
            {
                currSpawns--;
                list.Remove(obj);
                break; //prevents error from modifying list during foreach loop
            }
        }
    

    }
    IEnumerator spawnsAtOnce()
    {
        for (int i = 0; i < maxSpawns + 1; i++)
        {
            randInt = randGen.Next(0, 4);

            if (randInt == 1)
            {
                spawnPosition = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1, 0.0f);
                GameObject enemyObj = (GameObject)Instantiate(enemy, spawnPosition, spawnRotation);
                list.Add(enemyObj);
                //I needed a reference to every specific enemyObj to keep track of them, so I couldn't use 1 GameObject enemyObj for all spawns.
                // TODO: change this later to fit other AI
                // Give AI info about the tile map and the target object
                enemyObj.GetComponent<SmartChasingMonster>().tileMap = tileMap;
                enemyObj.GetComponent<SmartChasingMonster>().targetObject = targetObj;
            }
            else if (randInt == 2)
            {
                spawnPosition = new Vector3(this.gameObject.transform.position.x + 1, this.gameObject.transform.position.y, 0.0f);
                GameObject enemyObj = (GameObject)Instantiate(enemy, spawnPosition, spawnRotation);
                list.Add(enemyObj);
                // TODO: change this later to fit other AI
                // Give AI info about the tile map and the target object
                enemyObj.GetComponent<SmartChasingMonster>().tileMap = tileMap;
                enemyObj.GetComponent<SmartChasingMonster>().targetObject = targetObj;
            }
            else if (randInt == 3)
            {
                spawnPosition = new Vector3(this.gameObject.transform.position.x - 1, this.gameObject.transform.position.y, 0.0f);
                GameObject enemyObj = (GameObject)Instantiate(enemy, spawnPosition, spawnRotation);
                list.Add(enemyObj);
                // TODO: change this later to fit other AI
                // Give AI info about the tile map and the target object
                enemyObj.GetComponent<SmartChasingMonster>().tileMap = tileMap;
                enemyObj.GetComponent<SmartChasingMonster>().targetObject = targetObj;
            }
            else
            {
                spawnPosition = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 1, 0.0f);
                GameObject enemyObj = (GameObject)Instantiate(enemy, spawnPosition, spawnRotation);
                list.Add(enemyObj);
                // TODO: change this later to fit other AI
                // Give AI info about the tile map and the target object
                enemyObj.GetComponent<SmartChasingMonster>().tileMap = tileMap;
                enemyObj.GetComponent<SmartChasingMonster>().targetObject = targetObj;
            }
        }
        yield return 0;
    }
    IEnumerator spawnRandomDir()
    {
        while (health > 0)
        {
            if (currSpawns < maxSpawns)
            {
                randInt = randGen.Next(0, 4);

                if (randInt == 1)
                {
                    spawnPosition = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1, 0.0f);
                    GameObject enemyObj = (GameObject)Instantiate(enemy, spawnPosition, spawnRotation);
                    list.Add(enemyObj);
                    //I needed a reference to every specific enemyObj to keep track of them, so I couldn't use 1 GameObject enemyObj for all spawns.
                    // TODO: change this later to fit other AI
                    // Give AI info about the tile map and the target object
                    enemyObj.GetComponent<SmartChasingMonster>().tileMap = tileMap;
                    enemyObj.GetComponent<SmartChasingMonster>().targetObject = targetObj;
                }
                else if (randInt == 2)
                {
                    spawnPosition = new Vector3(this.gameObject.transform.position.x + 1, this.gameObject.transform.position.y, 0.0f);
                    GameObject enemyObj = (GameObject)Instantiate(enemy, spawnPosition, spawnRotation);
                    list.Add(enemyObj);
                    // TODO: change this later to fit other AI
                    // Give AI info about the tile map and the target object
                    enemyObj.GetComponent<SmartChasingMonster>().tileMap = tileMap;
                    enemyObj.GetComponent<SmartChasingMonster>().targetObject = targetObj;
                }
                else if (randInt == 3)
                {
                    spawnPosition = new Vector3(this.gameObject.transform.position.x - 1, this.gameObject.transform.position.y, 0.0f);
                    GameObject enemyObj = (GameObject)Instantiate(enemy, spawnPosition, spawnRotation);
                    list.Add(enemyObj);
                    // TODO: change this later to fit other AI
                    // Give AI info about the tile map and the target object
                    enemyObj.GetComponent<SmartChasingMonster>().tileMap = tileMap;
                    enemyObj.GetComponent<SmartChasingMonster>().targetObject = targetObj;
                }
                else
                {
                    spawnPosition = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 1, 0.0f);
                    GameObject enemyObj = (GameObject)Instantiate(enemy, spawnPosition, spawnRotation);
                    list.Add(enemyObj);
                    // TODO: change this later to fit other AI
                    // Give AI info about the tile map and the target object
                    enemyObj.GetComponent<SmartChasingMonster>().tileMap = tileMap;
                    enemyObj.GetComponent<SmartChasingMonster>().targetObject = targetObj;
                }

                currSpawns++;
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && player.isAttacking)
        {
            TakeDamage(player.damage);
        }
    }
    public override bool TakeDamage(int dmg)
    {
        gameObject.GetComponent<Animation>().Play("Damaged");
        return base.TakeDamage(dmg);
    }

    protected override void Die() {
        base.Die();
        deathEvent.Invoke();
    }
    IEnumerator waitForDeathAnim()
    {
        animator.SetBool("dead", true);
        yield return new WaitForSeconds(1.2f);
        this.gameObject.SetActive(false);
    }
    public int numSpawns() {
    	return currSpawns;
    }
}