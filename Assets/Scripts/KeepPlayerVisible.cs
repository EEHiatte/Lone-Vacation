using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeepPlayerVisible : MonoBehaviour
{
    public LayerMask layerMask;
    GameObject player;
    HashSet<GameObject> hitObjects = new HashSet<GameObject>();
    HashSet<GameObject> fadedObjects = new HashSet<GameObject>();

    // Use this for initialization
    void Start()
    {
        // Find player with tag
        player = GameObject.FindGameObjectWithTag("Player");
        // If we can't, find player by object name
        if (player == null)
            player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Get the vector to the player from the camera
        Vector3 toPlayer = player.transform.position - transform.position;

        // All of our hits
        RaycastHit[] rayHits;

        // Shoot a ray at the player, storing all of the hits
        rayHits = Physics.RaycastAll(transform.position, toPlayer, toPlayer.magnitude, layerMask);

        // Clear our old hitObjects
        hitObjects.Clear();

        // For every hit we got, put it in our list of hit objects
        foreach (RaycastHit hit in rayHits) hitObjects.Add(hit.collider.gameObject);

        if (!player.GetComponent<PlayerController>().currentlyHidingOrResting)
        {
            // Loop through all non-faded objects
            foreach (GameObject obj in hitObjects) if (!fadedObjects.Contains(obj))
                {
                    // Make sure we don't target the player
                    if (obj.tag != "Player" && !player.GetComponent<PlayerController>().currentlyHidingOrResting && obj.GetComponent<SpriteRenderer>())
                    {
                        // Set alpha to half
                        Color color = obj.GetComponent<SpriteRenderer>().color; // TODO: FIX THIS IF IT HAPPENS TO BREAK
                        color.a = 0.5f;
                        obj.GetComponent<SpriteRenderer>().color = color;
                        // Add to our list of faded objects
                        fadedObjects.Add(obj);
                    }
                }
        }
        else
        {
            hitObjects.Clear();
        }

        // Loops through faded objects and checks if they are still supposed to be faded
        foreach (GameObject obj in fadedObjects) if (!hitObjects.Contains(obj))
            {
                // Make sure this is not the player
                if (obj != null && obj.tag != "Player" && obj.GetComponent<SpriteRenderer>())
                {
                    // Set alpha back to maximum
                    Color color = obj.GetComponent<SpriteRenderer>().color; // TODO: FIX THIS IF IT BREAKS
                    color.a = 1;
                    obj.GetComponent<SpriteRenderer>().color = color;
                }
            }

        // Remove any faded objects that are not hit anymore
        fadedObjects.RemoveWhere(obj => !hitObjects.Contains(obj));

    }
}
