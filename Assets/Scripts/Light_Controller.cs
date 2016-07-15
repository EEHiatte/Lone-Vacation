using UnityEngine;
using System.Collections;

public class Light_Controller : MonoBehaviour 
{
    public Material GreenLight;
    public Material RedLight;
    public GameObject Room;
    bool room_light = true;

	void OnTriggerEnter(Collider coli)
    {
        Room_Controller room = Room.GetComponent<Room_Controller>();
        if (coli.gameObject.tag == "Player" && room_light)
        {
            gameObject.GetComponent<Renderer>().material = GreenLight;
            room.num_lights++;
            room_light = false;
        }
    }

    public void ResetLights()
    {
        gameObject.GetComponent<Renderer>().material = RedLight;
        room_light = true;
    }
}
