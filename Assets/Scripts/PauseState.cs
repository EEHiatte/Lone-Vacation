using UnityEngine;
using System.Collections;

public class PauseState : MonoBehaviour
{
    public GameObject pausePanel;


    void Start()
    {
        pausePanel = transform.parent.gameObject.GetComponent<GameManager>().pausePanel;
        //pausePanel.GetComponent<ToggleActive>().SetInactiveState();
        //Resume();
    }

    void Update()
    {
        if (pausePanel == null)
        {
            pausePanel = transform.parent.gameObject.GetComponent<GameManager>().pausePanel;                    
        }
    }

    public void SetPausePanel(GameObject obj)
    {
        pausePanel = obj;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        GameManager.paused = false;
        pausePanel.GetComponent<ToggleActive>().SetInactiveState();
    }

    public void Options()
    {
        //TODO
    }

    public void MainMenu()
    {
        Save();
        Resume();
        GameManager.paused = false;
        GameObject.Find("LevelLoader").GetComponent<LevelLoaderBehavior>().LoadLevel((int)GameManager.LevelID.MainMenu);
        //Application.LoadLevel((int)GameManager.LevelID.MainMenu);
    }

    public void Save()
    {
        gameObject.GetComponentInParent<SaveInfo>().Save();
    }
}
