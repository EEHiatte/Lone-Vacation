using UnityEngine;
using System.Collections;

public class Weather_Room : MonoBehaviour 
{
    public GameObject room;

    void OnTriggerEnter()
    {
        Room_Controller rm = room.gameObject.GetComponent<Room_Controller>();
        if (gameObject.name == "Hot Weather")
        {
            SendMessageUpwards("ApplyWeather", 2);
            if (rm.hotWeather == true)
            {
                rm.weatherCount++;
                rm.hotWeather = false;
            }
        }

        else if (gameObject.name == "Cold Weather")
        {
            SendMessageUpwards("ApplyWeather", 1);
            if (rm.coldWeather == true)
            {
                rm.weatherCount++;
                rm.coldWeather = false;
            }
        }

        else if (gameObject.name == "Rain Weather")
        {
            SendMessageUpwards("ApplyWeather", 3);
            if (rm.rainWeather == true)
            {
                rm.weatherCount++;
                rm.rainWeather = false;
            }
        }
    }

    void OnTriggerExit()
    {
        SendMessageUpwards("ApplyWeather", 0, SendMessageOptions.RequireReceiver);
    }
}
