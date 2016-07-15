using UnityEngine;
using System.Collections;

public class PowerUpSpawner : MonoBehaviour
{
    GameObject[] spawnLocations;
    public float spawnRadius = 1.0f;
    float timePassed = 0.0f;
    float maxTimePassed = 30.0f;

    bool bootSpawned = false;
    bool gloveSpawned = false;
    bool lampSpawned = false;
    bool backpackSpawned = false;
    bool canteenSpawned = false;

    public GameObject bootsObject;
    public GameObject glovesObject;
    public GameObject lampObject;
    public GameObject backpackObject;
    public GameObject canteenObject;

    public LayerMask layerMask; // SHOULD ONLY BE TERRAIN IN FIRST TURN IN

    // Use this for initialization
    void Start()
    {
        spawnLocations = GameObject.FindGameObjectsWithTag("BeachSpawnPoint");
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > maxTimePassed/*10/*Random.Range(60,360)*/)
        {
            timePassed = 0;

            int spawnPoint = Random.Range(0, spawnLocations.Length - 1);

            int spawnType = Random.Range(0, 4);

            // Ugly code.
            switch (spawnType)
            {
                case 0:
                    {
                        if (!bootSpawned)
                        {
                            bootSpawned = true;
                            SpawnPowerUp(bootsObject, spawnPoint);
                        }
                    }
                    break;
                case 1:
                    {
                        if (!gloveSpawned)
                        {
                            gloveSpawned = true;
                            SpawnPowerUp(glovesObject, spawnPoint);                            
                        }
                    }
                    break;
                case 2:
                    {
                        if (!lampSpawned)
                        {
                            lampSpawned = true;
                            SpawnPowerUp(lampObject, spawnPoint);                            
                        }
                    }
                    break;
                case 3:
                    {
                        if (!backpackSpawned)
                        {
                            backpackSpawned = true;
                            SpawnPowerUp(backpackObject, spawnPoint);                           
                        }
                    }
                    break;
                case 4:
                    {
                        if (!canteenSpawned)
                        {
                            canteenSpawned = true;
                            SpawnPowerUp(canteenObject, spawnPoint);                           
                        }
                    }
                    break;
                default:
                    Debug.LogError("PowerUp spawning is broken. Please fix.");
                    break;
            }
        }
    }

    void SpawnPowerUp(GameObject type, int location)
    {
        Vector3 locationPos = spawnLocations[location].transform.position;


        Vector3 spawnDestination = locationPos - new Vector3(Random.insideUnitCircle.x, 0, Random.insideUnitCircle.y) * spawnRadius;

        GameObject powerUpObject = GameObject.Instantiate(type, spawnDestination, Quaternion.Euler(45f, 0, 0)) as GameObject;

        RaycastHit hit;

        if (Physics.Raycast(powerUpObject.transform.position, -Vector3.up, out hit, 100f, layerMask))
        {
            powerUpObject.transform.position = hit.point + Vector3.up * 0.5f;
        }
    }
}
