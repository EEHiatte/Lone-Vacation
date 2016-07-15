using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerCreateTrap : MonoBehaviour
{
    public GameObject tempTrap;
    GameObject buildingTrap;
    public GameObject Player;
    float delay = 0.0f;
    public bool building = false;
    Vector3 buildingLoc;
    public AudioSource buildingSound;
    float upgrademessage = 0;

    InventoryManager invManager;

    public GameObject gloves;
    bool hasGloves = false;
    public int trapThornFieldCount;
    public int trapGopherHoleCount;
    public int trapTreeTrapCount;
    public int trapTripTrapCount;
    public int trapPitfallCount;
    public int trapRockTripTrapCount;
    bool timerstart;

    public List<Object> builtTraps = new List<Object>();

    public Text TrapUpgrade;

    NavMeshAgent navMeshAgent;

    GameObject currTree;
    // Use this for initialization
    void Start()
    {
        invManager = GetComponent<InventoryManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        trapThornFieldCount = 0;
        trapGopherHoleCount = 0;
        trapTreeTrapCount = 0;
        trapTripTrapCount = 0;
        trapPitfallCount = 0;
        trapRockTripTrapCount = 0;
        if (TrapUpgrade)
            TrapUpgrade.GetComponent<ToggleActive>().SetInactiveState();
        timerstart = false;

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        /// checks to see if the isntant build cheat is on if it is, build time is 0.
        if (PlayerPrefs.GetInt("building") == 1)
        {
            delay = 0;
        }
        else
            delay -= Time.deltaTime;

        if (timerstart == true)
            upgrademessage += Time.deltaTime;
        if (upgrademessage >= 5)
        {
            TrapUpgrade.GetComponent<ToggleActive>().SetInactiveState();
            timerstart = false;
        }
        if (building && !buildingSound.isPlaying)
        {
            buildingSound.Play();
        }
        if (building)
        {
            if ((Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 || Input.GetMouseButton(0)) && GetComponent<PlayerController>().canMove)
            {
                navMeshAgent.enabled = true;
                building = false;
                delay = 0;
                if (buildingSound.isPlaying)
                    buildingSound.Stop();
                if (currTree != null)
                    currTree = null;
            }
        }
        if (building && delay <= 0)
        {
            switch (buildingTrap.GetComponent<TrapBehavior>().trapType)
            {
                case TrapBehavior.TrapType.ThornField:
                    {
                        trapThornFieldCount += 1;
                        /// checks to see if this trap has been built enough times to upgrade said trap and displays the desired text.
                        if (trapThornFieldCount == 10)
                        {
                            TrapUpgrade.text = "You've become proficient at building Thorn Fields and build them at double the speed!";
                            TrapUpgrade.GetComponent<ToggleActive>().SetActiveState();
                            upgrademessage = 0;
                            timerstart = true;
                        }
                        if (trapThornFieldCount == 20)
                        {
                            TrapUpgrade.text = "You've Mastered building Thorn Fields and get two uses from them!";
                            TrapUpgrade.GetComponent<ToggleActive>().SetActiveState();
                            upgrademessage = 0;
                            timerstart = true;
                        }
                    }
                    break;
                case TrapBehavior.TrapType.GopherHole:
                    {
                        trapGopherHoleCount += 1;
                        /// checks to see if this trap has been built enough times to upgrade said trap and displays the desired text.
                        if (trapGopherHoleCount == 10)
                        {
                            TrapUpgrade.text = "You've become proficient at building Gopher Holes and build them at double the speed!";
                            TrapUpgrade.GetComponent<ToggleActive>().SetActiveState();
                            upgrademessage = 0;
                            timerstart = true;

                        }
                        if (trapGopherHoleCount == 20)
                        {
                            TrapUpgrade.text = "You've Mastered building Ghopher Holes and get two uses from them!";
                            TrapUpgrade.GetComponent<ToggleActive>().SetActiveState();
                            upgrademessage = 0;
                            timerstart = true;
                        }
                    }
                    break;
                case TrapBehavior.TrapType.TreeTrap:
                    {
                        trapTreeTrapCount += 1;

                        if (currTree)
                            currTree.GetComponent<HidingObjectBehavior>().hasTrap = true;
                        /// checks to see if this trap has been built enough times to upgrade said trap and displays the desired text.
                        if (trapTreeTrapCount == 10)
                        {
                            TrapUpgrade.text = "You've become proficient at building Tree Traps and build them at double the speed!";
                            TrapUpgrade.GetComponent<ToggleActive>().SetActiveState();
                            upgrademessage = 0;
                            timerstart = true;

                        }
                        if (trapTreeTrapCount == 20)
                        {
                            TrapUpgrade.text = "You've Mastered building Tree Traps and get two uses from them!";
                            TrapUpgrade.GetComponent<ToggleActive>().SetActiveState();
                            upgrademessage = 0;
                            timerstart = true;
                        }
                    }
                    break;
                case TrapBehavior.TrapType.TripTrap:
                    {
                        trapTripTrapCount += 1;
                        /// checks to see if this trap has been built enough times to upgrade said trap and displays the desired text.
                        if (trapTripTrapCount == 10)
                        {
                            TrapUpgrade.text = "You've become proficient at building Trip Traps and build them at double the speed!";
                            TrapUpgrade.GetComponent<ToggleActive>().SetActiveState();
                            upgrademessage = 0;
                            timerstart = true;

                        }
                        if (trapTripTrapCount == 20)
                        {
                            TrapUpgrade.text = "You've Mastered building Trip Traps and get two uses from them!";
                            TrapUpgrade.GetComponent<ToggleActive>().SetActiveState();
                            upgrademessage = 0;
                            timerstart = true;
                        }
                    }
                    break;
                case TrapBehavior.TrapType.Pitfall:
                    {
                        trapPitfallCount += 1;
                        /// checks to see if this trap has been built enough times to upgrade said trap and displays the desired text.
                        if (trapPitfallCount == 10)
                        {
                            TrapUpgrade.text = "You've become proficient at building Foot Spikes and build them at double the speed!";
                            TrapUpgrade.GetComponent<ToggleActive>().SetActiveState();
                            upgrademessage = 0;
                            timerstart = true;

                        }
                        if (trapPitfallCount == 20)
                        {
                            TrapUpgrade.text = "You've Mastered building Foot Spikes and get two uses from them!";
                            TrapUpgrade.GetComponent<ToggleActive>().SetActiveState();
                            upgrademessage = 0;
                            timerstart = true;
                        }
                    }
                    break;
                case TrapBehavior.TrapType.RockTripTrap:
                    {
                        trapRockTripTrapCount += 1;
                        /// checks to see if this trap has been built enough times to upgrade said trap and displays the desired text.
                        if (trapRockTripTrapCount == 10)
                        {
                            TrapUpgrade.text = "You've become proficient at building Stone Trip Traps and build them at double the speed!";
                            TrapUpgrade.GetComponent<ToggleActive>().SetActiveState();
                            upgrademessage = 0;
                            timerstart = true;

                        }
                        if (trapRockTripTrapCount == 20)
                        {
                            TrapUpgrade.text = "You've Mastered building Stone Trip Traps and get two uses from them!";
                            TrapUpgrade.GetComponent<ToggleActive>().SetActiveState();
                            upgrademessage = 0;
                            timerstart = true;
                        }
                    }
                    break;
            }
            Player.GetComponent<InventoryManager>().PlaceTrap(buildingTrap.GetComponent<TrapBehavior>().trapType);
            building = false;
            navMeshAgent.enabled = true;

            GameObject trap = null;
            //if (buildingTrap.GetComponent<TrapBehavior>().trapType == TrapBehavior.TrapType.TreeTrap)
            //{
            //    trap = Instantiate(buildingTrap, new Vector3(buildingLoc.x, buildingLoc.y, currTree.transform.position.z), transform.rotation) as GameObject;
            //}
            //else
            trap = Instantiate(buildingTrap, buildingLoc, transform.rotation) as GameObject;


            SpriteRenderer[] sprites = trap.GetComponentsInChildren<SpriteRenderer>();
            for (int i = 0; i < 6; i++)
            {
                if (i == (int)trap.GetComponent<TrapBehavior>().trapType)
                    sprites[i].enabled = true;
                else
                    sprites[i].enabled = false;
            }

            if (buildingSound.isPlaying)
                buildingSound.Stop();
        }
    }

    public bool CreateTrap(int type)
    {
        bool canBuild = false;
        if (delay <= 0)
        {
            TrapBehavior.TrapType currTrapType = (TrapBehavior.TrapType)type;
            tempTrap.GetComponent<TrapBehavior>().InitiateItemRequirements(currTrapType);
            switch (currTrapType)
            {
                case TrapBehavior.TrapType.ThornField:
                    {
                        if (Player.GetComponent<InventoryManager>().numThorns >= tempTrap.GetComponent<TrapBehavior>().thornRequirement &&
                            Player.GetComponent<InventoryManager>().numRocks >= tempTrap.GetComponent<TrapBehavior>().rockRequirement)
                        {
                            ///checks to see if the trap upgrade is active and acts accordingly
                            if (trapThornFieldCount >= 10)
                                delay = tempTrap.GetComponent<TrapBehavior>().trapDelay * 0.5f;
                            else
                                delay = tempTrap.GetComponent<TrapBehavior>().trapDelay;
                            canBuild = true;
                            buildingTrap = tempTrap;
                            building = true;
                            buildingLoc = Player.GetComponent<Transform>().position;
                        }
                    }
                    break;
                case TrapBehavior.TrapType.GopherHole:
                    {
                        if (Player.GetComponent<InventoryManager>().numTwigs >= tempTrap.GetComponent<TrapBehavior>().stickRequirement &&
                            Player.GetComponent<InventoryManager>().numLeaves >= tempTrap.GetComponent<TrapBehavior>().leafRequirement)
                        {
                            ///checks to see if the trap upgrade is active and acts accordingly
                            if (trapGopherHoleCount >= 10)
                                delay = tempTrap.GetComponent<TrapBehavior>().trapDelay * 0.5f;
                            else
                                delay = tempTrap.GetComponent<TrapBehavior>().trapDelay;
                            canBuild = true;
                            buildingTrap = tempTrap;
                            building = true;
                            buildingLoc = Player.GetComponent<Transform>().position;

                        }
                    }
                    break;
                case TrapBehavior.TrapType.TreeTrap:
                    {
                        Collider[] hitColliders = Physics.OverlapSphere(Player.transform.position, 1.0f);
                        GameObject tree = null;
                        for (int i = 0; i < hitColliders.Length; i++)
                        {
                            if (hitColliders[i].gameObject.tag == "Hiding Object" && hitColliders[i].gameObject.GetComponent<HidingObjectBehavior>().hidingObjectType == HidingObjectBehavior.HidingObjectType.Tree)
                            {
                                tree = hitColliders[i].gameObject;
                            }
                        }

                        if (tree != null && !tree.GetComponent<HidingObjectBehavior>().hasTrap &&
                            Player.GetComponent<InventoryManager>().numRope >= tempTrap.GetComponent<TrapBehavior>().ropeRequirement &&
                            Player.GetComponent<InventoryManager>().numTwigs >= tempTrap.GetComponent<TrapBehavior>().stickRequirement &&
                            Player.GetComponent<InventoryManager>().numLeaves >= tempTrap.GetComponent<TrapBehavior>().leafRequirement)
                        {
                            currTree = tree;
                            float distance = (transform.position - currTree.transform.position).x;
                            ///checks to see if the trap upgrade is active and acts accordingly
                            if (trapTreeTrapCount >= 10)
                                delay = tempTrap.GetComponent<TrapBehavior>().trapDelay * 0.5f;
                            else
                                delay = tempTrap.GetComponent<TrapBehavior>().trapDelay;

                            Debug.Log("Dist to Tree: " + distance);
                            if (distance > 0.0f)
                                tempTrap.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                            else
                                tempTrap.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                            canBuild = true;
                            buildingTrap = tempTrap;
                            buildingTrap.GetComponent<TrapBehavior>().currTree = tree;
                            building = true;
                            buildingLoc = Player.GetComponent<Transform>().position;
                            trapTreeTrapCount += 1;
                        }
                    }
                    break;
                case TrapBehavior.TrapType.TripTrap:
                    {
                        if (Player.GetComponent<InventoryManager>().numRope >= tempTrap.GetComponent<TrapBehavior>().ropeRequirement &&
                            Player.GetComponent<InventoryManager>().numTwigs >= tempTrap.GetComponent<TrapBehavior>().stickRequirement &&
                            Player.GetComponent<InventoryManager>().numLeaves >= tempTrap.GetComponent<TrapBehavior>().leafRequirement)
                        {
                            ///checks to see if the trap upgrade is active and acts accordingly
                            if (trapTripTrapCount >= 10)
                                delay = tempTrap.GetComponent<TrapBehavior>().trapDelay * 0.5f;
                            else
                                delay = tempTrap.GetComponent<TrapBehavior>().trapDelay;
                            canBuild = true;
                            buildingTrap = tempTrap;
                            building = true;
                            buildingLoc = Player.GetComponent<Transform>().position;

                        }
                    }
                    break;
                case TrapBehavior.TrapType.Pitfall:
                    {
                        if (Player.GetComponent<InventoryManager>().numThorns >= tempTrap.GetComponent<TrapBehavior>().thornRequirement &&
                            Player.GetComponent<InventoryManager>().numTwigs >= tempTrap.GetComponent<TrapBehavior>().stickRequirement &&
                            Player.GetComponent<InventoryManager>().numLeaves >= tempTrap.GetComponent<TrapBehavior>().leafRequirement)
                        {
                            ///checks to see if the trap upgrade is active and acts accordingly
                            if (trapPitfallCount >= 10)
                                delay = tempTrap.GetComponent<TrapBehavior>().trapDelay * 0.5f;
                            else
                                delay = tempTrap.GetComponent<TrapBehavior>().trapDelay;
                            canBuild = true;
                            buildingTrap = tempTrap;
                            building = true;
                            buildingLoc = Player.GetComponent<Transform>().position;

                        }
                    }
                    break;
                case TrapBehavior.TrapType.RockTripTrap:
                    {
                        if (Player.GetComponent<InventoryManager>().numRope >= tempTrap.GetComponent<TrapBehavior>().ropeRequirement &&
                            Player.GetComponent<InventoryManager>().numTwigs >= tempTrap.GetComponent<TrapBehavior>().stickRequirement &&
                            Player.GetComponent<InventoryManager>().numLeaves >= tempTrap.GetComponent<TrapBehavior>().leafRequirement &&
                            Player.GetComponent<InventoryManager>().numRocks >= tempTrap.GetComponent<TrapBehavior>().rockRequirement)
                        {
                            ///checks to see if the trap upgrade is active and acts accordingly
                            if (trapRockTripTrapCount >= 10)
                                delay = tempTrap.GetComponent<TrapBehavior>().trapDelay * 0.5f;
                            else
                                delay = tempTrap.GetComponent<TrapBehavior>().trapDelay;
                            canBuild = true;
                            buildingTrap = tempTrap;
                            building = true;
                            buildingLoc = Player.GetComponent<Transform>().position;

                        }
                    }
                    break;
                default:
                    break;
            }

            if (gameObject.name == "TutorialPlayer")
            {
                builtTraps.Add(tempTrap);
            }
            // If we have gloves, build traps faster
            if (hasGloves)
                delay *= 0.8f;
        }
        return canBuild;
    }

    public void CheckPowerUps()
    {
        if (invManager.haveGloves == true && hasGloves == false)
        {
            ToggleActive gloveState = gloves.GetComponent<ToggleActive>();
            gloveState.SetActiveState();
            hasGloves = true;
        }
    }
}
