using UnityEngine;
using System.Collections;

public class PigHeadBehavior : MonoBehaviour 
{
    GameObject player;
    bool playerIsNear = false;
    AudioSource fliesSFX;

    SFXVolume sfxVolume;

	// Use this for initialization
	void Start () 
    {
        sfxVolume = GetComponent<SFXVolume>();
        player = GameObject.FindGameObjectWithTag("Player");
        fliesSFX = GetComponent<AudioSource>();
        fliesSFX.volume = 0.0f;
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(playerIsNear)
        {
            float distance = (player.transform.position - transform.position).magnitude + 10;
            fliesSFX.volume = distance;
            Debug.Log(fliesSFX.volume);
            if (sfxVolume && fliesSFX.volume > sfxVolume.volume)
                fliesSFX.volume = sfxVolume.volume;
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            playerIsNear = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsNear = false;
            fliesSFX.volume = 0.0f;
        }
    }

    void OnBecameInvisible()
    {
        enabled = false;
    }

    void OnBecameVisible()
    {
        enabled = true;
    }
}
