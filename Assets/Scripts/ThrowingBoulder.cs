using UnityEngine;
using System.Collections;

public class ThrowingBoulder : MonoBehaviour {
    public Vector3 m_v3OriginPt;
    public GameObject m_goOwner;
    public Vector3 m_v3TargetPt;
    Vector3 m_v3Position;
    public float m_fProjectileSpeed;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        m_v3Position = transform.position;
        m_v3Position += (m_v3TargetPt - m_v3OriginPt).normalized * m_fProjectileSpeed * Time.deltaTime;
        transform.position = m_v3Position;
        if ((m_v3Position - m_v3OriginPt).magnitude > 15)
        {
            AoE();
            GameObject.Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        AoE();
        GameObject.Destroy(gameObject);
    }

    void AoE()
    {
        GetComponent<AudioSource>().spatialBlend = 1;
        GetComponent<AudioSource>().Play();
        Collider[] colliderAround = Physics.OverlapSphere(m_v3Position, 1.5f); // Get all the collider in range.
        for (int i = 0; i < colliderAround.Length; i++)
        {
            if (colliderAround[i].tag == "Player")
            {
                colliderAround[i].GetComponent<PlayerStatManager>().DecreaseHealth(m_goOwner.GetComponent<AIBehavior>().m_fAttackDamage);    //  Damage

                ////////KEITH'S CODE
                /// checks if te player's health is less than or equal to zero
                if (colliderAround[i].GetComponent<PlayerStatManager>().playerHealth <= 0)
                {
                    /// turns on the cooresponding achievement based on the ai's type
                    PlayerPrefs.SetInt("Death 4", 1);
                    PlayerPrefs.Save();
                }
                //////////END OF KEITH'S CODE
            }
            else if (colliderAround[i].tag == "Animal")
            {
                colliderAround[i].GetComponent<AIBehavior>().m_fHealth -= m_goOwner.GetComponent<AIBehavior>().m_fAttackDamage;
            }
            else if (colliderAround[i].tag == "Hiding Objects" && colliderAround[i].GetComponent<HidingObjectBehavior>().currentlyHiding == true)
            {
                //Pop out of hiding script here
                if (colliderAround[i].GetComponent<HidingObjectBehavior>().player.GetComponent<PlayerController>().currentlyHidingOrResting)
                {
                    colliderAround[i].GetComponent<HidingObjectBehavior>().player.GetComponent<PlayerController>().anObject.gameObject.GetComponent<HidingObjectBehavior>().Hide(colliderAround[i].GetComponent<HidingObjectBehavior>().player.transform);
                    colliderAround[i].GetComponent<HidingObjectBehavior>().player.GetComponent<PlayerController>().currentlyHidingOrResting = false;
                    colliderAround[i].GetComponent<HidingObjectBehavior>().player.GetComponent<PlayerStatManager>().DecreaseHealth(m_goOwner.GetComponent<AIBehavior>().m_fAttackDamage);
                }
            }
            else if (colliderAround[i].tag == "Cannibal")
            {
                colliderAround[i].GetComponent<AIBehavior>().m_fHealth -= m_goOwner.GetComponent<AIBehavior>().m_fAttackDamage;
            }
        }
    }
}
