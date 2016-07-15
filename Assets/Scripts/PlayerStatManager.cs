using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStatManager : MonoBehaviour
{
    public float playerHealth;
    public float playerHunger;
    public float playerThirst;
    public float hungerDecayRate;
    public float thirstDecayRate;
    public float hungerIncreaseRate;
    public float thirstIncreaseRate;
    public float healthDecreaseRate;
    
    public bool drinking = false;

    public bool hungerBubbleActive = false;
    public bool thirstBubbleActive = false;

    bool healthChange = false;

    //int thirstBubble = 0;
    //int hungerBubble = 3;

    InventoryManager invManager;
    GameObject Healthbar;
    GameObject ThirstBarobj;
    GameObject ThirstBar;
    GameObject HungerBarobj;
    GameObject HungerBar;

    Image painBorder;
    Color currPainBorderColor;
    public float painBorderTimer = 0.0f;

    GameObject thoughtBubbles;

    public AudioSource grunt;

    public GameObject LastAttacker;

    // Use this for initialization
    void Start()
    {
        Healthbar = GameObject.Find("HealthBar");
        HungerBar = GameObject.Find("HungerBar");
        ThirstBar = GameObject.Find("ThirstBar");
        ThirstBarobj = GameObject.Find("ThirstBarobj");
        HungerBarobj = GameObject.Find("HungerBarobj");

        invManager = GetComponent<InventoryManager>();
        painBorder = GameObject.FindGameObjectWithTag("Pain Border").GetComponent<Image>();
        currPainBorderColor = painBorder.color;

        thoughtBubbles = GameObject.FindGameObjectWithTag("ThoughtBubbles");

        grunt = GetComponents<AudioSource>()[2];

        //check for no thirst cheat
        if (PlayerPrefs.GetInt("Thirstay") == 1)
        {
            thirstDecayRate = 0;
        }
        //////////////////////////

        /// check for no hunger cheat
        if (PlayerPrefs.GetInt("Hungary") == 1)
        {
            hungerDecayRate = 0;
        }
        ///////////

        //// check for god mode cheat
        if (PlayerPrefs.GetInt("God Mode") == 1)
        {
            playerHealth = 10000;
            hungerDecayRate = 0;
            thirstDecayRate = 0;
        }
        //////////
    }

    void Awake()
    {
        playerHealth = 100;
        playerHunger = 99;
        playerThirst = 99;
        /// checks for difficulty
        if (PlayerPrefs.GetInt("Difficulty") == 1)
        {
            hungerDecayRate = .06125f * .5f;
            thirstDecayRate = .125f * .5f;
            hungerIncreaseRate = 1.66f * 2;
            thirstIncreaseRate = 1.66f * 2;
            healthDecreaseRate = .5f * .5f;
        }
        else if (PlayerPrefs.GetInt("Difficulty") == 2)
        {
            hungerDecayRate = .06125f;
            thirstDecayRate = .125f;
            hungerIncreaseRate = 1.66f;
            thirstIncreaseRate = 1.66f;
            healthDecreaseRate = .5f;
        }
        else if (PlayerPrefs.GetInt("Difficulty") == 3)
        {
            hungerDecayRate = .06125f * 2f;
            thirstDecayRate = .125f * 2f;
            hungerIncreaseRate = 1.66f * .5f;
            thirstIncreaseRate = 1.66f * .5f;
            healthDecreaseRate = .5f * 2f;
        }
        else if (PlayerPrefs.GetInt("Difficulty") == 4)
        {
            hungerDecayRate = .06125f * 4f;
            thirstDecayRate = .125f * 4f;
            hungerIncreaseRate = 1.66f * .25f;
            thirstIncreaseRate = 1.66f * .25f;
            healthDecreaseRate = .5f * 4f;
        }
        else
        {
            hungerDecayRate = .06125f;
            thirstDecayRate = .125f;
            hungerIncreaseRate = 1.66f;
            thirstIncreaseRate = 1.66f;
            healthDecreaseRate = .5f;
        }
        ///////////////

    }

    // Update is called once per frame
    void Update()
    {
        // checks for god mode cheat!
        if (PlayerPrefs.GetInt("God Mode") == 1)
        {
            playerHealth = 10000;
        }
        ///////
        ThirstBarCode();
        HungerBarCode();


        if (playerHunger > 0.0f)
            playerHunger -= hungerDecayRate * Time.deltaTime;
        else
        {
            playerHunger = 0.0f;
            thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().TurnBubbleRed((int)ThoughtBubbleBehavior.BubbleTypes.HungerBubble);
            DecreaseHealth(healthDecreaseRate);
        }


        if (drinking)
        {
            if (playerThirst <= 0.0f)
                thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().TurnBubbleNormal((int)ThoughtBubbleBehavior.BubbleTypes.ThirstBubble);

            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

            if (playerThirst < 100.0f && move.magnitude == 0.0f)
            {
                playerThirst += thirstIncreaseRate;
                if (playerThirst > 100.0f)
                {
                    playerThirst = 100.0f;
                }
            }
            else
                drinking = false;
        }
        else
        {
            if (playerThirst > 0.0f)
            {
                playerThirst -= thirstDecayRate * Time.deltaTime;
            }
            else
            {
                playerThirst = 0.0f;
                thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().TurnBubbleRed((int)ThoughtBubbleBehavior.BubbleTypes.ThirstBubble);
                DecreaseHealth(healthDecreaseRate);
            }
        }

        if (playerThirst <= 35 && !thirstBubbleActive)
            thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().ActivateControlBubble((int)ThoughtBubbleBehavior.BubbleTypes.ThirstBubble);
        else if (playerThirst >= 99.5f && !thirstBubbleActive)
        {
            thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().TurnBubbleGreen((int)ThoughtBubbleBehavior.BubbleTypes.ThirstBubble);
            thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().ActivateControlBubble((int)ThoughtBubbleBehavior.BubbleTypes.ThirstBubble);
        }
        else if (thirstBubbleActive && playerThirst > 35 && playerThirst < 99.5)
        {
            Debug.Log("Turn thirst off");
            thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().DeactivateControlBubble((int)ThoughtBubbleBehavior.BubbleTypes.ThirstBubble);
            thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().TurnBubbleNormal((int)ThoughtBubbleBehavior.BubbleTypes.ThirstBubble);
        }

        if (playerHunger <= 35 && !hungerBubbleActive)
            thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().ActivateControlBubble((int)ThoughtBubbleBehavior.BubbleTypes.HungerBubble);
        else if (playerHunger >= 99.5f && !hungerBubbleActive)
        {
            thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().TurnBubbleGreen((int)ThoughtBubbleBehavior.BubbleTypes.HungerBubble);
            thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().ActivateControlBubble((int)ThoughtBubbleBehavior.BubbleTypes.HungerBubble);
        }
        else if (hungerBubbleActive && playerHunger > 35 && playerHunger < 99.5)
        {
            Debug.Log("Turn hunger off");
            thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().DeactivateControlBubble((int)ThoughtBubbleBehavior.BubbleTypes.HungerBubble);
            thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().TurnBubbleNormal((int)ThoughtBubbleBehavior.BubbleTypes.HungerBubble);
        }

        PainBorderUpdate();
        if (healthChange)
            BarChanges();
    }

    void BarChanges()
    {
        if (PlayerPrefs.GetInt("God Mode") == 1)
            Healthbar.GetComponent<RectTransform>().localScale = new Vector3(playerHealth / 10000, 1, 1);
        else
            Healthbar.GetComponent<RectTransform>().localScale = new Vector3(playerHealth / 100, 1, 1);
    }

    void CheckStats()
    {

    }

    public void IncreaseHunger(float amount)
    {
        if (playerHunger <= 0.0f)
            thoughtBubbles.GetComponent<ThoughtBubbleBehavior>().TurnBubbleNormal((int)ThoughtBubbleBehavior.BubbleTypes.HungerBubble);

        if (playerHunger < 100)
        {

            playerHunger += amount;
            if (playerHunger > 100)
            {
                playerHunger = 100;
            }
            healthChange = true;
        }
    }

    public void IncreaseHealth(float amount)
    {
        if (playerHealth < 100)
        {
            playerHealth += amount * Time.deltaTime;
            if (playerHealth > 100)
            {
                playerHealth = 100;
            }
            healthChange = true;
        }
    }

    public void DecreaseHealth(float amount)
    {
        if (playerHealth > 0)
        {
            playerHealth -= amount;
            if (grunt.isPlaying == false)
            {
                grunt.Play();
            }
            if (playerHealth < 0)
            {
                playerHealth = 0;
            }
        }
    }

    public void PainBorderUpdate()
    {
        if (playerHealth <= 35)
        {
            painBorder.enabled = true;
            if (painBorderTimer == 0.0f && playerHealth <= 35 && playerHealth > 30)
            {
                painBorder.color = new Color(painBorder.color.r, painBorder.color.b, painBorder.color.g, 0.1f);
                currPainBorderColor = painBorder.color;
            }
            else if (painBorderTimer == 0.0f && playerHealth <= 30 && playerHealth > 25)
            {
                painBorder.color = new Color(painBorder.color.r, painBorder.color.b, painBorder.color.g, 0.2f);
                currPainBorderColor = painBorder.color;
            }
            else if (painBorderTimer == 0.0f && playerHealth <= 25 && playerHealth > 20)
            {
                painBorder.color = new Color(painBorder.color.r, painBorder.color.b, painBorder.color.g, 0.3f);
                currPainBorderColor = painBorder.color;
            }
            else if (painBorderTimer == 0.0f && playerHealth <= 20 && playerHealth > 15)
            {
                painBorder.color = new Color(painBorder.color.r, painBorder.color.b, painBorder.color.g, 0.4f);
                currPainBorderColor = painBorder.color;
            }
            else if (painBorderTimer == 0.0f && playerHealth <= 15 && playerHealth > 10)
            {
                painBorder.color = new Color(painBorder.color.r, painBorder.color.b, painBorder.color.g, 0.5f);
                currPainBorderColor = painBorder.color;
            }
            else if (painBorderTimer == 0.0f && playerHealth <= 10 && playerHealth > 5)
            {
                painBorder.color = new Color(painBorder.color.r, painBorder.color.b, painBorder.color.g, 0.6f);
                currPainBorderColor = painBorder.color;
            }
            else if (painBorderTimer == 0.0f && playerHealth <= 5 && playerHealth > 0)
            {
                painBorder.color = new Color(painBorder.color.r, painBorder.color.b, painBorder.color.g, 0.7f);
                currPainBorderColor = painBorder.color;
            }

            painBorderTimer += Time.deltaTime;
            painBorder.color = new Color(currPainBorderColor.r, currPainBorderColor.g, currPainBorderColor.b, currPainBorderColor.a - (painBorderTimer / 5));
            if (painBorderTimer >= 1.5f)
                painBorderTimer = 0.0f;
        }
        else if (painBorder.color.a != 0.0f)
        {
            painBorder.color = new Color(painBorder.color.r, painBorder.color.b, painBorder.color.b, 0.0f);
            painBorder.enabled = false;
        }
    }

    public void CheckPowerUps(bool CanteenCondition)
    {
        if (invManager.haveCanteen)
            thirstDecayRate = .0025f;
    }

    void ThirstBarCode()
    {
        if (playerThirst < 100 && drinking == true)
        {
            ThirstBarobj.GetComponent<ToggleActive>().SetActiveState();

            if (ThirstBar == null)
            {
                ThirstBar = GameObject.Find("ThirstBar");
            }

            ThirstBar.transform.localScale = new Vector3(playerThirst / 100f, 1f, 1f);
        }
        else if (playerThirst <= 100)
        {
            ThirstBarobj.GetComponent<ToggleActive>().SetInactiveState();
        }
    }
    void HungerBarCode()
    {
        if (playerHunger < 100 && gameObject.GetComponent<PlayerController>().eating == true)
        {
            HungerBarobj.GetComponent<ToggleActive>().SetActiveState();
            if (HungerBar == null)
            {
                HungerBar = GameObject.Find("HungerBar");
            }
            HungerBar.transform.localScale = new Vector3(playerHunger / 100f, 1f, 1f);
        }
        else if (playerHunger <= 100)
        {
            HungerBarobj.GetComponent<ToggleActive>().SetInactiveState();
        }
    }
}