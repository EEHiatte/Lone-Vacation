using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorColliding : MonoBehaviour 
{
    public GameObject player;
    public GameObject mainRoom;
    public GameObject room;

    public GameObject enemy;

    public List<Object> pickups;

    public int currRoom = 0;

    void Awake()
    {
        if (Application.isMobilePlatform)
        {
            GameObject.Find("Room1 Controls").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Sprint").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Room2 Controls").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Room3 Controls").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Room6 Controls1").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Room6 Controls2").GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("Room6 Controls3").GetComponent<MeshRenderer>().enabled = false;

            GameObject.Find("Tablet move").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Tablet Sprint").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Tablet Objects").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Tablet Fire").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Tablet Pond").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Tablet Eat").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find("Tablet Hide").GetComponent<MeshRenderer>().enabled = true;
        }
    }

    void OnTriggerEnter(Collider coli)
    {
        MainRoom main = mainRoom.GetComponent<MainRoom>();
        InventoryManager inven = player.GetComponent<InventoryManager>();
        //Room_Controller rm = room.GetComponent<Room_Controller>();

        if (coli.gameObject.tag == "Door1")
        {
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = new Vector3(-76, 1, 34);
            player.GetComponent<NavMeshAgent>().enabled = true;
            currRoom = 1;
        }
        if (coli.gameObject.tag == "Door2" && main.OpenDoors[1])
        {
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = new Vector3(-76, 1, 100);
            player.GetComponent<NavMeshAgent>().enabled = true;
            Instantiate(pickups[0], new Vector3(-85, 0.5f, 116), new Quaternion());
            Instantiate(pickups[1], new Vector3(-87, 0.5f, 124), new Quaternion());
            Instantiate(pickups[2], new Vector3(-74, 0.5f, 118), new Quaternion());
            Instantiate(pickups[3], new Vector3(-65, 0.5f, 114), new Quaternion());
            Instantiate(pickups[4], new Vector3(-80, 0.5f, 106), new Quaternion());
            currRoom = 2;
        }
        if (coli.gameObject.tag == "Door3" && main.OpenDoors[2])
        {
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = new Vector3(-76, 1, 173);
            player.GetComponent<NavMeshAgent>().enabled = true;
            inven.numTwigs = 20;
            currRoom = 3;
        }
        if (coli.gameObject.tag == "Door4" && main.OpenDoors[3])
        {
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = new Vector3(-76, 1, 236);
            player.GetComponent<NavMeshAgent>().enabled = true;
            currRoom = 4;
        }
        if (coli.gameObject.tag == "Door5" && main.OpenDoors[4])
        {
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = new Vector3(-76, 1, 303);
            player.GetComponent<NavMeshAgent>().enabled = true;
            currRoom = 5;
        }
        if (coli.gameObject.tag == "Door6" && main.OpenDoors[5])
        {
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = new Vector3(-76, 1, 363);
            player.GetComponent<NavMeshAgent>().enabled = true;
            currRoom = 6;
        }
        if (coli.gameObject.tag == "Door7" && main.OpenDoors[6])
        {
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = new Vector3(-76, 1, 426);
            player.GetComponent<NavMeshAgent>().enabled = true;
            currRoom = 7;
        }
        if (coli.gameObject.tag == "Door8" && main.OpenDoors[7])
        {
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = new Vector3(-76, 1, 492);
            player.GetComponent<NavMeshAgent>().enabled = true;
            currRoom = 8;
        }
        //if (coli.gameObject.tag == "Door9" && main.OpenDoors[8])
        //{
        //    player.GetComponent<NavMeshAgent>().enabled = false;
        //    player.transform.position = new Vector3(-116, 1, -146);
        //    player.GetComponent<NavMeshAgent>().enabled = true;
        //    rm.fire = GameObject.Find("Fire Pit 1");
        //    currRoom = 9;
        //}
        if (coli.gameObject.name == "Door 10")
        {
            GameObject.Find("LevelLoader").GetComponent<LevelLoaderBehavior>().LoadLevel(0);
            //Application.LoadLevel(0);
        }
    }
}
