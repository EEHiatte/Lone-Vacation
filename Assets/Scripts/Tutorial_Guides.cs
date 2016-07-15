using UnityEngine;
using System.Collections;

public class Tutorial_Guides : MonoBehaviour 
{
    GameObject bubbles;
    bool isTalking = false;
	// Use this for initialization
	void Start () 
    {
        bubbles = GameObject.FindGameObjectWithTag("ThoughtBubbles");
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.E))
            isTalking = true;
	}

    void OnTriggerStay(Collider coli)
    {
        if (coli.gameObject.tag == "Player")
        {
            bubbles.GetComponent<ThoughtBubbleBehavior>().ActivateControlBubble((int)ThoughtBubbleBehavior.BubbleTypes.EBubble);
          
            if (isTalking == true)
            {
                if (gameObject.name == "TutorialGuide Main")
                    GameObject.Find("RoomManager").GetComponent<RoomManager>().page[0] = 0;
                if (gameObject.name == "TutorialGuide 1")
                    GameObject.Find("RoomManager").GetComponent<RoomManager>().page[1] = 0;
                else if (gameObject.name == "TutorialGuide 2")
                    GameObject.Find("RoomManager").GetComponent<RoomManager>().page[2] = 0;
                else if (gameObject.name == "TutorialGuide 3")
                    GameObject.Find("RoomManager").GetComponent<RoomManager>().page[3] = 0;
                else if (gameObject.name == "TutorialGuide 4")
                    GameObject.Find("RoomManager").GetComponent<RoomManager>().page[4] = 0;
                else if (gameObject.name == "TutorialGuide 5")
                    GameObject.Find("RoomManager").GetComponent<RoomManager>().page[5] = 0;
                else if (gameObject.name == "TutorialGuide 6")
                    GameObject.Find("RoomManager").GetComponent<RoomManager>().page[6] = 0;
                else if (gameObject.name == "TutorialGuide 7")
                    GameObject.Find("RoomManager").GetComponent<RoomManager>().page[7] = 0;
                else if (gameObject.name == "TutorialGuide 8")
                    GameObject.Find("RoomManager").GetComponent<RoomManager>().page[8] = 0;

                isTalking = false;
            }
        }
    }

    void OnTriggerExit(Collider coli)
    {
        if (coli.gameObject.tag == "Player")
        {
            bubbles.GetComponent<ThoughtBubbleBehavior>().DeactivateControlBubble((int)ThoughtBubbleBehavior.BubbleTypes.EBubble);
        }
    }
}
