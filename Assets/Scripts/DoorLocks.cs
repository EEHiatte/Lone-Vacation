using UnityEngine;
using System.Collections;

public class DoorLocks : MonoBehaviour {

    public GameObject room;
    void OnTriggerEnter(Collider coli)
    {
        Room_Controller rm = room.GetComponent<Room_Controller>();

        if (coli.gameObject.tag == "Player")
        {
            rm.switchLocks = 1;
        }
    }
}
