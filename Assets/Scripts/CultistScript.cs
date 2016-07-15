using UnityEngine;
using System.Collections;

public class CultistScript : MonoBehaviour 
{
    GameObject player;
    //AudioSource audio;
    float activationDistance;
    bool activated = false;
    AudioSource voiceSFX;
    SpriteRenderer[] cultistVisuals;

    public bool playerIsNear = false;

	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cultistVisuals = GetComponentsInChildren<SpriteRenderer>();
        voiceSFX = GetComponent<AudioSource>();
        activationDistance = 3.0f;
        cultistVisuals[1].enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (playerIsNear)
        {
            float distance = (player.transform.position - transform.position).magnitude;
            if (distance <= activationDistance)
            {
                activated = true;
            }
            else
                playerIsNear = false;
        }
        else if (activated)
        {
            activated = false;
            cultistVisuals[0].enabled = true;
            cultistVisuals[1].enabled = false;
            voiceSFX.Stop();
        }

        if(activated)
        {
            if (!voiceSFX.isPlaying)
                voiceSFX.Play();
            cultistVisuals[1].enabled = true;
            cultistVisuals[0].enabled = false;
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
