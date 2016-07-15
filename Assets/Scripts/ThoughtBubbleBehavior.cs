using UnityEngine;
using System.Collections;

public class ThoughtBubbleBehavior : MonoBehaviour
{
    public enum BubbleTypes { ThirstBubble = 0, EBubble, FBubble, HungerBubble, EBubbleHeal, FBubbleFood, FBubbleWater, FBubbleFire };

    //public int thirstBubble = 0;
    //public int hungerBubble = 1;
    //public int fBubble = 2;
    //public int eBubble = 3;

    public SpriteRenderer[] thoughtBubbles;

    // Use this for initialization
    void Start()
    {
        thoughtBubbles = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < thoughtBubbles.Length; i++)
        {
            thoughtBubbles[i].enabled = false;
        }
    }

    public void ActivateControlBubble(int bubbleType)
    {
#if !UNITY_ANDROID
        thoughtBubbles[bubbleType].enabled = true;
        if (bubbleType == (int)BubbleTypes.HungerBubble)
            gameObject.GetComponentInParent<PlayerStatManager>().hungerBubbleActive = true;
        else if (bubbleType == (int)BubbleTypes.ThirstBubble)
            gameObject.GetComponentInParent<PlayerStatManager>().thirstBubbleActive = true;
#else
        if (bubbleType == (int)BubbleTypes.HungerBubble || bubbleType == (int)BubbleTypes.ThirstBubble)
        {
            thoughtBubbles[bubbleType].enabled = true;
            if (bubbleType == (int)BubbleTypes.HungerBubble)
                gameObject.GetComponentInParent<PlayerStatManager>().hungerBubbleActive = true;
            else if (bubbleType == (int)BubbleTypes.ThirstBubble)
                gameObject.GetComponentInParent<PlayerStatManager>().thirstBubbleActive = true;
        }
#endif
    }

    public void DeactivateControlBubble(int bubbleType)
    {
#if !UNITY_ANDROID
        thoughtBubbles[bubbleType].enabled = false;
        if (bubbleType == (int)BubbleTypes.HungerBubble)
            gameObject.GetComponentInParent<PlayerStatManager>().hungerBubbleActive = false;
        else if (bubbleType == (int)BubbleTypes.ThirstBubble)
            gameObject.GetComponentInParent<PlayerStatManager>().thirstBubbleActive = false;
#else
        if (bubbleType == (int)BubbleTypes.HungerBubble || bubbleType == (int)BubbleTypes.ThirstBubble)
        {
            thoughtBubbles[bubbleType].enabled = false;
            if (bubbleType == (int)BubbleTypes.HungerBubble)
                gameObject.GetComponentInParent<PlayerStatManager>().hungerBubbleActive = true;
            else if (bubbleType == (int)BubbleTypes.ThirstBubble)
                gameObject.GetComponentInParent<PlayerStatManager>().thirstBubbleActive = true;
        }
#endif
    }

    public void TurnBubbleRed(int bubbleType)
    {
        thoughtBubbles[bubbleType].color = new Color(1.0f, 0.2f, 0.2f);
    }

    public void TurnBubbleNormal(int bubbleType)
    {
        thoughtBubbles[bubbleType].color = new Color(1.0f, 1.0f, 1.0f);
    }

    public void TurnBubbleGreen(int bubbleType)
    {
        thoughtBubbles[bubbleType].color = new Color(.2f, 1.0f, .2f);
    }
}
