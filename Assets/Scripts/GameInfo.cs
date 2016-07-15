using UnityEngine;
using System.Collections;

public class GameInfo : MonoBehaviour 
{
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public static int numTwigs{get;set;}
    public static int numRocks{get;set;} 
    public static int numLeaves{get;set;}
    public static int numRope{get;set;}  
    public static int numThorns{get;set;}
    public static float playerHealth{get;set;}
    public static float playerHunger{get;set;}
    public static float playerThirst{get;set;}
    public static bool haveCompass{get;set;} 
    public static bool haveCanteen{get;set;} 
    public static bool haveBackpack{get;set;}
    public static bool haveBoots{get;set;} 
    public static bool haveLantern{get;set;}
    public static bool haveGloves{get;set;} 
}
