using UnityEngine;
using System.Collections;

public class CoveDecorationPiece : MonoBehaviour
{
    CoveDecorations decorationManager;

    public CoveDecorations.CoveDecorationType pieceType;
    public int pieceNumber;

    float collectDistance = 0;

    bool queuePickup = false;
    bool bubbleActive = false;

    GameObject player;
    GameObject thoughtBubbles;

    SpriteRenderer currSprite;

    public bool playerIsNear = false;

    // Use this for initialization
    void Start()
    {
        decorationManager = GameObject.Find("GameManager").GetComponent<CoveDecorations>();

        player = GameObject.FindGameObjectWithTag("Player");
        thoughtBubbles = GameObject.FindGameObjectWithTag("ThoughtBubbles");

        collectDistance = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsNear)
        {
            float distance = (player.transform.position - this.transform.position).magnitude;

            if (distance < collectDistance)
            {
                thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().ActivateControlBubble((int)ThoughtBubbleBehavior.BubbleTypes.FBubble);
                bubbleActive = true;

                if (Input.GetKey(KeyCode.F))
                    Collect();                  
            }
            else
                playerIsNear = false;
        }
        if (bubbleActive && !playerIsNear)
        {
            bubbleActive = false;
            thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().DeactivateControlBubble((int)ThoughtBubbleBehavior.BubbleTypes.FBubble);
        }

        if (queuePickup)
        {
            if (player.GetComponent<PlayerController>().walkToObject == false)
                queuePickup = false;
            else
            {
                float distance = (player.transform.position - this.transform.position).magnitude;
                if (distance < collectDistance)
                {
                    player.GetComponent<PlayerController>().walkToObject = false;
                    player.GetComponent<PlayerController>().currentPickup = null;
                    queuePickup = false;
                    Collect();
                }
            }
        }
    }

    void OnMouseEnter()
    {
        currSprite.material.color = new Color(0.5f, 0.5f, 0.5f);
    }
    void OnMouseExit()
    {
        currSprite.material.color = new Color(1.0f, 1.0f, 1.0f);
    }
    void OnMouseOver()
    {
        if (Input.GetMouseButton(0))
        {
            float distance = (player.transform.position - this.transform.position).magnitude;
            if (distance < collectDistance)
                Collect();
       
            else
            {
                player.GetComponent<PlayerController>().walkToObject = true;
                player.GetComponent<PlayerController>().currentPickup = gameObject;
                player.GetComponent<PlayerController>().distToTargetStop = collectDistance;
                queuePickup = true;
            }
        }
    }

    public void Collect()
    {
        if (bubbleActive)
        {
            bubbleActive = false;
            thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().DeactivateControlBubble((int)ThoughtBubbleBehavior.BubbleTypes.FBubble);
        }
        decorationManager.CollectPiece(pieceType, gameObject);

        player.gameObject.GetComponent<AudioSource>().Play();
    }

    public void UpdateSprite()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i != pieceNumber)
                GetComponentsInChildren<SpriteRenderer>()[i].enabled = false;
            else
            {
                GetComponentsInChildren<SpriteRenderer>()[i].enabled = true;
                currSprite = GetComponentsInChildren<SpriteRenderer>()[i];
            }
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
