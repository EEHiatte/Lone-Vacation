using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public class MainMenu : MonoBehaviour
{
	GameObject levelLoader;
    int levelindex;

    public static bool loaded = false;
    
    void Start()
    {
        levelLoader = GameObject.Find("LevelLoader");
#if UNITY_ANDROID
            Application.targetFrameRate = 30;
            Cursor.visible = false;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
#elif !UNITY_ANDROID
            Application.targetFrameRate = 60;
#endif
    }

    void Update()
    {
        if (levelLoader == null)
        {
            levelLoader = GameObject.Find("LevelLoader");
        }
    }

    void RunGame()
    {
        levelLoader.GetComponent<LevelLoaderBehavior>().LoadLevel(levelindex);
    }

    public void EasyGame()
    {
        PlayerPrefs.SetInt("Difficulty", 1);
        RunGame();
    }

    public void NormalGame()
    {
        PlayerPrefs.SetInt("Difficulty", 2);
        RunGame();

    }

    public void HardGame()
    {
        PlayerPrefs.SetInt("Difficulty", 3);
        RunGame();
    }

    public void InsaneGame()
    {
        PlayerPrefs.SetInt("Difficulty", 4);
        RunGame();
    }

   public void SetLevelIndex(int LevelIndex)
    {
        levelindex = LevelIndex;
        PlayerPrefs.SetInt("island", LevelIndex);
    }

    public int GetLevelIndex()
   {
       return levelindex;
   }

    public void Load()
    {
        string path;
        if (Application.isMobilePlatform)
        {
            path = Application.persistentDataPath + "/Playerinfo.dat";
        }
        else
        {
            path = Environment.CurrentDirectory + @"\SaveFiles\Playerinfo.dat";
        }


        if (File.Exists(path))
        {
            ///////////
            //BUG FIX (WILL)
            //BUG #28/29
            BinaryFormatter lf = new BinaryFormatter();

            FileStream file = File.Open(path, FileMode.Open);
            PlayerSaving loads = (PlayerSaving)lf.Deserialize(file);
            file.Close();

            PlayerPrefs.SetInt("island", loads.island);
            //END BUG FIX
            ///////////
            levelLoader.GetComponent<LevelLoaderBehavior>().LoadLevel(PlayerPrefs.GetInt("island"));
            loaded = true; 
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        // Quits if in Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Quits if in actual application
		Application.Quit();
#endif
    }
}
