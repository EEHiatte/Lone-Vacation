using UnityEngine;
using System.Collections;
public class TerrainBehavior : MonoBehaviour 
{
    public int type = 0;

    float scrollspeed = 0.2f;
    float offset;
	// Use this for initialization
	void Start () 
    {
        gameObject.layer = LayerMask.NameToLayer("TerrainEffect");
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (type == 4)
        {
            offset += (Time.deltaTime * scrollspeed) / 10.0f;
            Vector2 offsetV = new Vector2(offset, 0);
            gameObject.GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex", offsetV);
        }
	}
     
    void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.tag == "Player")
        {
            switch(type)
            {
                case 0:
                    break;
                case 1:
                    target.gameObject.GetComponent<PlayerController>().staminaDecreaseRate += 5;
                    break;
                case 2:
                    {
                        target.gameObject.GetComponent<PlayerController>().orignalSpeed *= .75f;
                        target.gameObject.GetComponent<PlayerController>().sprintSpeed *= .65f;
                    }
                    break;
                case 3:
                    {
                        target.gameObject.GetComponent<PlayerController>().staminaDecreaseRate += 10;
                        target.gameObject.GetComponent<PlayerController>().orignalSpeed *= .85f;
                        target.gameObject.GetComponent<PlayerController>().sprintSpeed *= .75f;
                    }
                    break;
                case 4:
                    target.gameObject.GetComponent<PlayerStatManager>().playerHealth -= 3;
                    break;
                case 5:
                    target.gameObject.GetComponent<PlayerStatManager>().thirstDecayRate *= 2;
                    target.gameObject.GetComponent<PlayerController>().staminaDecreaseRate += 5;
                    break;
                default:
                break;
            }
        }
        if (target.gameObject.tag == "Cannibal")
        {
            switch (type)
            {
                case 2:
                    target.gameObject.GetComponent<AIBehavior>().m_fMovement *= .85f;
                    target.gameObject.GetComponent<AIBehavior>().m_fSlowTimer = float.MaxValue;
                    break;
                case 4:
                    target.gameObject.GetComponent<AIBehavior>().m_fHealth -= 3;
                    break;
                default:
                    break;
            }
        }
        else if (target.gameObject.tag == "Animal")
        {
            switch (type)
            {
                case 2:
                    target.gameObject.GetComponent<AIBehavior>().m_fMovement *= .85f;
                    target.gameObject.GetComponent<AIBehavior>().m_fSlowTimer = float.MaxValue;
                    break;
                case 4:
                    target.gameObject.GetComponent<AIBehavior>().m_fHealth -= 3;
                    break;
                default:
                    break;
            }
        }
    }
    void OnTriggerExit(Collider target)
    {
        if (target.gameObject.tag == "Player")
        {
            switch (type)
            {
                case 0:
                    break;
                case 1:
                    target.gameObject.GetComponent<PlayerController>().staminaDecreaseRate -= 5;
                    break;
                case 2:
                    {
                        target.gameObject.GetComponent<PlayerController>().orignalSpeed /= .75f;
                        target.gameObject.GetComponent<PlayerController>().sprintSpeed /= .65f;
                    }
                    break;
                case 3:
                    {
                        target.gameObject.GetComponent<PlayerController>().staminaDecreaseRate -= 10;
                        target.gameObject.GetComponent<PlayerController>().orignalSpeed /= .85f;
                        target.gameObject.GetComponent<PlayerController>().sprintSpeed /= .75f;
                    }
                    break;
                case 5:
                    target.gameObject.GetComponent<PlayerController>().staminaDecreaseRate -= 5;
                    target.gameObject.GetComponent<PlayerStatManager>().thirstDecayRate /= 2;
                    break;
                default:
                    break;
            }
        }
         else if (target.gameObject.tag == "Cannibal")
        {
            switch (type)
            {
                case 2 :
                    target.gameObject.GetComponent<AIBehavior>().m_fSlowTimer = 0;
                    break;
            }
        }
        else if (target.gameObject.tag == "Animal")
        {
            switch (type)
            {
                case 2:
                    target.gameObject.GetComponent<AIBehavior>().m_fSlowTimer = 0;
                    break;
            }
        }
    }
    void OnTriggerStay(Collider target)
    {
        if (target.gameObject.tag == "Player")
        {
            switch (type)
            {
                case 4:
                    target.gameObject.GetComponent<PlayerStatManager>().playerHealth -= 3;
                    break;
                default:
                    break;
            }
        }
        else if (target.gameObject.tag == "Cannibal")
        {
            switch (type)
            {
                case 4:
                    target.gameObject.GetComponent<AIBehavior>().m_fHealth -= 3;
                    break;
                default:
                    break;
            }
        }
        else if (target.gameObject.tag == "Animal")
        {
            switch (type)
            {
                case 4:
                    target.gameObject.GetComponent<AIBehavior>().m_fHealth -= 3;
                    break;
                default:
                    break;
            }
        }
    }
}
