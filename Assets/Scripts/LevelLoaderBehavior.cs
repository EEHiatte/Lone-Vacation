using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class LevelLoaderBehavior : MonoBehaviour
{

    int levelToLoad = -1;
    Slider progressBar = null;
    bool loadingStarted = false;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Application.loadedLevel == 9 && !loadingStarted) // TODO: HARDCODED FOR TESTING
        {
            loadingStarted = true;
            StartCoroutine(LoadIndex(levelToLoad));
        }
    }

    public void LoadLevel(int level)
    {
        Application.LoadLevel("LoadingScene");
        levelToLoad = level;
    }

    IEnumerator LoadIndex(int level)
    {
        AsyncOperation async = Application.LoadLevelAsync(level);

        while (!async.isDone)
        {
            if (progressBar != null)
            {
                progressBar.value = (async.progress * 100.0f) / 100.0f;
            }
            else
            {
                GameObject barObject = GameObject.Find("ProgressBar");
                if (barObject != null)
                    progressBar = barObject.GetComponent<Slider>();
            }

            yield return null;
        }

        Destroy(gameObject);
    }
}
