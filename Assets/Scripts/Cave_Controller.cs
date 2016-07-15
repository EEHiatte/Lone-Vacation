using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Cave_Controller : MonoBehaviour
{

    public GameObject Player;
    public GameObject CaveEntranceSW;
    public GameObject CaveExitSW;
    public GameObject CaveExitNW;
    public GameObject CaveEntranceNW;
    public GameObject CaveExitN;
    public GameObject CaveEntranceN;
    public GameObject CaveExitNE;
    public GameObject CaveEntranceNE;
    public GameObject CaveEntranceS;
    public GameObject CaveExitS;
    public float offset;
    GameObject Sun;
    GameObject Moon;
    // Use this for initialization
    void Start()
    {
        CaveEntranceSW = GameObject.Find("CaveEntranceSW");
        CaveExitSW = GameObject.Find("CaveExitSW");
        CaveExitNW = GameObject.Find("CaveExitNW");
        CaveEntranceNW = GameObject.Find("CaveEntranceNW");
        CaveExitN = GameObject.Find("CaveExitN");
        CaveEntranceN = GameObject.Find("CaveEntranceN");
        CaveExitNE = GameObject.Find("CaveExitNE");
        CaveEntranceNE = GameObject.Find("CaveEntranceNE");
        CaveEntranceS = GameObject.Find("CaveEntranceS");
        CaveExitS = GameObject.Find("CaveExitS");
        offset = 9;
        Sun = GameObject.Find("Sun");
        Moon = GameObject.Find("Moon");

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider col)
    {
        

       

        Player.GetComponent<NavMeshAgent>().enabled = false;
        if (gameObject.name == "CaveEntranceSW" && col.tag == "Player")
        {
            Player.transform.position = new Vector3(CaveExitSW.transform.position.x, CaveExitSW.transform.position.y, CaveExitSW.transform.position.z - offset);
            Moon.GetComponent<Light>().intensity = 0;
            Sun.GetComponent<Light>().intensity = 0;
        }
        else if (gameObject.name == "CaveExitSW" && col.tag == "Player")
        {
            Player.transform.position = new Vector3(CaveEntranceSW.transform.position.x, CaveEntranceSW.transform.position.y, CaveEntranceSW.transform.position.z - offset);
            Moon.GetComponent<Light>().intensity = 1;
            Sun.GetComponent<Light>().intensity = .6f;
        }
        else if (gameObject.name == "CaveEntranceNW" && col.tag == "Player")
        {
            Player.transform.position = new Vector3(CaveExitNW.transform.position.x, CaveExitNW.transform.position.y, CaveExitNW.transform.position.z - offset);
            Moon.GetComponent<Light>().intensity = 0;
            Sun.GetComponent<Light>().intensity = 0;
        }
        else if (gameObject.name == "CaveExitNW" && col.tag == "Player")
        {
            Player.transform.position = new Vector3(CaveEntranceNW.transform.position.x, CaveEntranceNW.transform.position.y, CaveEntranceNW.transform.position.z - offset);
            Moon.GetComponent<Light>().intensity = 1;
            Sun.GetComponent<Light>().intensity = .6f;
        }
        else if (gameObject.name == "CaveEntranceN" && col.tag == "Player")
        {
            Player.transform.position = new Vector3(CaveExitN.transform.position.x, CaveExitN.transform.position.y, CaveExitN.transform.position.z - offset);
            Moon.GetComponent<Light>().intensity = 0;
            Sun.GetComponent<Light>().intensity = 0;
        }
        else if (gameObject.name == "CaveExitN" && col.tag == "Player")
        {
            Player.transform.position = new Vector3(CaveEntranceN.transform.position.x, CaveEntranceN.transform.position.y, CaveEntranceN.transform.position.z - offset);
            Moon.GetComponent<Light>().intensity = 1;
            Sun.GetComponent<Light>().intensity = .6f;
        }
        else if (gameObject.name == "CaveEntranceNE" && col.tag == "Player")
        {
            Player.transform.position = new Vector3(CaveExitNE.transform.position.x, CaveExitNE.transform.position.y, CaveExitNE.transform.position.z - offset);
            Moon.GetComponent<Light>().intensity = 0;
            Sun.GetComponent<Light>().intensity = 0;
        }
        else if (gameObject.name == "CaveExitNE" && col.tag == "Player")
        {
            Player.transform.position = new Vector3(CaveEntranceNE.transform.position.x, CaveEntranceNE.transform.position.y, CaveEntranceNE.transform.position.z - offset);
            Moon.GetComponent<Light>().intensity = 1;
            Sun.GetComponent<Light>().intensity = .6f;
        }
        else if (gameObject.name == "CaveEntranceS" && col.tag == "Player")
        {
            Player.transform.position = new Vector3(CaveExitS.transform.position.x, CaveExitS.transform.position.y, CaveExitS.transform.position.z - offset);
            Moon.GetComponent<Light>().intensity = 0;
            Sun.GetComponent<Light>().intensity = 0;
        }
        else if (gameObject.name == "CaveExitS" && col.tag == "Player")
        {
            Player.transform.position = new Vector3(CaveEntranceS.transform.position.x, CaveEntranceS.transform.position.y, CaveEntranceS.transform.position.z - offset);
            Moon.GetComponent<Light>().intensity = 1;
            Sun.GetComponent<Light>().intensity = .6f;
        }
        Player.GetComponent<NavMeshAgent>().enabled = true;
        
    }
}
