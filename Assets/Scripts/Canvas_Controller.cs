using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Canvas_Controller : MonoBehaviour 
{
    public GameObject Twigs;
    public GameObject Rock;
    public GameObject Leaves;
    public GameObject Thorns;
    public GameObject Rope;

    public GameObject player;

    InventoryManager Inv_script;

    public int max_size;
	// Use this for initialization
	void Start () 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Inv_script = player.GetComponent<InventoryManager>();        
	}
	
	// Update is called once per frame
	void Update () 
    {
        Twigs.GetComponentInChildren<Text>().text = Inv_script.numTwigs + " / " + max_size;
        Rock.GetComponentInChildren<Text>().text = Inv_script.numRocks + " / " + max_size;
        Leaves.GetComponentInChildren<Text>().text = Inv_script.numLeaves + " / " + max_size;
        Thorns.GetComponentInChildren<Text>().text = Inv_script.numThorns + " / " + max_size;
        Rope.GetComponentInChildren<Text>().text = Inv_script.numRope + " / " + max_size;
    }
}
