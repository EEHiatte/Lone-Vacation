using UnityEngine;
using System.Collections;

public class DisableOnMobile : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        if (Application.isMobilePlatform)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf && Application.isMobilePlatform)
        {
            gameObject.SetActive(false);
        }
    }
}
