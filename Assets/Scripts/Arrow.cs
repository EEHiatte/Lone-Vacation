using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

    Vector3 m_v3OriginPt;
    public GameObject m_goOwner;
    public Vector3 m_v3TargetPt;
    Vector3 m_v3Position;
    public float m_fArrowSpeed;

	// Use this for initialization
	void Start () {
        m_v3OriginPt = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        m_v3Position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        m_v3Position = transform.position;
        m_v3Position += (m_v3TargetPt - m_v3OriginPt).normalized*m_fArrowSpeed*Time.deltaTime;
        transform.position = m_v3Position;
        if ((m_v3Position - m_v3OriginPt).magnitude > 20)
        {
            GameObject.Destroy(gameObject);
        }
	}
    void OnTriggerEnter(Collider other)
    {
        if( other.tag == "Player")
        {
            other.GetComponent<PlayerStatManager>().DecreaseHealth(m_goOwner.GetComponent<AIBehavior>().m_fAttackDamage);    //  Damage

            ////////KEITH'S CODE
            /// checks if te player's health is less than or equal to zero
            if (other.GetComponent<PlayerStatManager>().playerHealth <= 0)
            {
                /// turns on the cooresponding achievement based on the ai's type
                PlayerPrefs.SetInt("Death 2", 1);
                PlayerPrefs.Save();
            }
            //////////END OF KEITH'S CODE
        }
        else if (other.tag == "Animal")
        {
            other.GetComponent<AIBehavior>().m_fHealth -= m_goOwner.GetComponent<AIBehavior>().m_fAttackDamage;
        }
        else if (other.tag == "Hiding Objects" && other.GetComponent<HidingObjectBehavior>().currentlyHiding == true)
        {
            //Pop out of hiding script here
            if (other.GetComponent<HidingObjectBehavior>().player.GetComponent<PlayerController>().currentlyHidingOrResting)
            {
                other.GetComponent<HidingObjectBehavior>().player.GetComponent<PlayerController>().anObject.gameObject.GetComponent<HidingObjectBehavior>().Hide(other.GetComponent<HidingObjectBehavior>().player.transform);
                other.GetComponent<HidingObjectBehavior>().player.GetComponent<PlayerController>().currentlyHidingOrResting = false;
                other.GetComponent<HidingObjectBehavior>().player.GetComponent<PlayerStatManager>().DecreaseHealth(m_goOwner.GetComponent<AIBehavior>().m_fAttackDamage);
            }
        }
        else if(other.tag == "Cannibal")
        {
            return;
        }
        GameObject.Destroy(gameObject);
    }
}
