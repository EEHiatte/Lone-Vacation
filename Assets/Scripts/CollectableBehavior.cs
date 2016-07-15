using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectableBehavior : MonoBehaviour
{
    public int collectibleType = -1;
    public float collectDistance = 0;

    bool queuePickup = false;

    bool bubbleActive = false;

    GameObject player;
    GameObject thoughtBubbles;

    SpriteRenderer currSprite;

    public bool playerIsNear = false;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        thoughtBubbles = GameObject.FindGameObjectWithTag("ThoughtBubbles");
        
        collectDistance = 3.0f;
        
        transform.rotation = Quaternion.Euler(45.0f, 0, 0);
        UpdateSprite();

        //GetComponent<BoxCollider>().isTrigger = true;
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
              {
                  if (distance < collectDistance)
                      Collect();
              }
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

    public int Collect()
    {
        if (player.GetComponent<InventoryManager>().IncreaseItem(collectibleType, 0))
        {
            //player.GetComponent<AudioSource>().Play();
            if (bubbleActive)
            {
                bubbleActive = false;
                thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().DeactivateControlBubble((int)ThoughtBubbleBehavior.BubbleTypes.FBubble);
            }
            Destroy(gameObject);
            return 1;
        }
        return 0;
    }

    public void UpdateSprite()
    {
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        if (sprites[0] != null)
        {
            if (collectibleType < 5)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (i == collectibleType)
                    {
                        sprites[i].enabled = true;
                        currSprite = sprites[i];
                    }
                    else
                        sprites[i].enabled = false;
                }
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