using UnityEngine;
using System.Collections;

public class AmbientVolume : MonoBehaviour
{
    /// Bug # 3
    /// used for the audio source in main menu 
    public AudioSource[] AmbientSources;
    public float volume = 0;
    // Use this for initialization
    void Start()
    {
        if (AmbientSources.Length == 0)
        {
            AmbientSources = GetComponents<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        volume = PlayerPrefs.GetFloat("AmbientVolume");
        for (int i = 0; i < AmbientSources.Length; i++)
        {
            AmbientSources[i].volume = volume;// 10;
        }
    }
}
