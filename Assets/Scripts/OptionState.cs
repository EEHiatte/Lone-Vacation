using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionState : MonoBehaviour
{

    public Toggle invuln;
    public Toggle resources;
    public Toggle hunger;
    public Toggle thirst;
    public Toggle QB;
    public Toggle animals;
    public Toggle cannibals;


    public Slider[] sliders;
    public Toggle fullScreenToggle;
    Resolution res;
    void Start()
    {
        sliders[0].value = PlayerPrefs.GetFloat("MusicVolume") * sliders[0].maxValue;
        sliders[1].value = PlayerPrefs.GetFloat("SFXVolume") * sliders[1].maxValue;
        sliders[2].value = PlayerPrefs.GetFloat("AmbientVolume") * sliders[2].maxValue;
        res = new Resolution();
        res.width = 1024;
        res.height = 768;
    }

    void Update()
    {
        if (fullScreenToggle)
        {
            fullScreenToggle.isOn = Screen.fullScreen;
        }
    }

    public void SaveMusicVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("MusicVolume", slider.value / slider.maxValue);
        PlayerPrefs.Save();
    }

    public void SaveSFXVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("SFXVolume", slider.value / slider.maxValue);
        PlayerPrefs.Save();
    }

    public void SaveAmbientVolume(Slider slider)
    {
        PlayerPrefs.SetFloat("AmbientVolume", slider.value / slider.maxValue);
        PlayerPrefs.Save();
    }

    public void ToggleFullScreen()
    {
        //Screen.fullScreen = !Screen.fullScreen;
        if (Screen.fullScreen)
        {
            Screen.SetResolution(res.width, res.height, false);
        }
        else
        {
            Screen.SetResolution(Screen.resolutions[Screen.resolutions.Length - 1].width, Screen.resolutions[Screen.resolutions.Length - 1].height, true);
        }
    }

    public void Invulnerability()
    {
        if (invuln.isOn)
        {
            PlayerPrefs.SetInt("God Mode", 1);
        }
        else
            PlayerPrefs.SetInt("God Mode", 0);

        PlayerPrefs.Save();
    }

    public void InfiniteResources()
    {
        if (resources.isOn)
        {
            PlayerPrefs.SetInt("No Worries", 1);
        }
        else
            PlayerPrefs.SetInt("No Worries", 0);

        PlayerPrefs.Save();
    }

    public void Hunger()
    {
        if (hunger.isOn)
        {
            PlayerPrefs.SetInt("Hungary", 1);
        }
        else
            PlayerPrefs.SetInt("Hungary", 0);

        PlayerPrefs.Save();
    }

    public void Thirst()
    {
        if (thirst.isOn)
        {
            PlayerPrefs.SetInt("Thirstay", 1);
        }
        else
            PlayerPrefs.SetInt("Thirstay", 0);

        PlayerPrefs.Save();
    }

    public void QuickBuild()
    {
        if (QB.isOn)
        {
            PlayerPrefs.SetInt("building", 1);
        }
        else
            PlayerPrefs.SetInt("building", 0);

        PlayerPrefs.Save();
    }

    public void Animals()
    {
        if (animals.isOn)
        {
            PlayerPrefs.SetInt("animals", 1);
        }
        else
            PlayerPrefs.SetInt("animals", 0);

        PlayerPrefs.Save();
    }
    public void Cannibals()
    {
        if (cannibals.isOn)
        {
            PlayerPrefs.SetInt("cannibals", 1);
        }
        else
            PlayerPrefs.SetInt("cannibals", 0);

        PlayerPrefs.Save();
    }
}
