using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /// /////////////////
    PauseState s_pause;// Pause menu variable 
    public GameObject pausePanel;
    /////////////////////

    public static bool paused = false;

    public enum LevelID
    {
        MainMenu = 0, DefaultIsland, VolcanoIsland, LizardIsland,
        CthulhuIsland, TutorialLevel, WinScene, LossScene, Credits,
        LoadingScene
    };

    float Timetimer = 0;
    public float currentTime;
    //float creditsTimer;
    public int currentDay;
    //int deathreason;
    GameObject fire;
    GameObject player;

    public GameObject sun;
    public GameObject moon;
    public PlayerStatManager playerStats;
    public GameObject CannibalHut;
    public GameObject AnimalNest;

    public GameObject birds;
    public GameObject crickets;

    // Use this for initialization
    void Start()
    {
        if (Application.loadedLevel != (int)GameManager.LevelID.TutorialLevel)
        {
            birds.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("AmbientVolume");
            crickets.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("AmbientVolume");
        }
        

        if (MainMenu.loaded == true)
        {
            gameObject.GetComponent<SaveInfo>().Load();
            MainMenu.loaded = false;
        }

        pausePanel = GameObject.Find("PausePanel");
        s_pause = GetComponentInChildren<PauseState>();
        s_pause.SetPausePanel(pausePanel);
        s_pause.Resume();

        currentTime = 0;
        currentDay = 1;

        fire = GameObject.FindGameObjectWithTag("Fire");

        //deathreason = -1;

        //creditsTimer = 0;

        player = GameObject.FindGameObjectWithTag("Player");
        //playerStats = player.GetComponent<PlayerStatManager>();
        sun = GameObject.Find("Sun");
        moon = GameObject.Find("Moon");

        Object.DontDestroyOnLoad(gameObject);
        if (Application.loadedLevel == 1)
        {
            if (PlayerPrefs.GetInt("cannibals") != 1)
            {
                GameObject goTempHut;
                goTempHut = (GameObject)Instantiate(CannibalHut, new Vector3(600, 6, 800), new Quaternion(0, 0, 0, 0));
                goTempHut.GetComponent<AIManager>().m_fSpawnWait = 0f;
                goTempHut = (GameObject)Instantiate(CannibalHut, new Vector3(1450, 6, 1150), new Quaternion(0, 0, 0, 0));
                goTempHut.GetComponent<AIManager>().m_fSpawnWait = 0.2f;
            }
            if (PlayerPrefs.GetInt("animals") != 1)
            {
                GameObject goTempNest;
                goTempNest = (GameObject)Instantiate(AnimalNest, new Vector3(1280, 6, 340), new Quaternion(0, 0, 0, 0));
                goTempNest.GetComponent<AIManager>().m_entNestType = AIManager.eNestTypes.Boar;
                goTempNest = (GameObject)Instantiate(AnimalNest, new Vector3(500, 6, 300), new Quaternion(0, 0, 0, 0));
                goTempNest.GetComponent<AIManager>().m_entNestType = AIManager.eNestTypes.HoneyBadger;
                goTempNest = (GameObject)Instantiate(AnimalNest, new Vector3(1040, 20, 1540), new Quaternion(0, 0, 0, 0));
                goTempNest.GetComponent<AIManager>().m_entNestType = AIManager.eNestTypes.Wolf;
            }
        }
        else if (Application.loadedLevel == 2)
        {
            if (PlayerPrefs.GetInt("cannibals") != 1)
            {
                GameObject goTempHut;
                goTempHut = (GameObject)Instantiate(CannibalHut, new Vector3(1100, 6, 1220), new Quaternion(0, 0, 0, 0));
                goTempHut.GetComponent<AIManager>().m_fSpawnWait = 0f;
                goTempHut = (GameObject)Instantiate(CannibalHut, new Vector3(1245, 6, 300), new Quaternion(0, 0, 0, 0));
                goTempHut.GetComponent<AIManager>().m_fSpawnWait = 0.2f;
            }
            if (PlayerPrefs.GetInt("animals") != 1)
            {
                GameObject goTempNest;
                goTempNest = (GameObject)Instantiate(AnimalNest, new Vector3(825, 6, 500), new Quaternion(0, 0, 0, 0));
                goTempNest.GetComponent<AIManager>().m_entNestType = AIManager.eNestTypes.Boar;
                goTempNest = (GameObject)Instantiate(AnimalNest, new Vector3(750, 6, 800), new Quaternion(0, 0, 0, 0));
                goTempNest.GetComponent<AIManager>().m_entNestType = AIManager.eNestTypes.HoneyBadger;
                goTempNest = (GameObject)Instantiate(AnimalNest, new Vector3(400, 6, 890), new Quaternion(0, 0, 0, 0));
                goTempNest.GetComponent<AIManager>().m_entNestType = AIManager.eNestTypes.Wolf;
            }
        }
        else if (Application.loadedLevel == 3)
        {
            if (PlayerPrefs.GetInt("cannibals") != 1)
            {
                GameObject goTempHut;
                goTempHut = (GameObject)Instantiate(CannibalHut, new Vector3(1250, 6, 1075), new Quaternion(0, 0, 0, 0));
                goTempHut.GetComponent<AIManager>().m_fSpawnWait = 0f;
                goTempHut = (GameObject)Instantiate(CannibalHut, new Vector3(480, 6, 1260), new Quaternion(0, 0, 0, 0));
                goTempHut.GetComponent<AIManager>().m_fSpawnWait = 0.2f;
            }
            if (PlayerPrefs.GetInt("animals") != 1)
            {
                GameObject goTempNest;
                goTempNest = (GameObject)Instantiate(AnimalNest, new Vector3(1110, 6, 675), new Quaternion(0, 0, 0, 0));
                goTempNest.GetComponent<AIManager>().m_entNestType = AIManager.eNestTypes.Boar;
                goTempNest = (GameObject)Instantiate(AnimalNest, new Vector3(1250, 6, 375), new Quaternion(0, 0, 0, 0));
                goTempNest.GetComponent<AIManager>().m_entNestType = AIManager.eNestTypes.HoneyBadger;
                goTempNest = (GameObject)Instantiate(AnimalNest, new Vector3(390, 6, 800), new Quaternion(0, 0, 0, 0));
                goTempNest.GetComponent<AIManager>().m_entNestType = AIManager.eNestTypes.Wolf;
            }
        }
        else if (Application.loadedLevel == 4)
        {
            if (PlayerPrefs.GetInt("cannibals") != 1)
            {
                GameObject goTempHut;
                goTempHut = (GameObject)Instantiate(CannibalHut, new Vector3(430, 6, 400), new Quaternion(0, 0, 0, 0));
                goTempHut.GetComponent<AIManager>().m_fSpawnWait = 0f;
                goTempHut = (GameObject)Instantiate(CannibalHut, new Vector3(1070, 6, 410), new Quaternion(0, 0, 0, 0));
                goTempHut.GetComponent<AIManager>().m_fSpawnWait = 0.2f;
            }
            if (PlayerPrefs.GetInt("animals") != 1)
            {
                GameObject goTempNest;
                goTempNest = (GameObject)Instantiate(AnimalNest, new Vector3(450, 6, 960), new Quaternion(0, 0, 0, 0));
                goTempNest.GetComponent<AIManager>().m_entNestType = AIManager.eNestTypes.Boar;
                goTempNest = (GameObject)Instantiate(AnimalNest, new Vector3(560, 6, 1200), new Quaternion(0, 0, 0, 0));
                goTempNest.GetComponent<AIManager>().m_entNestType = AIManager.eNestTypes.HoneyBadger;
                goTempNest = (GameObject)Instantiate(AnimalNest, new Vector3(1105, 6, 690), new Quaternion(0, 0, 0, 0));
                goTempNest.GetComponent<AIManager>().m_entNestType = AIManager.eNestTypes.Wolf;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Timetimer += Time.deltaTime;

        if (pausePanel == null)
        {
            pausePanel = GameObject.Find("PausePanel");
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            TogglePause();
        }

        if (Application.loadedLevel == 0)
        {
            Object.Destroy(gameObject);
        }
        // if we are in one of the 4 main island scenes
        if (Application.loadedLevel == 1 || Application.loadedLevel == 2 || Application.loadedLevel == 3 || Application.loadedLevel == 4)
        {
            currentTime += Time.deltaTime;
            if (currentTime > 10 && currentTime < 18)
            {
                if (birds.GetComponent<AudioSource>().isPlaying == false)
                {
                    birds.GetComponent<AudioSource>().Play();
                }
            }
            else
                birds.GetComponent<AudioSource>().Stop();

            if (currentTime > 150 && currentTime < 180)
            {
                if (crickets.GetComponent<AudioSource>().isPlaying == false)
                {
                    crickets.GetComponent<AudioSource>().Play();
                }
            }
            else
                crickets.GetComponent<AudioSource>().Stop();

            sun.transform.Rotate(new Vector3(-Time.deltaTime, 0, 0)); // rotate sun
            moon.transform.Rotate(new Vector3(-Time.deltaTime, 0, 0)); // rotate moon
            CycleDay(); // change the day
            CheckVictory(); // check for win or loss
            player.GetComponent<PlayerStatManager>().IncreaseHealth(.1666666f);
        }

        if (Application.loadedLevel == 6)
        {
            //creditsTimer += Time.deltaTime;
            //if (creditsTimer >= 4)
            //{
            //    creditsTimer = 0;
            //    Application.LoadLevel(8);
            //}

        }
        // if we're in the loss scene
        if (Application.loadedLevel == 7)
        {
            //if (deathreason == 0)
            //{
            //    GameObject.Find("Reason").GetComponent<Text>().text = "Reason: You died!";
            //}
            //else if (deathreason == 1)
            //{
            //    GameObject.Find("Reason").GetComponent<Text>().text = "Reason: Your fire died out! You will never be rescued!";
            //}
        }
        //if (Application.loadedLevel == 8)
        //{
        //    creditsTimer += Time.deltaTime;
        //    if (creditsTimer >= 8)
        //    {
        //        Application.LoadLevel(0);
        //    }
        //}
    }

    // Use this to cycle through the day
    void CycleDay()
    {
        if (currentTime > 360)
        {
            currentDay += 1;
            currentTime = 0;
        }

        switch (currentDay)
        {
            case 3: PlayerPrefs.SetInt("Survival 1", 1);
                break;
            case 6: PlayerPrefs.SetInt("Survival 2", 1);
                break;
            case 9: PlayerPrefs.SetInt("Survival 3", 1);
                break;
            case 12: PlayerPrefs.SetInt("Survival 4", 1);
                break;
            case 15: PlayerPrefs.SetInt("Survival 5", 1);
                break;
            default:
                break;
        }
        PlayerPrefs.Save();
    }

    // Use this to Check whether the player has won
    void CheckVictory()
    {
        //if the plyer dies or the fire goes out
        if (GameObject.Find("PlayerPrefab").GetComponent<PlayerStatManager>().playerHealth <= 0 ||
            GameObject.Find("Fire Pit").GetComponent<FireBehavior>().fireIntensity <= 0)
        {
            // if the player died
            if (player.GetComponent<PlayerStatManager>().playerHealth <= 0)
            {
                //deathreason = 0; // the player died
            }
            //if the player let their fire die out
            else if (fire.GetComponent<FireBehavior>().fireIntensity <= 0)
            {
                //deathreason = 1; // their fire died out
                PlayerPrefs.SetInt("Fire 1", 1);
                PlayerPrefs.Save();
            }
            //load the loss scene
            SaveInfo.DeleteSave();
            Application.LoadLevel((int)LevelID.LossScene);
        }

        // create the random value for win chance
        // checks the win condition every minute
        if (currentTime > 59 && currentTime < 61 || currentTime > 119 && currentTime < 121 || currentTime > 179 && currentTime < 181 || currentTime > 239 && currentTime < 241 || currentTime > 299 && currentTime < 301 || currentTime > 359)
        {
            if (Timetimer >= 60)
            {
                Timetimer = 0;
                gameObject.GetComponent<SaveInfo>().Save();
                float random = Random.Range(0, 100);
                // is the fire intensity between two certain numbers
                if (fire.GetComponent<FireBehavior>().fireIntensity >= 50 && fire.GetComponent<FireBehavior>().fireIntensity <= 55)
                {
                    // based on the fires intensity range give a chance for the player to win
                    if (random >= 0 && random <= 1)
                    {
                        // player wins
                        Application.LoadLevel((int)LevelID.WinScene);
                    }
                }
                // is the fire intensity between two certain numbers
                else if (fire.GetComponent<FireBehavior>().fireIntensity >= 56 && fire.GetComponent<FireBehavior>().fireIntensity <= 60)
                {
                    // based on the fires intensity range give a chance for the player to win
                    // player wins
                    if (random > 0 && random < 2)
                    {
                        Application.LoadLevel((int)LevelID.WinScene);
                    }
                }
                // is the fire intensity between two certain numbers
                else if (fire.GetComponent<FireBehavior>().fireIntensity >= 61 && fire.GetComponent<FireBehavior>().fireIntensity <= 65)
                {
                    // based on the fires intensity range give a chance for the player to win
                    if (random > 0 && random < 3)
                    {
                        // player wins
                        Application.LoadLevel((int)LevelID.WinScene);
                    }
                }
                // is the fire intensity between two certain numbers
                else if (fire.GetComponent<FireBehavior>().fireIntensity >= 66 && fire.GetComponent<FireBehavior>().fireIntensity <= 70)
                {
                    // based on the fires intensity range give a chance for the player to win
                    if (random > 0 && random < 4)
                    {
                        // player wins
                        Application.LoadLevel((int)LevelID.WinScene);
                    }
                }
                // is the fire intensity between two certain numbers
                else if (fire.GetComponent<FireBehavior>().fireIntensity >= 71 && fire.GetComponent<FireBehavior>().fireIntensity <= 75)
                {
                    // based on the fires intensity range give a chance for the player to win
                    if (random > 0 && random < 5)
                    {
                        // player wins
                        Application.LoadLevel((int)LevelID.WinScene);
                    }
                }
                // is the fire intensity between two certain numbers
                else if (fire.GetComponent<FireBehavior>().fireIntensity >= 76 && fire.GetComponent<FireBehavior>().fireIntensity <= 80)
                {
                    // based on the fires intensity range give a chance for the player to win
                    if (random > 0 && random < 6)
                    {
                        // player wins
                        Application.LoadLevel((int)LevelID.WinScene);
                    }
                }
                // is the fire intensity between two certain numbers
                else if (fire.GetComponent<FireBehavior>().fireIntensity >= 81 && fire.GetComponent<FireBehavior>().fireIntensity <= 85)
                {
                    // based on the fires intensity range give a chance for the player to win
                    if (random > 0 && random < 7)
                    {
                        // player wins
                        Application.LoadLevel((int)LevelID.WinScene);
                    }
                }
                // is the fire intensity between two certain numbers
                else if (fire.GetComponent<FireBehavior>().fireIntensity >= 86 && fire.GetComponent<FireBehavior>().fireIntensity <= 90)
                {
                    // based on the fires intensity range give a chance for the player to win
                    if (random > 0 && random < 8)
                    {
                        // player wins
                        Application.LoadLevel((int)LevelID.WinScene);
                    }
                }
                // is the fire intensity between two certain numbers
                else if (fire.GetComponent<FireBehavior>().fireIntensity >= 91 && fire.GetComponent<FireBehavior>().fireIntensity <= 95)
                {
                    // based on the fires intensity range give a chance for the player to win
                    if (random > 0 && random < 9)
                    {
                        // player wins
                        Application.LoadLevel((int)LevelID.WinScene);
                    }
                }
                // is the fire intensity between two certain numbers
                else if (fire.GetComponent<FireBehavior>().fireIntensity >= 96 && fire.GetComponent<FireBehavior>().fireIntensity <= 100)
                {
                    // based on the fires intensity range give a chance for the player to win
                    if (random > 0 && random < 10)
                    {
                        // player wins
                        Application.LoadLevel((int)LevelID.WinScene);
                    }
                }
            }
        }
    }

    public void TogglePause()
    {
        ///////////
        //BUG FIX
        //BUG #9 (!paused)
        if (Time.timeScale == 1 && !paused)
        {
            Time.timeScale = 0;
            paused = true;
            pausePanel.GetComponent<ToggleActive>().SetActiveState();
        }
        else if(paused)
            s_pause.Resume();

        if (GameObject.Find("Pause Panel Achievements") != null)
        {
            if (GameObject.Find("Pause Panel Achievements").activeSelf == true)
            {
                GameObject.Find("Pause Panel Achievements").GetComponent<ToggleActive>().SetInactiveState();
            }
        }
    }
}
