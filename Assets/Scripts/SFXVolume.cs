using UnityEngine;
using System.Collections;

public class SFXVolume : MonoBehaviour
{
    public AudioSource[] SFXSources;
    public float volume = -1;

    void Awake()
    {
        if (SFXSources.Length == 0)
        {
            SFXSources = GetComponents<AudioSource>();
        }
    }

    void Update()
    {
        if (gameObject.GetComponent<CollectableBehavior>() && volume != -1)
        {
            return;
        }
        volume = PlayerPrefs.GetFloat("SFXVolume");
        for (int i = 0; i < SFXSources.Length; i++)
        {
            SFXSources[i].volume = volume;// 10;
        }
    }
}
