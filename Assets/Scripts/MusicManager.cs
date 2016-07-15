using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public AudioSource[] bgm;
    float volume = 0;
    int currSong = 0;

    // Use this for initialization
    void Awake()
    {
        //bgm = GetComponents<AudioSource>();
        volume = PlayerPrefs.GetFloat("MusicVolume");
        foreach(AudioSource sound in bgm)
            sound.volume = volume;
    }

    // Update is called once per frame
    void Update()
    {
        volume = PlayerPrefs.GetFloat("MusicVolume");
        foreach (AudioSource sound in bgm)
            sound.volume = volume;

        if (!bgm[currSong].isPlaying)
        {
            currSong++;
            if (currSong < bgm.Length)
                bgm[currSong].Play();
            else
            {
                currSong = 0;
                bgm[currSong].Play();
            }
        }
    }
}