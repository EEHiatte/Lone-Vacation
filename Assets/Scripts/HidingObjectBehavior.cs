using UnityEngine;
using System.Collections;

public class HidingObjectBehavior : MonoBehaviour
{
    public enum HidingObjectType { Tree, Rock, Bush };
    public HidingObjectType hidingObjectType;

    public bool currentlyHiding;
    bool bubbleActive = false;
    Transform toHide;
    Transform hiddenObject;

    public bool trapdoor = false;

    float collectDistance = 3.75f;

    public GameObject player;
    GameObject thoughtBubbles;
    //Vector3 playerSpot;

    public bool hasFood = false;
    public float foodCount = 10.0f;

    public bool hasDamage = false;
    public float damageCount = 5.0f;

    TouchExtension touchEvent;

    bool queueHide = false;
    bool queueEat = false;

    PlayerController controller;
    float distance;
    Vector3 prevPosition;

    public bool hasTrap = false;


    float hidetimer = 0f;
    void Awake()
    {
        //if (Application.loadedLevel == 0)
        //{
        //    Destroy(this);
        //}
        //if (GetComponent<MeshFilter>())
        //{
        //    Destroy(GetComponent<MeshFilter>());
        //}
        currentlyHiding = false;
        //gameObject.layer = LayerMask.NameToLayer("Environment");
        touchEvent = gameObject.AddComponent<TouchExtension>();

        BoxCollider col = GetComponent<BoxCollider>();
        if (col != null)
        {
            col.size = new Vector3(col.size.x, col.size.y, 3);
        }

        MeshRenderer rend = gameObject.AddComponent<MeshRenderer>();
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        touchEvent = gameObject.AddComponent<TouchExtension>();
        gameObject.AddComponent<CreateTooltip>();
    }    // Use this for initialization
    void Start()
    {
        if (Application.loadedLevel != 0)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            thoughtBubbles = GameObject.FindGameObjectWithTag("ThoughtBubbles");
            touchEvent.singleClick.AddListener(delegate { player.GetComponent<PlayerController>().NavigateToObject(gameObject); });
            touchEvent.singleClick.AddListener(delegate { QueueHide(player.transform); });
            if (hasFood)
            {
                touchEvent.doubleClick.AddListener(delegate { player.GetComponent<PlayerController>().NavigateToObject(gameObject); });
                touchEvent.doubleClick.AddListener(delegate { QueueEat(); });
            }

            controller = player.GetComponent<PlayerController>();

            CreateTooltip create = GetComponent<CreateTooltip>();
            if (hidingObjectType == HidingObjectType.Tree && hasFood)
            {
                create.nameText = "Banana Tree";
                create.descriptionTest = "Banana trees provide food and a place to hide from hunters.";
            }
            else if (hidingObjectType == HidingObjectType.Tree && hasDamage)
            {
                create.nameText = "Bee Tree";
                create.descriptionTest = "Bee trees are infested with bees that will swarm anything in the tree.";
                gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("AmbientVolume");
            }
            else if (hidingObjectType == HidingObjectType.Tree)
            {
                create.nameText = "Tree";
                create.descriptionTest = "Trees provide a place to hide from pursuit.";
            }
            else if (hidingObjectType == HidingObjectType.Bush && hasFood)
            {
                create.nameText = "Berry Bush";
                create.descriptionTest = "Berry bushes provide both food and a place to hide.";
            }
            else if (hidingObjectType == HidingObjectType.Bush && hasDamage)
            {
                create.nameText = "Thorn Bush";
                create.descriptionTest = "Thorns in these bushes will cause a great amount of harm to anything inside them.";
            }
            else if (hidingObjectType == HidingObjectType.Bush)
            {
                create.nameText = "Bush";
                create.descriptionTest = "Bushes provide a place to hide and nothing more.";
            }
            else if (hidingObjectType == HidingObjectType.Rock && hasFood)
            {
                create.nameText = "Bug Infested Rock";
                create.descriptionTest = "The bugs infesting these rocks provide nourishment.";
            }
            else if (hidingObjectType == HidingObjectType.Rock && hasDamage)
            {
                create.nameText = "Jagged Rock";
                create.descriptionTest = "These sharp rocks provide a place to hide but also cause bodily harm while hidden.";
            }
            else if (hidingObjectType == HidingObjectType.Rock)
            {
                create.nameText = "Rock";
                create.descriptionTest = "Rocks simply provide a place to hide.";
            }

            enabled = false;
        }
        else
        {
            //gameObject.AddComponent<BillboardRenderer>();
            gameObject.AddComponent<BillboardTest>();
            HidingObjectBehavior[] toDelete = GetComponents<HidingObjectBehavior>();
            for (int i = 0; i < toDelete.Length; i++)
            {
                Destroy(toDelete[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        hidetimer += Time.deltaTime;
        
        if (Application.loadedLevel != 0)
        {
            if (player == null)
                return;

            distance = (player.transform.position - this.transform.position).magnitude;

            if (distance > collectDistance * 2)
            {
                return;
            }

            if (distance <= collectDistance && currentlyHiding == false)
            {
                thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().ActivateControlBubble((int)ThoughtBubbleBehavior.BubbleTypes.EBubble);
                if (hasFood)
                    thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().ActivateControlBubble((int)ThoughtBubbleBehavior.BubbleTypes.FBubbleFood);
                bubbleActive = true;
            }
            else if (bubbleActive)
            {
                bubbleActive = false;
                thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().DeactivateControlBubble((int)ThoughtBubbleBehavior.BubbleTypes.EBubble);
                if (hasFood)
                    thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().DeactivateControlBubble((int)ThoughtBubbleBehavior.BubbleTypes.FBubbleFood);
            }

            if (Input.GetKeyDown(KeyCode.E) && distance <= collectDistance && hidetimer >= 1)
            {
                QueueHide(player.transform);
            }

            if (Input.GetKeyDown(KeyCode.F) && distance < collectDistance && hasFood)
            {
                QueueEat();
            }
            if (distance <= collectDistance && queueHide)
            {
                //player.GetComponent<NavMeshAgent>().enabled = false;
                Hide(toHide);
                queueHide = false;
            }

            if (distance <= collectDistance && queueEat)
            {
                Eat();
                queueEat = false;
            }
        }
        else
        {
            //transform.position = mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_MV, float4(0.0, 0.0, 0.0, 1.0)) - float4(input.vertex.x, input.vertex.z, 0.0, 0.0));
            transform.LookAt(Camera.main.transform.position);
        }
    }

    public void Hide(Transform tohide)
    {
        if (currentlyHiding == false && hidetimer >= 1)
        {
            if (trapdoor == false)
            {
            player.transform.GetChild(1).gameObject.SetActive(false);
            }

            hiddenObject = tohide;
            prevPosition = toHide.position;
            hiddenObject.transform.position = transform.position;
            //player.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            //hiddenObject.GetComponent<CapsuleCollider>().enabled = false;
            hiddenObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            currentlyHiding = true;
            player = tohide.gameObject;
            controller.eating = false;
        }
        else
        {
            if (trapdoor == false)
            {
                player.transform.GetChild(1).gameObject.SetActive(true);
            }
            //player.GetComponent<SpriteRenderer>().enabled = true;
            //controller.navMeshTarget.enabled = true;
            hiddenObject.transform.position = prevPosition;
            hiddenObject.GetComponent<CapsuleCollider>().enabled = true;
            hiddenObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            currentlyHiding = false;
            player.GetComponent<NavMeshAgent>().enabled = true;
            hidetimer = 0;
        }

        controller.currentlyHidingOrResting = !controller.currentlyHidingOrResting;
        //gameObject.GetComponent<AudioSource>().Play();
        if (hasDamage)
        {
            controller.inPainObj = !controller.inPainObj;
            controller.painTime = 2.0f;
        }

        if (controller.currentlyHidingOrResting == true)
        {
            controller.hidecount += 1;
        }

        if (PlayerPrefs.GetInt("Hide 1") != 1 && controller.hidecount >= 250)
        {
            PlayerPrefs.SetInt("Hide 1", 1);
        }
        else if (PlayerPrefs.GetInt("Hide 2") != 1 && controller.hidecount >= 500)
        {
            PlayerPrefs.SetInt("Hide 2", 1);
        }
        else if (PlayerPrefs.GetInt("Hide 3") != 1 && controller.hidecount >= 750)
        {
            PlayerPrefs.SetInt("Hide 3", 1);
        }
    }

    public void Eat()
    {
        controller.eating = true;
        controller.eatingSpot = player.transform.position;
    }

    public void QueueHide(Transform queuedObject)
    {
        toHide = queuedObject;
        queueHide = true;
    }

    void QueueEat()
    {
        queueEat = true;
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
