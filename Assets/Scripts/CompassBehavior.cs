using UnityEngine;
using System.Collections;

public class CompassBehavior : MonoBehaviour
{
    GameObject player;
    GameObject fire;

    // Use this for initialization
    void Start()
    {
        // Find player by tag
        player = GameObject.FindGameObjectWithTag("Player");
        // If we can't, find the player by name
        if (player == null)
            player = GameObject.Find("Player");

        // Find fire by tag
        fire = GameObject.FindGameObjectWithTag("Fire");
        // If we can't, find the fire by name
        if (fire == null)
            fire = GameObject.Find("Fire");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDirection = fire.transform.position - player.transform.position;
        targetDirection.y = 0;
        float angle = Vector3.Angle(targetDirection, Vector3.forward) * Mathf.Sign(Vector3.Cross(targetDirection, Vector3.forward).y);
        if (angle == 0)
            angle = 0;
        transform.localEulerAngles = new Vector3(0, 0, angle);
    }
}
