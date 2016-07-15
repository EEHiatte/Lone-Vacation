using UnityEngine;
using System.Collections;


public class LoadInfo : MonoBehaviour 
{
    public GameObject player;
    public void LoadAllInformation()
    {
        InventoryManager inven = player.GetComponent<InventoryManager>();
        PlayerStatManager stats = player.GetComponent<PlayerStatManager>();

        inven.numTwigs = PlayerPrefs.GetInt("TWIGS");
        inven.numLeaves = PlayerPrefs.GetInt("LEAVES");
        inven.numRocks = PlayerPrefs.GetInt("ROCKS");
        inven.numRope = PlayerPrefs.GetInt("ROPE");
        inven.numThorns = PlayerPrefs.GetInt("THORNS");
        stats.playerHealth = PlayerPrefs.GetFloat("PLAYERHEALTH");
        stats.playerHunger = PlayerPrefs.GetFloat("PLAYERHUNGER");
        stats.playerThirst = PlayerPrefs.GetFloat("PLAYERTHRIST");
        inven.haveCompass = (PlayerPrefs.GetInt("HAVECOMPASS") != 0);
        inven.haveCanteen = (PlayerPrefs.GetInt("HAVECANTEEN") != 0);
        inven.haveBackpack = (PlayerPrefs.GetInt("HAVEBACKPACK") != 0);
        inven.haveBoots = (PlayerPrefs.GetInt("HAVEBOOTS") != 0);
        inven.haveLantern = (PlayerPrefs.GetInt("HAVELANTERN") != 0);
        inven.haveGloves = (PlayerPrefs.GetInt("HAVEGLOVES") != 0);
        GameObject.Find("TextBoxManager").GetComponent<TextBoxManager>().start = (PlayerPrefs.GetInt("STARTBOX") != 0);
        GameObject.Find("TextBoxManager").GetComponent<TextBoxManager>().textBox = (PlayerPrefs.GetInt("TEXTBOX") != 0);
        
    }                            
    
}
