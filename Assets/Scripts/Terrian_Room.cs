using UnityEngine;
using System.Collections;

public class Terrian_Room : MonoBehaviour 
{
    public GameObject room;
    void OnTriggerEnter(Collider coli)
    {
        Room_Controller rm = room.gameObject.GetComponent<Room_Controller>();
        if (gameObject.name == "Snow")
        {
            if (rm.snowTerrian == true)
            {
                rm.terrianCount++;
                rm.snowTerrian = false;
            }
        }

        else if (gameObject.name == "Beach")
        {
            if (rm.beachTerrian == true)
            {
                rm.terrianCount++;
                rm.beachTerrian = false;
            }
        }

        else if (gameObject.name == "Marsh")
        {
            if (rm.marshTerrian == true)
            {
                rm.terrianCount++;
                rm.marshTerrian = false;
            }
        }
    }
}
