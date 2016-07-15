using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FireBehavior : MonoBehaviour
{
    public float fireIntensity;
    public float decayRate;
    public float decayTime;
    public float collectDistance;
    public float regenAmount;

    public bool playerIsNear = false;
    bool queuePutIn = false;
    bool queueRest = false;
    bool bubbleActive = false;

    GameObject player;

    public ParticleSystem p_fire;
    public ParticleSystem p_putInFire;

    GameObject FireBar;
    GameObject thoughtBubbles;

    float distance;

    // Use this for initialization
    void Start()
    {
        if (Application.loadedLevel == 0)
            Destroy(gameObject.GetComponent<FireBehavior>());
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
            thoughtBubbles = GameObject.FindGameObjectWithTag("ThoughtBubbles");

            collectDistance = 5.0f;
            fireIntensity = 35;

            FireBar = GameObject.Find("FireBar").gameObject;

            //if (GetComponentInChildren<ParticleSystem>().tag == "Fire")
            //    p_fire = GetComponentInChildren<ParticleSystem>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fireIntensity >= 50)
        {
            PlayerPrefs.SetInt("Fire 2", 1);
        }
        if (fireIntensity >= 75)
        {
            PlayerPrefs.SetInt("Fire 3", 1);
        }
        if (fireIntensity >= 100)
        {
            PlayerPrefs.SetInt("Fire 4", 1);
        }

        FireBarChange();
        decayTime -= Time.deltaTime;

        distance = (player.transform.position - this.transform.position).magnitude;

        // Check if we're close to our fire
        if (distance <= collectDistance)
        {
            player.GetComponent<PlayerController>().anObject = transform;
        }
        else if (player.GetComponent<PlayerController>().anObject == transform)
        {
            player.GetComponent<PlayerController>().anObject = null;
        }

        if (playerIsNear)
        {
            if (distance <= collectDistance)
            {
                thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().ActivateControlBubble((int)ThoughtBubbleBehavior.BubbleTypes.FBubbleFire);
                thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().ActivateControlBubble((int)ThoughtBubbleBehavior.BubbleTypes.EBubbleHeal);
                bubbleActive = true;
            }
            else
                playerIsNear = false;
        }
        else if (bubbleActive)
        {
            bubbleActive = false;
            thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().DeactivateControlBubble((int)ThoughtBubbleBehavior.BubbleTypes.FBubbleFire);
            thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().DeactivateControlBubble((int)ThoughtBubbleBehavior.BubbleTypes.EBubbleHeal);
        }

        if (decayTime < 0)
        {
            fireIntensity -= decayRate;
            decayTime = 2;
        }

        if (fireIntensity == 0)
            p_fire.emissionRate = 0;
        else if (fireIntensity >= 1 && fireIntensity <= 5)
            p_fire.emissionRate = 43;
        else if (fireIntensity >= 6 && fireIntensity <= 10)
            p_fire.emissionRate = 86;
        else if (fireIntensity >= 11 && fireIntensity <= 20)
            p_fire.emissionRate = 129;
        else if (fireIntensity >= 21 && fireIntensity <= 30)
            p_fire.emissionRate = 172;
        else if (fireIntensity >= 31 && fireIntensity <= 40)
            p_fire.emissionRate = 215;
        else if (fireIntensity >= 41 && fireIntensity <= 50)
            p_fire.emissionRate = 260;


        if (Input.GetKeyDown(KeyCode.F) && distance <= collectDistance)
        {
            QueueFire();
        }

        if (Input.GetKeyDown(KeyCode.E) && distance <= collectDistance)
        {
            QueueRest();
        }

        if (queuePutIn)
        {
            if (player.GetComponent<PlayerController>().currentPickup != this.gameObject)
            {
                queuePutIn = false;
            }
            IncreaseFire();
        }

        if (queueRest)
        {
            if (player.GetComponent<PlayerController>().currentPickup != this.gameObject)
            {
                queueRest = false;
            }
            Rest();
        }
    }

    void FireBarChange()
    {
        FireBar.GetComponent<RectTransform>().localScale = new Vector3(fireIntensity / 100, 1, 1);
        Color yellow = new Color(255f / 255f, 255f / 255f, 0);
        Color orange = new Color(255f / 255f, 125f / 255f, 0);
        if (fireIntensity < 35)
        {
            if (FireBar.GetComponentInChildren<RawImage>().color == yellow
                && decayTime < 2 && decayTime > 1.5
                || decayTime < 1 && decayTime > .5)
            {
                FireBar.GetComponentInChildren<RawImage>().color = orange;
            }
            else if (FireBar.GetComponentInChildren<RawImage>().color == orange
                && decayTime < 1.5 && decayTime > 1
                || decayTime < .5 && decayTime > 0)
            {
                FireBar.GetComponentInChildren<RawImage>().color = yellow;
            }
        }
        else
        {
            FireBar.GetComponentInChildren<RawImage>().color = yellow;
        }
    }

    public void IncreaseFire()
    {
        distance = (player.transform.position - this.transform.position).magnitude;
        if (distance < collectDistance && player.GetComponent<InventoryManager>().numTwigs > 0)
        {
            p_putInFire.Emit(5);
            GetComponent<AudioSource>().Play();
            bool canDecStick = true;
            if (fireIntensity >= 0 && fireIntensity < 25)
                fireIntensity += 4;
            else if (fireIntensity >= 25 && fireIntensity < 50)
                fireIntensity += 3;
            else if (fireIntensity >= 50 && fireIntensity < 70)
                fireIntensity += 2;
            else if (fireIntensity >= 70 && fireIntensity < 80)
                fireIntensity += 1;
            else if (fireIntensity >= 80 && fireIntensity < 90)
                fireIntensity += 0.5f;
            else if (fireIntensity >= 90 && fireIntensity < 100)
                fireIntensity += 0.2f;
            else
                canDecStick = false;

            if (canDecStick)
                player.GetComponent<InventoryManager>().DecreaseItem(0);

            queuePutIn = false;
        }
    }

    void Rest()
    {
        if (distance <= collectDistance)
        {
            queueRest = false;
            player.GetComponent<PlayerController>().currentlyHidingOrResting = true;
        }
    }

    void OnMouseEnter()
    {
        GetComponentInChildren<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
    }
    void OnMouseExit()
    {
        GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
    }

    public void QueueFire()
    {
        queuePutIn = true;
    }

    public void QueueRest()
    {
        queueRest = true;
    }
}
