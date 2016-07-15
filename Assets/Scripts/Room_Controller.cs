using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room_Controller : MonoBehaviour 
{
    public GameObject player;
    public GameObject Mainroom;
    public GameObject fire;
    public List<GameObject> lights;
    public GameObject roomMan;
    public GameObject RoomTraps;

    public bool Unlock_Door = false;

    public int num_lights = 0;

    public int traps = 0;

    public int switchLocks = 0;

    public int weatherCount = 0;
    public bool hotWeather = true;
    public bool coldWeather = true;
    public bool rainWeather = true;

    public int terrianCount = 0;
    public bool marshTerrian = true;
    public bool snowTerrian = true;
    public bool beachTerrian = true;

    static public bool finished = false;

    public GUIStyle textStyle;

	// Use this for initialization
	void Start () 
    {
        PlayerStatManager Stats = player.gameObject.GetComponent<PlayerStatManager>();

        Stats.playerHunger = 98;
        Stats.playerThirst = 98;

	}
	
	// Update is called once per frame
	void Update () 
    {
        InventoryManager inven = player.gameObject.GetComponent<InventoryManager>();

        PlayerStatManager Stats = player.gameObject.GetComponent<PlayerStatManager>();

        Stats.hungerDecayRate = 0;
        Stats.thirstDecayRate = 0;
        Stats.playerHealth = 100;

        Unlock_Door = false;

        if (traps >= 6)
        {
            Unlock_Door = true;
        }

        if (gameObject.tag == "Room2" && inven.numLeaves >= 1 && inven.numRocks >= 1 && inven.numRope >= 1 && inven.numThorns >= 1 && inven.numTwigs >= 1)
        {
            Unlock_Door = true;
        }


        if (fire.GetComponent<FireBehavior>().fireIntensity > 74 && fire == GameObject.Find("Fire Pit"))
        {           
            Unlock_Door = true;
        }

        if (Stats.playerHunger >= 100 && Stats.playerThirst >= 100)
        {
            Unlock_Door = true;
        }

        if (num_lights == 3)
        {
            Unlock_Door = true;
        }

        if (fire.GetComponent<FireBehavior>().fireIntensity > 35 && fire == GameObject.Find("Fire Pit 1"))
        {
            Unlock_Door = true;
        }

        if (switchLocks >= 1)
            Unlock_Door = true;

        if (weatherCount == 3)
            Unlock_Door = true;

        if (terrianCount == 3)
            Unlock_Door = true;
	}

    void OnTriggerEnter(Collider coli)
    {
        MainRoom main = Mainroom.GetComponent<MainRoom>();
        PlayerStatManager Stats = player.gameObject.GetComponent<PlayerStatManager>();
        InventoryManager inven = player.gameObject.GetComponent<InventoryManager>();
        DoorColliding current = player.gameObject.GetComponent<DoorColliding>();
        //RoomManager pageNum = roomMan.gameObject.GetComponent<RoomManager>();
        //TrapRoom trapRoom = RoomTraps.gameObject.GetComponent<TrapRoom>();
        //PlayerCreateTrap createTrap = player.gameObject.GetComponent<PlayerCreateTrap>();

        if (coli.gameObject.tag == "Player")
        {
            if (gameObject.tag == "Room1" && Unlock_Door)
            {
                player.GetComponent<NavMeshAgent>().enabled = false;
                player.transform.position = new Vector3(-76, 1, -49);
                player.GetComponent<NavMeshAgent>().enabled = true;
                main.OpenDoors[1] = true;
                Unlock_Door = false;
                num_lights = 0;
                foreach (var item in lights)
                {
                    Light_Controller resetLights = item.GetComponent<Light_Controller>();
                    resetLights.ResetLights();
                }
                current.currRoom = 0;
            }
            if (gameObject.tag == "Room2" && Unlock_Door)
            {
                player.GetComponent<NavMeshAgent>().enabled = false;
                player.transform.position = new Vector3(-76, 1, -49);
                player.GetComponent<NavMeshAgent>().enabled = true;
                main.OpenDoors[2] = true;
                Unlock_Door = false;

                inven.numTwigs = 0;
                inven.numThorns = 0;
                inven.numRope = 0;
                inven.numRocks = 0;
                inven.numLeaves = 0;

                current.currRoom = 0;
            }
            if (gameObject.tag == "Room3" && Unlock_Door)
            {
                player.GetComponent<NavMeshAgent>().enabled = false;
                player.transform.position = new Vector3(-76, 1, -49);
                player.GetComponent<NavMeshAgent>().enabled = true;
                main.OpenDoors[3] = true;
                Unlock_Door = false;
                fire.GetComponent<FireBehavior>().fireIntensity = 35;
                Stats.playerHunger = 98;
                Stats.playerThirst = 98;

                current.currRoom = 0;
            }
            if (gameObject.tag == "Room4" && Unlock_Door)
            {
                player.GetComponent<NavMeshAgent>().enabled = false;
                player.transform.position = new Vector3(-76, 1, -49);
                player.GetComponent<NavMeshAgent>().enabled = true;
                main.OpenDoors[4] = true;
                Unlock_Door = false;
                Stats.playerHunger = 99;
                Stats.playerThirst = 99;

                current.currRoom = 0;
            }
            if (gameObject.tag == "Room5" && Unlock_Door)
            {
                player.GetComponent<NavMeshAgent>().enabled = false;
                player.transform.position = new Vector3(-76, 1, -49);
                player.GetComponent<NavMeshAgent>().enabled = true;
                main.OpenDoors[5] = true;
                Unlock_Door = false;
                traps = 0;

                //for (int i = 0; i < trapRoom.trapping.Count; i++)
                //    trapRoom.trapping[i] = true;

                current.currRoom = 0;
            }
            if (gameObject.tag == "Room6" && Unlock_Door)
            {
                player.GetComponent<NavMeshAgent>().enabled = false;
                player.transform.position = new Vector3(-76, 1, -49);
                player.GetComponent<NavMeshAgent>().enabled = true;
                main.OpenDoors[6] = true;
                Unlock_Door = false;

                Stats.playerHunger = 99;
                Stats.playerThirst = 99;

                current.currRoom = 0;
            }
            if (gameObject.tag == "Room7" && Unlock_Door)
            {
                player.GetComponent<NavMeshAgent>().enabled = false;
                player.transform.position = new Vector3(-76, 1, -49);
                player.GetComponent<NavMeshAgent>().enabled = true;
                Unlock_Door = false;
                hotWeather = true;
                coldWeather = true;
                rainWeather = true;
                weatherCount = 0;
                main.OpenDoors[7] = true;

                current.currRoom = 0;
            }
            if (gameObject.tag == "Room8" && Unlock_Door)
            {
                /// Keith's Code for achievement
                PlayerPrefs.SetInt("TutorialFinished", 1);
                PlayerPrefs.Save();
                ///////

                finished = true;

                player.GetComponent<NavMeshAgent>().enabled = false;
                player.transform.position = new Vector3(-76, 1, -49);
                player.GetComponent<NavMeshAgent>().enabled = true;
                Unlock_Door = false;
                marshTerrian = true;
                snowTerrian = true;
                beachTerrian = true;
                terrianCount = 0;
                //main.OpenDoors[8] = true;

                current.currRoom = 0;
            }
            if (gameObject.tag == "Room9")
            {
                player.GetComponent<NavMeshAgent>().enabled = false;
                player.transform.position = new Vector3(-76, 1, -49);
                player.GetComponent<NavMeshAgent>().enabled = true;
                Unlock_Door = false;
            }
        }
    }
}
