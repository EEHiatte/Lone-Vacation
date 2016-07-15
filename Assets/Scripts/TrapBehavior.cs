using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TrapBehavior : MonoBehaviour
{
    public enum TrapType { GopherHole, TripTrap, RockTripTrap, ThornField, TreeTrap, Pitfall };
    public float trapDelay = 2;
    public float slowAmount = 2.0f;
    public float slowTime = 2.0f;
    public float stunTime = 2.0f;
    public int stickRequirement = 0;
    public int thornRequirement = 0;
    public int rockRequirement = 0;
    public int ropeRequirement = 0;
    public int leafRequirement = 0;

    public TrapType trapType = TrapType.GopherHole;

    public bool upgraded = false;

    public GameObject player;
    public List<GameObject> traps;
    int usecountGopherHole;
    int usecountTripTrap;
    int usecountRockTripTrap;
    int usecountThornField;
    int usecountTreeTrap;
    int usecountPitfall;
    //AudioSource thornFieldSFX;

    public GameObject currTree;

    //GameObject target;
    // Use this for initialization
    void Start()
    {
        if (gameObject.GetComponent<SphereCollider>() != null)
        {
            if (trapType == 0)
                gameObject.GetComponent<SphereCollider>().radius = 4.0f;
            else
                gameObject.GetComponent<SphereCollider>().radius = 1.0f;

            //thornFieldSFX = GetComponents<AudioSource>()[0];

            player = GameObject.FindGameObjectWithTag("Player");
        }

        usecountGopherHole = 0;
        usecountTripTrap = 0;
        usecountRockTripTrap = 0;
        usecountThornField = 0;
        usecountTreeTrap = 0;
        usecountPitfall = 0;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    ///// keeps track of the amount of times a trap has been "Used" (when an enemy steps on it) this is used to determine, if the trap has recieved it's second upgrade if it ahs been stepped on once or twice.
    //    //usecountGopherHole = 0;
    //    //usecountTripTrap = 0;
    //    //usecountRockTripTrap = 0;
    //    //usecountThornField = 0;
    //    //usecountTreeTrap = 0;
    //    //usecountPitfall = 0;
    //
    //    //InventoryManager inven = player.gameObject.GetComponent<InventoryManager>();
    //    //
    //    //for (int i = 0; i < traps.Capacity; i++)
    //    //    traps[i].GetComponentInChildren<Image>().color = new Color(0.25f, 0.25f, 0.25f);
    //    //
    //    //if (inven.numTwigs == 2 && inven.numLeaves == 1)
    //    //    traps[(int)TrapType.GopherHole].GetComponentInChildren<Image>().color = new Color(1, 1, 1);
    //    //
    //    //if (inven.numTwigs == 2 && inven.numLeaves == 2 && inven.numRope == 1)
    //    //    traps[(int)TrapType.TripTrap].GetComponentInChildren<Image>().color = new Color(1, 1, 1);
    //    //
    //    //if (inven.numTwigs == 2 && inven.numLeaves == 1 && inven.numRope == 2 && inven.numRocks == 1)
    //    //    traps[(int)TrapType.RockTripTrap].GetComponentInChildren<Image>().color = new Color(1, 1, 1);
    //    //
    //    //if (inven.numRocks == 1 && inven.numThorns == 2)
    //    //    traps[(int)TrapType.ThornField].GetComponentInChildren<Image>().color = new Color(1, 1, 1);
    //    //
    //    //if (inven.numTwigs == 2 && inven.numLeaves == 1 && inven.numRope == 2)
    //    //    traps[(int)TrapType.TreeTrap].GetComponentInChildren<Image>().color = new Color(1, 1, 1);
    //    //
    //    //if (inven.numTwigs == 3 && inven.numLeaves == 2 && inven.numThorns == 2)
    //    //    traps[(int)TrapType.Pitfall].GetComponentInChildren<Image>().color = new Color(1, 1, 1);
    //}

    public void InitiateItemRequirements(TrapType currTrapType)
    {
        trapType = currTrapType;
        switch (currTrapType)
        {
            case TrapType.ThornField:
                {
                    thornRequirement = 2;
                    rockRequirement = 1;
                    break;
                }
            case TrapType.GopherHole:
                {
                    stickRequirement = 2;
                    leafRequirement = 1;
                    break;
                }
            case TrapType.TreeTrap:
                {
                    ropeRequirement = 2;
                    stickRequirement = 2;
                    leafRequirement = 1;
                    break;
                }
            case TrapType.TripTrap:
                {
                    ropeRequirement = 1;
                    stickRequirement = 2;
                    leafRequirement = 2; break;
                }
            case TrapType.Pitfall:
                {
                    stickRequirement = 3;
                    leafRequirement = 2;
                    thornRequirement = 2;
                    break;
                }
            case TrapType.RockTripTrap:
                {
                    ropeRequirement = 2;
                    stickRequirement = 2;
                    leafRequirement = 2;
                    rockRequirement = 1;
                    break;
                }
        }
    }

    public void StunEffect(float stunTime, Collider enemy)
    {
        enemy.gameObject.GetComponent<AIBehavior>().m_fMovement = 0.0f;
        enemy.gameObject.GetComponent<AIBehavior>().m_fStunTimer = stunTime;
    }

    public void SlowEffect(float slowEffect, Collider enemy)
    {
        if (slowEffect == 0.0f)
        {
            enemy.gameObject.GetComponent<AIBehavior>().m_fMovement = slowAmount;
            enemy.gameObject.GetComponent<AIBehavior>().m_fSlowTimer = slowTime;
        }
        else
        {
            enemy.gameObject.GetComponent<AIBehavior>().m_fMovement = slowAmount;
            enemy.gameObject.GetComponent<AIBehavior>().m_fSlowTimer = slowTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //target = other.gameObject;
        if (other.gameObject.tag == "Cannibal" || other.gameObject.tag == "Animal")
        {
            if (other.GetComponent<AIBehavior>().m_aitType == AIBehavior.AIType.HeavyDuty || other.GetComponent<AIBehavior>().m_aitType == AIBehavior.AIType.HoneyBadger)
            {
                if (other.GetComponent<AIBehavior>().m_aitType == AIBehavior.AIType.HoneyBadger)
                {
                    other.GetComponent<AIBehavior>().PlayOwSound();
                }
                GameObject.Destroy(gameObject);
                return;
            }
            switch (trapType)
            {
                case TrapType.GopherHole:
                    {
                        other.gameObject.GetComponent<AIBehavior>().PlayOwSound();
                        SlowEffect(slowAmount, other);
                        GameObject.Destroy(gameObject);
                        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCreateTrap>().trapGopherHoleCount >= 20)
                        {
                            if (usecountGopherHole == 1)
                                GameObject.Destroy(gameObject);
                            else
                                usecountGopherHole += 1;
                        }
                        else
                            GameObject.Destroy(gameObject);
                        break;
                    }
                case TrapType.ThornField:
                    {
                        other.gameObject.GetComponent<AIBehavior>().PlayOwSound();
                        break;
                    }
                case TrapType.TreeTrap:
                    {
                        other.gameObject.GetComponent<AIBehavior>().PlayOwSound();
                        StunEffect(stunTime, other);
                        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCreateTrap>().trapTreeTrapCount >= 20)
                        {
                            if (usecountTreeTrap == 1)
                            {
                                currTree.GetComponent<HidingObjectBehavior>().hasTrap = false;
                                GameObject.Destroy(gameObject);
                            }
                            else
                                usecountTreeTrap += 1;
                        }
                        else
                        {
                            currTree.GetComponent<HidingObjectBehavior>().hasTrap = false;
                            GameObject.Destroy(gameObject);
                        }
                        break;
                    }
                case TrapType.TripTrap:
                    {
                        other.gameObject.GetComponent<AIBehavior>().PlayOwSound();
                        StunEffect(stunTime, other);
                        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCreateTrap>().trapTripTrapCount >= 20)
                        {
                            if (usecountTripTrap == 1)
                                GameObject.Destroy(gameObject);
                            else
                                usecountTripTrap += 1;
                        }
                        else
                            GameObject.Destroy(gameObject); break;
                    }
                case TrapType.Pitfall:
                    {
                        other.gameObject.GetComponent<AIBehavior>().PlayOwSound();
                        StunEffect(stunTime, other);
                        GameObject.Destroy(other.gameObject, 1.0f);
                        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCreateTrap>().trapPitfallCount >= 20)
                        {
                            if (usecountPitfall == 1)
                                GameObject.Destroy(gameObject);
                            else
                                usecountPitfall += 1;
                        }
                        else
                            GameObject.Destroy(gameObject);
                        break;
                    }
                case TrapType.RockTripTrap:
                    {
                        other.gameObject.GetComponent<AIBehavior>().PlayOwSound();
                        SlowEffect(slowAmount, other);
                        GameObject.Destroy(other.gameObject, 1.0f);
                        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCreateTrap>().trapRockTripTrapCount >= 20)
                        {
                            if (usecountRockTripTrap == 1)
                                GameObject.Destroy(gameObject);
                            else
                                usecountRockTripTrap += 1;
                        }
                        else
                            GameObject.Destroy(gameObject); break;
                    }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Cannibal" || other.gameObject.tag == "Animal")
        {
            if (trapType == TrapType.ThornField)
            {
                SlowEffect(0.0f, other);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cannibal" || other.gameObject.tag == "Animal")
        {
            if (trapType == TrapType.ThornField)
            {
                other.gameObject.GetComponent<AIBehavior>().m_fMovement = 4.0f;
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCreateTrap>().trapThornFieldCount >= 20)
                {
                    if (usecountThornField == 1)
                        GameObject.Destroy(gameObject);
                    else
                        usecountThornField += 1;
                }
                else
                    GameObject.Destroy(gameObject);
            }
        }
    }

    public void Upgrade()
    {

    }
}
