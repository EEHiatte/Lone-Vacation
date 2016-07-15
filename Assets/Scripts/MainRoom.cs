using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainRoom : MonoBehaviour 
{
    public List<GameObject> doors;
    public List<bool> OpenDoors;
    public Material door;

	// Use this for initialization
    void Start()
    {
        
    }
	
	// Update is called once per frame
	void Update () 
    {
        Material mat = new Material(Shader.Find("Transparent/Diffuse"));
        mat.color = new Color(0.25f, 0.25f, 0.25f);

        for (int i = 0; i < doors.Capacity; i++)
            doors[i].GetComponentInChildren<Renderer>().material = door;

        if (doors[1].tag == "Door2" && !OpenDoors[1])
            doors[1].GetComponentInChildren<Renderer>().material = mat;
        if (doors[2].tag == "Door3" && !OpenDoors[2])
            doors[2].GetComponentInChildren<Renderer>().material = mat;
        if (doors[3].tag == "Door4" && !OpenDoors[3])
            doors[3].GetComponentInChildren<Renderer>().material = mat;
        if (doors[4].tag == "Door5" && !OpenDoors[4])
            doors[4].GetComponentInChildren<Renderer>().material = mat;
        if (doors[5].tag == "Door6" && !OpenDoors[5])
            doors[5].GetComponentInChildren<Renderer>().material = mat;
        if (doors[6].tag == "Door7" && !OpenDoors[6])
            doors[6].GetComponentInChildren<Renderer>().material = mat;
        if (doors[7].tag == "Door8" && !OpenDoors[7])
            doors[7].GetComponentInChildren<Renderer>().material = mat;
        //if (doors[8].tag == "Door9" && !OpenDoors[8])
        //    doors[8].GetComponentInChildren<Renderer>().material = mat;
        
	}
}
