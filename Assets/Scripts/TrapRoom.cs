using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrapRoom : MonoBehaviour 
{
    public GameObject player;
    public GameObject room;
    //public List<bool> trapping = new List<bool>();

    void Start()
    {
        //for (int i = 0; i < 6; i++)
        //    trapping.Add(true);
    }

    void OnTriggerEnter()
    {
        InventoryManager inven = player.gameObject.GetComponent<InventoryManager>();

        if (gameObject.name == "Gopher Hole")
        {
            inven.numTwigs = 2;
            inven.numLeaves = 1;
        }
        else if (gameObject.name == "Pitfall")
        {
            inven.numTwigs = 3;
            inven.numLeaves = 2;
            inven.numThorns = 2;
        }
        else if (gameObject.name == "TripTrap")
        {
            inven.numTwigs = 2;
            inven.numLeaves = 2;
            inven.numRope = 1;
        }
        else if (gameObject.name == "Thorn Field")
        {
            inven.numThorns = 2;
            inven.numRocks = 1;
        }
        else if (gameObject.name == "Rock Trip Trap")
        {
            inven.numTwigs = 2;
            inven.numLeaves = 2;
            inven.numRocks = 1;
            inven.numRope = 2;
        }
        else if (gameObject.name == "Tree Trap")
        {
            inven.numTwigs = 2;
            inven.numLeaves = 1;
            inven.numRope = 2;
        }
    }

    void OnTriggerStay()
    {
        Room_Controller s_room = room.gameObject.GetComponent<Room_Controller>();
        if (Input.GetKey(KeyCode.Alpha1) || Input.GetMouseButtonDown(0))
        {
            s_room.traps++;
            //trapping[0] = false;
        }
        if (Input.GetKey(KeyCode.Alpha2) || Input.GetMouseButtonDown(0))
        {
            s_room.traps++;
            //trapping[1] = false;
        }
        if (Input.GetKey(KeyCode.Alpha3) || Input.GetMouseButtonDown(0))
        {
            s_room.traps++;
            //trapping[2] = false;
        }
        if (Input.GetKey(KeyCode.Alpha4) || Input.GetMouseButtonDown(0))
        {
            s_room.traps++;
//trapping[3] = false;
        }
        if (Input.GetKey(KeyCode.Alpha5) || Input.GetMouseButtonDown(0))
        {
            s_room.traps++;
            //trapping[4] = false;
        }
        if (Input.GetKey(KeyCode.Alpha6) || Input.GetMouseButtonDown(0))
        {
            s_room.traps++;
            //trapping[5] = false;
        }
    }

    void OnTriggerExit()
    {
        InventoryManager inven = player.gameObject.GetComponent<InventoryManager>();

        inven.numTwigs = 0;
        inven.numThorns = 0;
        inven.numRope = 0;
        inven.numRocks = 0;
        inven.numLeaves = 0;
    }
}
