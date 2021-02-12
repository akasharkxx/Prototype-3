using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;

    private float startDelay = 2.0f;
    private float repeatDelay = 2.0f;

    private Vector3 spawnPosition = new Vector3(25.0f, 0.0f, 0.0f);

    private PlayerController playerControllerScript;

    private void Start()
    {
        playerControllerScript = FindObjectOfType<PlayerController>();
        InvokeRepeating("SpawnObject", startDelay, repeatDelay);
    }

    private void SpawnObject()
    {
        if(playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefab, spawnPosition, obstaclePrefab.transform.rotation);
        }
    }
}
