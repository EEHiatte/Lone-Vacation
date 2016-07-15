using UnityEngine;
using System.Collections;

public class ItemSpawner : MonoBehaviour
{
    public int stickAmount;
    public int rockAmount;
    public int leafAmount;
    public int ropeAmount;
    public int thornAmount;
    
    int toSpawnInBeginning = 350;
    int toSpawnInGame = 100;
    int minItemsInGame = 40;

    public GameObject terrain;
    public GameObject pickupPrefab;

    float correctYPosition = 6.456f;

    BoxCollider coveBounds;

    // Use this for initialization
    void Start()
    {
        stickAmount = 0;
        rockAmount = 0;
        leafAmount = 0;
        ropeAmount = 0;
        thornAmount = 0;

        Random.seed = (int)System.DateTime.Now.Ticks;

        coveBounds = GameObject.FindGameObjectWithTag("CoveBounds").GetComponent<BoxCollider>();

        //Stick
        SpawnItem(0, toSpawnInBeginning);
        //Rock
        SpawnItem(1, toSpawnInBeginning);
        //Leaf
        SpawnItem(2, toSpawnInBeginning);
        //Rope
        SpawnItem(3, toSpawnInBeginning);
        //Thorns
        SpawnItem(4, toSpawnInBeginning);
    }

    public void SpawnItem(int type, int amount = 0)
    {
        if (amount != 0)
        {
            for (int i = 0; i < amount; i++)
            {
                Vector3 destination = new Vector3(Random.Range(139.8f, 1799.0f), 25.0f, Random.Range(93.6f, 1903.0f));
                RaycastHit rayHitPoint;

                GameObject pickup = null;

                bool moveOn = false;
                while (!moveOn)
                {
                    Physics.Linecast(destination, new Vector3(destination.x, destination.y - 10000, destination.z), out rayHitPoint);

                    if (rayHitPoint.collider != null)
                    {
                        if (rayHitPoint.collider.tag == "Terrain" && !coveBounds.bounds.Contains(rayHitPoint.point) && /*!fireCollider.bounds.Contains(rayHitPoint.point) &&*/ rayHitPoint.point.y >= 6.0f && rayHitPoint.point.y <=6.01f)
                        {
                            destination = new Vector3(destination.x, correctYPosition, destination.z);
                            pickup = Instantiate(pickupPrefab, destination, Quaternion.Euler(45f, 0, 0)) as GameObject;
                            moveOn = true;
                            if(!pickup.GetComponentInChildren<SpriteRenderer>().isVisible)
                                moveOn = true;
                        }
                        else
                            destination = new Vector3(Random.Range(139.8f, 1799.0f), 25.0f, Random.Range(93.6f, 1903.0f));
                    }
                    else
                        destination = new Vector3(Random.Range(139.8f, 1799.0f), 25.0f, Random.Range(93.6f, 1903.0f));
                }

                pickup.GetComponent<CollectableBehavior>().collectibleType = type;
                pickup.transform.position = destination;
 
                switch (type)
                {
                    case 0:
                        stickAmount++;
                        break;
                    case 1:
                        rockAmount++;
                        break;
                    case 2:
                        leafAmount++;
                        break;
                    case 3:
                        ropeAmount++;
                        break;
                    case 4:
                        thornAmount++;
                        break;
                }
            }
        }
    }

    public void CheckToSpawn()
    {
        if(stickAmount < minItemsInGame)
        {
            SpawnItem(0, toSpawnInGame);            
        }
        if (rockAmount < minItemsInGame)
        {
            SpawnItem(1, toSpawnInGame);
        }
        if (leafAmount < minItemsInGame)
        {
            SpawnItem(2, toSpawnInGame);
        }
        if (ropeAmount < minItemsInGame)
        {
            SpawnItem(3, toSpawnInGame);
        }
        if (thornAmount < minItemsInGame)
        {
            SpawnItem(4, toSpawnInGame);
        }
    }
}
