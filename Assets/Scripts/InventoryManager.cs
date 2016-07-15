using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public enum Collectibletype
    {
        Stick = 0,
        Rock,
        Leaf,
        Rope,
        Thorn,
        Boots,
        Gloves,
        Lamp,
        Backpack,
        Canteen,
        TikiPiece,
        TikiTorch,
        HeadPiece,
        CthulhuStatuePiece,
        BarPiece,
        BoardPiece
    };

    public int numTwigs = 0;
    public int numRocks = 0;
    public int numLeaves = 0;
    public int numRope = 0;
    public int numThorns = 0;

    public int maxInvValue = 0;

    public bool haveCompass = false;
    public bool haveCanteen = false;
    public bool haveBackpack = false;
    public bool haveBoots = false;
    public bool haveLantern = false;
    public bool haveGloves = false;

    bool hasBackpack = false;
    public Sprite newBackpackImage;

    public GameObject twigIcon;
    public GameObject rockIcon;
    public GameObject leafIcon;
    public GameObject ropeIcon;
    public GameObject thornIcon;

    float flashTimer = 0.0f;

    Image flashIcon;
    Text flashText;

    Color origTextColor;
    Color origIconColor;

    public List<AudioClip> audios;
    public List<AudioSource> audioSource = new List<AudioSource>();
    //List<bool> successfulPickup = new List<bool>();
    public GameObject itemSpawner;

    public Image[] trapIcons;
    public GameObject trapIconHolder;

    Color iconDimColor;

    // Use this for initialization
    void Start()
    {
        iconDimColor = new Color(0.3f, 0.3f, 0.3f);
        maxInvValue = 20;

        for (int i = 0; i < 2; i++)
        {
            audioSource.Add(gameObject.AddComponent<AudioSource>());
            audioSource[i].clip = audios[i];
        }

        trapIcons = new Image[6];
        int k = 0;
        for (int i = 0; i < 12; i++)
        {
            if (trapIconHolder.GetComponentsInChildren<Image>()[i].GetComponent<Button>() == null)
            {
                trapIcons[k] = trapIconHolder.GetComponentsInChildren<Image>()[i];
                //trapIcons[k].color = iconDimColor;
                k++;
            }
        }
        UpdateIconColors();
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerPrefs.GetInt("No Worries") == 1)
        {
            numTwigs = 20;
            numRocks = 20;
            numLeaves = 20;
            numRope = 20;
            numThorns = 20;
        }
        if (flashTimer > 0.0f)
        {
            if (flashTimer >= 1.5f)
            {
                flashIcon.color = new Color(1.0f, 0.2f, 0.2f);
                flashText.color = new Color(1.0f, 0.2f, 0.2f);
            }
            else if (flashTimer >= 1.0f)
            {
                flashIcon.color = origIconColor;
                flashText.color = origTextColor;
            }
            else if (flashTimer > 0.5f)
            {
                flashIcon.color = new Color(1.0f, 0.2f, 0.2f);
                flashText.color = new Color(1.0f, 0.2f, 0.2f);
            }
            else
            {
                flashIcon.color = origIconColor;
                flashText.color = origTextColor;
            }
            flashTimer -= Time.deltaTime;
        }
        else if (flashIcon != null)
            flashIcon = null;
    }

    public bool IncreaseItem(int itemType, int increaseAmt)
    {
        //for (int i = 0; i < 3; i++)
        //    successfulPickup.Add(false);
        bool successfulPickup = false;
        bool isPowerup = false;
        if (increaseAmt > 0)
        {
            switch (itemType)
            {
                case 0:
                    {
                        if (numTwigs + increaseAmt <= maxInvValue)
                        {
                            if (itemSpawner != null)
                                itemSpawner.GetComponent<ItemSpawner>().stickAmount -= 1;
                            numTwigs += increaseAmt;
                            successfulPickup = true;
                        }
                        else
                        {
                            if (flashIcon != null)
                            {
                                flashIcon.color = origIconColor;
                                flashText.color = origTextColor;
                            }
                            flashIcon = twigIcon.GetComponentInChildren<Image>();
                            origIconColor = flashIcon.color;
                            flashText = twigIcon.GetComponentInChildren<Text>();
                            origTextColor = flashText.color;
                            flashTimer = 2.0f;
                        }
                        break;
                    }
                case 1:
                    {
                        if (numRocks + increaseAmt <= maxInvValue)
                        {
                            if (itemSpawner != null)
                                itemSpawner.GetComponent<ItemSpawner>().rockAmount -= 1;
                            numRocks += increaseAmt;
                            successfulPickup = true;
                        }
                        else
                        {
                            if (flashIcon != null)
                            {
                                flashIcon.color = origIconColor;
                                flashText.color = origTextColor;
                            }
                            flashIcon = rockIcon.GetComponentInChildren<Image>();
                            origIconColor = flashIcon.color;
                            flashText = rockIcon.GetComponentInChildren<Text>();
                            origTextColor = flashText.color;
                            flashTimer = 2.0f;
                        }
                        break;
                    }
                case 2:
                    {
                        if (numLeaves + increaseAmt <= maxInvValue)
                        {
                            if (itemSpawner != null)
                                itemSpawner.GetComponent<ItemSpawner>().leafAmount -= 1;
                            numLeaves += increaseAmt;
                            successfulPickup = true;
                        }
                        else
                        {
                            if (flashIcon != null)
                            {
                                flashIcon.color = origIconColor;
                                flashText.color = origTextColor;
                            }
                            flashIcon = leafIcon.GetComponentInChildren<Image>();
                            origIconColor = flashIcon.color;
                            flashText = leafIcon.GetComponentInChildren<Text>();
                            origTextColor = flashText.color;
                            flashTimer = 2.0f;
                        }
                        break;
                    }
                case 3:
                    {
                        if (numRope + increaseAmt < maxInvValue)
                        {
                            if (itemSpawner != null)
                                itemSpawner.GetComponent<ItemSpawner>().ropeAmount -= 1;
                            numRope += increaseAmt;
                            successfulPickup = true;
                        }
                        else
                        {
                            if (flashIcon != null)
                            {
                                flashIcon.color = origIconColor;
                                flashText.color = origTextColor;
                            }
                            flashIcon = ropeIcon.GetComponentInChildren<Image>();
                            origIconColor = flashIcon.color;
                            flashText = ropeIcon.GetComponentInChildren<Text>();
                            origTextColor = flashText.color;
                            flashTimer = 2.0f;
                        }
                        break;
                    }
                case 4:
                    {
                        if (numThorns + increaseAmt <= maxInvValue)
                        {
                            if (itemSpawner != null)
                                itemSpawner.GetComponent<ItemSpawner>().thornAmount -= 1;
                            numThorns += increaseAmt;
                            successfulPickup = true;
                        }
                        else
                        {
                            if (flashIcon != null)
                            {
                                flashIcon.color = origIconColor;
                                flashText.color = origTextColor;
                            }
                            flashIcon = thornIcon.GetComponentInChildren<Image>();
                            origIconColor = flashIcon.color;
                            flashText = thornIcon.GetComponentInChildren<Text>();
                            origTextColor = flashText.color;
                            flashTimer = 2.0f;
                        }
                        break;
                    }
            }
        }
        else if (PlayerPrefs.GetInt("Difficulty") == 1)
        {
            switch (itemType)
            {
                case 0:
                    {
                        if (numTwigs < maxInvValue)
                        {
                            if (itemSpawner != null)
                                itemSpawner.GetComponent<ItemSpawner>().stickAmount -= 1;
                            if (numTwigs + 2 > maxInvValue)
                                numTwigs += 1;
                            else
                                numTwigs += 2;
                            successfulPickup = true;
                        }
                        else
                        {
                            if (flashIcon != null)
                            {
                                flashIcon.color = origIconColor;
                                flashText.color = origTextColor;
                            }
                            flashIcon = twigIcon.GetComponentInChildren<Image>();
                            origIconColor = flashIcon.color;
                            flashText = twigIcon.GetComponentInChildren<Text>();
                            origTextColor = flashText.color;
                            flashTimer = 2.0f;
                        }
                        break;
                    }
                case 1:
                    {
                        if (numRocks < maxInvValue)
                        {
                            if (itemSpawner != null)
                                itemSpawner.GetComponent<ItemSpawner>().rockAmount -= 1;
                            if (numRocks + 2 > maxInvValue)
                                numRocks += 1;
                            else
                                numRocks += 2;
                            successfulPickup = true;
                        }
                        else
                        {
                            if (flashIcon != null)
                            {
                                flashIcon.color = origIconColor;
                                flashText.color = origTextColor;
                            }
                            flashIcon = rockIcon.GetComponentInChildren<Image>();
                            origIconColor = flashIcon.color;
                            flashText = rockIcon.GetComponentInChildren<Text>();
                            origTextColor = flashText.color;
                            flashTimer = 2.0f;
                        }
                        break;
                    }
                case 2:
                    {
                        if (numLeaves < maxInvValue)
                        {
                            if (itemSpawner != null)
                                itemSpawner.GetComponent<ItemSpawner>().leafAmount -= 1;
                            if (numLeaves + 2 > maxInvValue)
                                numLeaves += 1;
                            else
                                numLeaves += 2;
                            successfulPickup = true;
                        }
                        else
                        {
                            if (flashIcon != null)
                            {
                                flashIcon.color = origIconColor;
                                flashText.color = origTextColor;
                            }
                            flashIcon = leafIcon.GetComponentInChildren<Image>();
                            origIconColor = flashIcon.color;
                            flashText = leafIcon.GetComponentInChildren<Text>();
                            origTextColor = flashText.color;
                            flashTimer = 2.0f;
                        }
                        break;
                    }
                case 3:
                    {
                        if (numRope < maxInvValue)
                        {
                            if (itemSpawner != null)
                                itemSpawner.GetComponent<ItemSpawner>().ropeAmount -= 1;
                            if (numRope + 2 > maxInvValue)
                                numRope += 1;
                            else
                                numRope += 2;
                            successfulPickup = true;
                        }
                        else
                        {
                            if (flashIcon != null)
                            {
                                flashIcon.color = origIconColor;
                                flashText.color = origTextColor;
                            }
                            flashIcon = ropeIcon.GetComponentInChildren<Image>();
                            origIconColor = flashIcon.color;
                            flashText = ropeIcon.GetComponentInChildren<Text>();
                            origTextColor = flashText.color;
                            flashTimer = 2.0f;
                        }
                        break;
                    }
                case 4:
                    {
                        if (numThorns < maxInvValue)
                        {
                            if (itemSpawner != null)
                                itemSpawner.GetComponent<ItemSpawner>().thornAmount -= 1;
                            if (numThorns + 2 > maxInvValue)
                                numThorns += 1;
                            else
                                numThorns += 2;
                            successfulPickup = true;
                        }
                        else
                        {
                            if (flashIcon != null)
                            {
                                flashIcon.color = origIconColor;
                                flashText.color = origTextColor;
                            }
                            flashIcon = thornIcon.GetComponentInChildren<Image>();
                            origIconColor = flashIcon.color;
                            flashText = thornIcon.GetComponentInChildren<Text>();
                            origTextColor = flashText.color;
                            flashTimer = 2.0f;
                        }
                        break;
                    }
                case 5:
                    {
                        // BOOTS
                        haveBoots = true;
                        BroadcastMessage("CheckPowerUps", haveBoots, SendMessageOptions.DontRequireReceiver);
                        successfulPickup = true;
                        isPowerup = true;
                        break;
                    }
                case 6:
                    {
                        // GLOVES
                        haveGloves = true;
                        BroadcastMessage("CheckPowerUps", haveGloves, SendMessageOptions.DontRequireReceiver);
                        successfulPickup = true;
                        isPowerup = true;
                        break;
                    }
                case 7:
                    {
                        // LAMP
                        haveLantern = true;
                        BroadcastMessage("CheckPowerUps", haveLantern, SendMessageOptions.DontRequireReceiver);
                        successfulPickup = true;
                        isPowerup = true;
                        break;
                    }
                case 8:
                    {
                        // BACKPACK
                        haveBackpack = true;
                        BroadcastMessage("CheckPowerUps", haveBackpack, SendMessageOptions.DontRequireReceiver);
                        successfulPickup = true;
                        isPowerup = true;
                        break;
                    }
                case 9:
                    {
                        // CANTEEN
                        haveCanteen = true;
                        BroadcastMessage("CheckPowerUps", haveCanteen, SendMessageOptions.DontRequireReceiver);
                        successfulPickup = true;
                        isPowerup = true;
                        break;
                    }
                default:
                    {
                        Debug.Log("Attempt to increase item not possible.");
                        break;
                    }
            }
        }
        else
        {
            switch (itemType)
            {
                case 0:
                    {
                        if (numTwigs < maxInvValue)
                        {
                            if (itemSpawner != null)
                                itemSpawner.GetComponent<ItemSpawner>().stickAmount -= 1;
                            numTwigs += 1;
                            successfulPickup = true;
                        }
                        else
                        {
                            if (flashIcon != null)
                            {
                                flashIcon.color = origIconColor;
                                flashText.color = origTextColor;
                            }
                            flashIcon = twigIcon.GetComponentInChildren<Image>();
                            origIconColor = flashIcon.color;
                            flashText = twigIcon.GetComponentInChildren<Text>();
                            origTextColor = flashText.color;
                            flashTimer = 2.0f;
                        }
                        break;
                    }
                case 1:
                    {
                        if (numRocks < maxInvValue)
                        {
                            if (itemSpawner != null)
                                itemSpawner.GetComponent<ItemSpawner>().rockAmount -= 1;
                            numRocks += 1;
                            successfulPickup = true;
                        }
                        else
                        {
                            if (flashIcon != null)
                            {
                                flashIcon.color = origIconColor;
                                flashText.color = origTextColor;
                            }
                            flashIcon = rockIcon.GetComponentInChildren<Image>();
                            origIconColor = flashIcon.color;
                            flashText = rockIcon.GetComponentInChildren<Text>();
                            origTextColor = flashText.color;
                            flashTimer = 2.0f;
                        }
                        break;
                    }
                case 2:
                    {
                        if (numLeaves < maxInvValue)
                        {
                            if (itemSpawner != null)
                                itemSpawner.GetComponent<ItemSpawner>().leafAmount -= 1;
                            numLeaves += 1;
                            successfulPickup = true;
                        }
                        else
                        {
                            if (flashIcon != null)
                            {
                                flashIcon.color = origIconColor;
                                flashText.color = origTextColor;
                            }
                            flashIcon = leafIcon.GetComponentInChildren<Image>();
                            origIconColor = flashIcon.color;
                            flashText = leafIcon.GetComponentInChildren<Text>();
                            origTextColor = flashText.color;
                            flashTimer = 2.0f;
                        }
                        break;
                    }
                case 3:
                    {
                        if (numRope < maxInvValue)
                        {
                            if (itemSpawner != null)
                                itemSpawner.GetComponent<ItemSpawner>().ropeAmount -= 1;
                            numRope += 1;
                            successfulPickup = true;
                        }
                        else
                        {
                            if (flashIcon != null)
                            {
                                flashIcon.color = origIconColor;
                                flashText.color = origTextColor;
                            }
                            flashIcon = ropeIcon.GetComponentInChildren<Image>();
                            origIconColor = flashIcon.color;
                            flashText = ropeIcon.GetComponentInChildren<Text>();
                            origTextColor = flashText.color;
                            flashTimer = 2.0f;
                        }
                        break;
                    }
                case 4:
                    {
                        if (numThorns < maxInvValue)
                        {
                            if (itemSpawner != null)
                                itemSpawner.GetComponent<ItemSpawner>().thornAmount -= 1;
                            numThorns += 1;
                            successfulPickup = true;
                        }
                        else
                        {
                            if (flashIcon != null)
                            {
                                flashIcon.color = origIconColor;
                                flashText.color = origTextColor;
                            }
                            flashIcon = thornIcon.GetComponentInChildren<Image>();
                            origIconColor = flashIcon.color;
                            flashText = thornIcon.GetComponentInChildren<Text>();
                            origTextColor = flashText.color;
                            flashTimer = 2.0f;
                        }
                        break;
                    }
                case 5:
                    {
                        // BOOTS
                        haveBoots = true;
                        BroadcastMessage("CheckPowerUps", haveBoots, SendMessageOptions.DontRequireReceiver);
                        successfulPickup = true;
                        isPowerup = true;
                        break;
                    }
                case 6:
                    {
                        // GLOVES
                        haveGloves = true;
                        BroadcastMessage("CheckPowerUps", haveGloves, SendMessageOptions.DontRequireReceiver);
                        successfulPickup = true;
                        isPowerup = true;
                        break;
                    }
                case 7:
                    {
                        // LAMP
                        haveLantern = true;
                        BroadcastMessage("CheckPowerUps", haveLantern, SendMessageOptions.DontRequireReceiver);
                        successfulPickup = true;
                        break;
                    }
                case 8:
                    {
                        // BACKPACK
                        haveBackpack = true;
                        BroadcastMessage("CheckPowerUps", haveBackpack, SendMessageOptions.DontRequireReceiver);
                        successfulPickup = true;
                        isPowerup = true;
                        break;
                    }
                case 9:
                    {
                        // CANTEEN
                        haveCanteen = true;
                        BroadcastMessage("CheckPowerUps", haveCanteen, SendMessageOptions.DontRequireReceiver);
                        successfulPickup = true;
                        isPowerup = true;
                        break;
                    }
                default:
                    {
                        Debug.Log("Attempt to increase item not possible.");
                        break;
                    }
            }
        }

        if (!isPowerup && successfulPickup)
        {
            audioSource[0].Play();
            if (Application.loadedLevel != (int)GameManager.LevelID.TutorialLevel)
                itemSpawner.GetComponent<ItemSpawner>().CheckToSpawn();
            UpdateIconColors();
            return successfulPickup;
        }
        else if (isPowerup && successfulPickup)
        {
            audioSource[1].Play();
            return successfulPickup;
        }

        return successfulPickup;
    }

    public bool DecreaseItem(int itemType)
    {
        switch (itemType)
        {
            case 0:
                {
                    if (numTwigs <= maxInvValue && numTwigs > 0)
                    {
                        numTwigs -= 1;
                        return true;
                    }
                    else
                    {
                        //Put red flash code here
                    }
                    break;
                }
            case 1:
                {
                    if (numRocks <= maxInvValue && numRocks > 0)
                    {
                        numRocks -= 1;
                        return true;
                    }
                    else
                    {
                        //Put red flash code here
                    }
                    break;
                }
            case 2:
                {
                    if (numLeaves <= maxInvValue && numLeaves > 0)
                    {
                        numLeaves -= 1;
                        return true;
                    }
                    else
                    {
                        //Put red flash code here
                    }
                    break;
                }
            case 3:
                {
                    if (numRope <= maxInvValue && numRope > 0)
                    {
                        numRope -= 1;
                        return true;
                    }
                    else
                    {
                        //Put red flash code here
                    }
                    break;
                }
            case 4:
                {
                    if (numThorns <= maxInvValue && numThorns > 0)
                    {
                        numThorns -= 1;
                        return true;
                    }
                    else
                    {
                        //Put red flash code here
                    }
                    break;
                }
            default:
                {
                    Debug.Log("Attempt to increase item not possible.");
                    break;
                }
        }
        return false;
    }

    public bool PlaceTrap(TrapBehavior.TrapType trapType)
    {
        switch (trapType)
        {
            case TrapBehavior.TrapType.ThornField:
                {
                    if (numThorns >= 2 && numRocks >= 1)
                    {
                        numThorns -= 2;
                        numRocks -= 1;
                        return true;
                    }
                    break;
                }
            case TrapBehavior.TrapType.GopherHole:
                {
                    if (numTwigs >= 2 && numLeaves >= 1)
                    {
                        numTwigs -= 2;
                        numLeaves -= 1;
                        return true;
                    }
                    break;
                }
            case TrapBehavior.TrapType.TreeTrap:
                {
                    if (numRope >= 2 && numTwigs >= 2 && numLeaves >= 1)
                    {
                        numRope -= 2;
                        numTwigs -= 2;
                        numLeaves -= 1;
                        return true;
                    }
                    break;
                }
            case TrapBehavior.TrapType.TripTrap:
                {
                    if (numRope >= 1 && numTwigs >= 2 && numLeaves >= 2)
                    {
                        numRope -= 1;
                        numTwigs -= 2;
                        numLeaves -= 2;
                        return true;
                    }
                    break;
                }
            case TrapBehavior.TrapType.Pitfall:
                {
                    if (numTwigs >= 3 && numLeaves >= 2 && numThorns >= 2)
                    {
                        numTwigs -= 3;
                        numLeaves -= 2;
                        numThorns -= 2;
                        return true;
                    }
                    break;
                }
            case TrapBehavior.TrapType.RockTripTrap:
                {
                    if (numRope >= 1 && numTwigs >= 2 && numLeaves >= 2 && numRocks >= 2)
                    {
                        numRope -= 1;
                        numTwigs -= 2;
                        numLeaves -= 2;
                        numRocks -= 2;
                        return true;
                    }
                    break;
                }
        }
        return false;
    }
    public void CheckPowerUps()
    {
        if (haveBackpack == true && hasBackpack == false)
        {
            maxInvValue = 40;
            // TODO: Fix this to have the ui update with the value
            Canvas_Controller uiInventoryController = null;
            if (GameObject.Find("UI") != null)
                uiInventoryController = GameObject.Find("UI").GetComponent<Canvas_Controller>();
            if (uiInventoryController == null)
            {
                if (GameObject.Find("UI 2") != null)
                    uiInventoryController = GameObject.Find("UI 2").GetComponent<Canvas_Controller>();
            }
            uiInventoryController.max_size = maxInvValue;
            GameObject.Find("BackpackImageUI").GetComponent<Image>().sprite = newBackpackImage;
            hasBackpack = true;
        }
    }

    void UpdateIconColors()
    {
        if (numTwigs >= 2 && numLeaves >= 1)
            trapIcons[0].color = new Color(1.0f, 1.0f, 1.0f);
        else
            trapIcons[0].color = iconDimColor;

        if (numRope >= 1 && numTwigs >= 2 && numLeaves >= 2)
            trapIcons[1].color = new Color(1.0f, 1.0f, 1.0f);
        else
            trapIcons[1].color = iconDimColor;

        if (numRope >= 1 && numTwigs >= 2 && numLeaves >= 2 && numRocks >= 2)
            trapIcons[2].color = new Color(1.0f, 1.0f, 1.0f);
        else
            trapIcons[2].color = iconDimColor;

        if (numThorns >= 2 && numRocks >= 1)
            trapIcons[3].color = new Color(1.0f, 1.0f, 1.0f);
        else
            trapIcons[3].color = iconDimColor;

        if (numRope >= 2 && numTwigs >= 2 && numLeaves >= 1)
            trapIcons[4].color = new Color(1.0f, 1.0f, 1.0f);
        else
            trapIcons[4].color = iconDimColor;

        if (numTwigs >= 3 && numLeaves >= 2 && numThorns >= 2)
            trapIcons[5].color = new Color(1.0f, 1.0f, 1.0f);
        else
            trapIcons[5].color = iconDimColor;
    }
}
