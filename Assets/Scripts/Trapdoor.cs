using UnityEngine;
using System.Collections;

public class Trapdoor : MonoBehaviour 
{
    public Sprite trapdoor;


    void Awake()
    {
        gameObject.GetComponent<HidingObjectBehavior>().trapdoor = true;
    }
	void OnTriggerStay(Collider coli)
    {
        if (gameObject.GetComponentInParent<HidingObjectBehavior>().currentlyHiding == true)
        {
            if (coli.gameObject.tag == "Player")
            {
                for (int i = 1; i <= 6; i++ )
                    GameObject.Find("Secret RoomDoor " + i).GetComponent<SeceretRoom>().orgSpot = GameObject.FindGameObjectWithTag("Player").transform.position;
                GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>().enabled = false;

                if (gameObject.name == "Bush Trapdoor")
                    GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(-1212, 37, 1027);
                else if (gameObject.name == "BerryBush Trapdoor")
                    GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(-1313.5f, 37, 1027);
                else if (gameObject.name == "ThornBush Trapdoor")
                    GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(-1414, 37, 1027);
                else if (gameObject.name == "Bush Trapdoor 1")
                    GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(-1515.5f, 37, 1027);
                else if (gameObject.name == "BerryBush Trapdoor 1")
                    GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(-1616, 37, 1027);
                else if (gameObject.name == "ThornBush Trapdoor 1")
                    GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(-1717.5f, 37, 1027);

                GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>().enabled = true;

                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().currentlyHidingOrResting = false;
                gameObject.GetComponent<HidingObjectBehavior>().currentlyHiding = false;
                coli.gameObject.GetComponentInChildren<ThoughtBubbleBehavior>().DeactivateControlBubble((int)ThoughtBubbleBehavior.BubbleTypes.EBubble);

                gameObject.GetComponentInChildren<SpriteRenderer>().sprite = trapdoor;
            }
        }
    }
}
