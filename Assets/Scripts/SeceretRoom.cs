using UnityEngine;
using System.Collections;

public class SeceretRoom : MonoBehaviour 
{
    public Vector3 orgSpot;
    void OnTriggerEnter(Collider coli)
    {
        if (coli.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").transform.position = orgSpot;
            GameObject.FindGameObjectWithTag("Player").GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
