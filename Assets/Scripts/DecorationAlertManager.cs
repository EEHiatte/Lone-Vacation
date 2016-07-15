using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DecorationAlertManager : MonoBehaviour 
{
    int youCollected = 6;
    int youCompleted = 7;
    int twoOfThree = 8;
    int oneOfThree = 9;
    int inTheCove = 10;

    float messageTimer = -1.0f;
    float maxMessageTime = 5.0f;

    Image[] messages;

	// Use this for initialization
	void Start () 
    {
        messages = GetComponentsInChildren<Image>();

        for (int i = 0; i < messages.Length; i++)
        {
            messages[i].enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(messageTimer >= 0.0f)
        {
            messageTimer += Time.deltaTime;
            if(messageTimer >= maxMessageTime)
            {
                for (int i = 0; i < messages.Length; i++)
                {
                    if(messages[i].enabled)
                        messages[i].enabled = false;
                }
                messageTimer = -1.0f;
            }
        }
	}

    public void ActivateMessage(int decorationType, int toCollect)
    {
        if (decorationType <= 5 && decorationType >= 0)
        {
            messages[decorationType].enabled = true;
            if (toCollect == 1)
            {
                messages[youCollected].enabled = true;
                messages[twoOfThree].enabled = true;
            }
            else if (toCollect == 2)
            {
                messages[youCollected].enabled = true;
                messages[oneOfThree].enabled = true;
            }
            else if (toCollect == 0)
            {
                messages[youCompleted].enabled = true;
                messages[inTheCove].enabled = true;
            }
            messageTimer = 0.0f;
        }
    }
}