using UnityEngine;
using System.Collections;

public class PondBehavior : MonoBehaviour
{
    public float collectDistance = 7.0f;
    float sfxTimer = 1.0f;
    public bool drinking = false;
    bool queuePickup = false;
    GameObject player;
    AudioSource drinkingSFX;

    //NavMeshAgent navMeshAgent;

    GameObject thoughtBubbles;
    bool bubbleActive = false;

    // Use this for initialization
    void Start()
    {
        if (Application.loadedLevel != 0)
        {
            gameObject.layer = LayerMask.NameToLayer("TerrainEffect");
            player = GameObject.FindGameObjectWithTag("Player");
            //navMeshAgent = player.GetComponent<NavMeshAgent>();
            thoughtBubbles = GameObject.FindGameObjectWithTag("ThoughtBubbles");
            drinkingSFX = GetComponent<AudioSource>();
            GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (drinking == true)
        {
            sfxTimer += Time.deltaTime;

            if (sfxTimer >= 1.0f)
            {
                sfxTimer = 0.0f;
                drinkingSFX.Play();
            }
        }
        else if(sfxTimer != 1.0f)
            sfxTimer = 1.0f;
    }

    void OnMouseEnter()
    {
        GetComponent<MeshRenderer>().material.color = new Color(0.5f, 0.5f, 0.5f);
    }
    void OnMouseExit()
    {
        GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f);
    }
    void OnMouseOver()
    {
        //BUG #41
        if (Input.GetMouseButtonUp(0))
        {
            if (player.GetComponent<CapsuleCollider>().bounds.Intersects(this.GetComponent<BoxCollider>().bounds))
            {
                player.GetComponent<PlayerStatManager>().drinking = true;
                drinking = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerController>().slowedByLake = true;
            thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().ActivateControlBubble((int)ThoughtBubbleBehavior.BubbleTypes.FBubbleWater);
            bubbleActive = true;

            if (queuePickup)
            {
                if (player.GetComponent<PlayerController>().walkToObject == false)
                    queuePickup = false;
                else
                {

                    player.GetComponent<PlayerController>().walkToObject = false;
                    player.GetComponent<PlayerController>().currentPickup = null;
                    queuePickup = false;
                    player.GetComponent<PlayerStatManager>().drinking = true;
                    drinking = true;
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.F))
            {
                other.GetComponent<PlayerStatManager>().drinking = true;
                drinking = true;
            }

            if (player.GetComponent<PlayerStatManager>().drinking == false)
                drinking = false;

            // BUG #64
            other.GetComponent<PlayerController>().onWater = true;
            // END BUG
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerController>().slowedByLake = false;
            player.GetComponent<PlayerStatManager>().drinking = false;
            drinking = false;
            if (bubbleActive)
            {
                bubbleActive = false;
                thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().DeactivateControlBubble((int)ThoughtBubbleBehavior.BubbleTypes.FBubbleWater);
            }

            // BUG #64
            other.GetComponent<PlayerController>().onWater = false;
            // END BUG
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