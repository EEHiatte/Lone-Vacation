using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleActive : MonoBehaviour
{
    public bool message = false;
    public void ToggleActiveState()
    {
        if (PlayerPrefs.GetInt("TutorialFinished") != 1 && gameObject.name == "IslandPanel")
        {
            message = true;
            if (GameObject.Find("NewGame Message") != null) 
                GameObject.Find("NewGame Message").GetComponentInChildren<Image>().enabled = true;
            return;
        }
        else
        {
            if (GameObject.Find("NewGame Message") != null)
                GameObject.Find("NewGame Message").GetComponentInChildren<Image>().enabled = false;
        }
            

        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void SetActiveState()
    {
        gameObject.SetActive(true);
    }

    public void SetInactiveState()
    {
        gameObject.SetActive(false);
    }
}
