using UnityEngine;
using System.Collections;

public class MoviePlayer : MonoBehaviour
{
#if !UNITY_ANDROID
    public bool winMovie;
    bool playAlready = false;
    MovieTexture movie;
    AudioSource[] music;
    bool canSkip = true;
#endif

    // Use this for initialization
    void Start()
    {
#if !UNITY_ANDROID
        float scaleX = (float)Screen.width / (float)Screen.height;
        transform.localScale = new Vector3(scaleX, 1.0f, 1.0f);
        movie = gameObject.GetComponent<Renderer>().material.mainTexture as MovieTexture;
        music = GetComponents<AudioSource>();
        movie.Play();
        if (movie.isPlaying)
            music[0].Play();

        if (Input.anyKey)
            canSkip = false;
#elif UNITY_ANDROID
        if (Application.loadedLevel == 6)
        {
            // Win Scene
            Handheld.PlayFullScreenMovie("WinMP4.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput, FullScreenMovieScalingMode.AspectFit);
        }
        else if (Application.loadedLevel == 7)
        {
            // Lose Scene
            Handheld.PlayFullScreenMovie("LoseMP4.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput, FullScreenMovieScalingMode.AspectFit);
        }
        else if (Application.loadedLevel == 8)
        {
            // Credits scene
            Handheld.PlayFullScreenMovie("CreditsMP4.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput, FullScreenMovieScalingMode.AspectFit);
        }
#endif
    }

    // Update is called once per frame
    void Update()
    {
#if !UNITY_ANDROID
        if (!canSkip && Input.anyKey)
            canSkip = true;

        if (winMovie && !playAlready && !music[0].isPlaying)
        {
            music[1].Play();
            playAlready = true;
        }

        if (!movie.isPlaying)
        {
            for (int i = 0; i < music.Length; i++)
                music[i].Stop();
            Application.LoadLevel((int)GameManager.LevelID.MainMenu);
        }
        else if (canSkip && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            for (int i = 0; i < music.Length; i++)
                music[i].Stop();
            Application.LoadLevel((int)GameManager.LevelID.MainMenu);
        }
#elif UNITY_ANDROID
        Application.LoadLevel(0);
#endif
    }
}
