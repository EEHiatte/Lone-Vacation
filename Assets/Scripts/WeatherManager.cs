using UnityEngine;
using System.Collections;

public class WeatherManager : MonoBehaviour
{
    public GameManager m_gmDays;
    public WeatherBehavior m_wbWeather;
    enum WeatherType { Clear = 0, Cold, Hot, Rain };
    public int m_nCurrWeather;
    float m_fWeatherTimer;

    public GameObject Wind;
    public GameObject Rain;

    // Use this for initialization
    void Start()
    {
        Wind.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("AmbientVolume");
        Rain.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("AmbientVolume");
        
        m_nCurrWeather = 0;
        m_wbWeather.ApplyWeather(m_nCurrWeather);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_gmDays.currentDay > 2)
        {
            m_fWeatherTimer -= Time.deltaTime;
            if (m_fWeatherTimer <= 0)
            {
                int nRandom = Random.Range(0, 99);
                if (m_nCurrWeather == (int)WeatherType.Clear)
                {
                    if (nRandom >= 0 && nRandom < 15)
                    {
                        m_nCurrWeather = (int)WeatherType.Clear;
                        Rain.GetComponent<AudioSource>().Stop();
                        Rain.GetComponent<AudioSource>().loop = false;
                        Wind.GetComponent<AudioSource>().Stop();
                        Wind.GetComponent<AudioSource>().loop = false;

                        m_fWeatherTimer = Random.Range(60.0f, 120.0f);
                    }
                    else if (nRandom >= 15 && nRandom < 50)
                    {
                        m_nCurrWeather = (int)WeatherType.Cold;
                        Rain.GetComponent<AudioSource>().Stop();
                        Rain.GetComponent<AudioSource>().loop = false;
                        if (Wind.GetComponent<AudioSource>().isPlaying == false)
                        {
                            Wind.GetComponent<AudioSource>().Play();
                        }
                        m_fWeatherTimer = Random.Range(90.0f, 150.0f);
                    }
                    else if (nRandom >= 50 && nRandom < 80)
                    {
                        m_nCurrWeather = (int)WeatherType.Hot;
                        Rain.GetComponent<AudioSource>().Stop();
                        Rain.GetComponent<AudioSource>().loop = false;
                        Wind.GetComponent<AudioSource>().Stop();
                        Wind.GetComponent<AudioSource>().loop = false;
                        m_fWeatherTimer = Random.Range(120.0f, 180.0f);
                    }
                    else if (nRandom >= 80 && nRandom < 100)
                    {
                        m_nCurrWeather = (int)WeatherType.Rain;
                        Wind.GetComponent<AudioSource>().Stop();
                        Wind.GetComponent<AudioSource>().loop = false;
                        if (Rain.GetComponent<AudioSource>().isPlaying == false)
                        {
                            Rain.GetComponent<AudioSource>().Play();
                        }
                        m_fWeatherTimer = Random.Range(90.0f, 150.0f);
                    }
                }

                else if (m_nCurrWeather == (int)WeatherType.Cold)
                {
                    if (Wind.GetComponent<AudioSource>().isPlaying == false)
                    {
                        Wind.GetComponent<AudioSource>().Play();
                    }
                    if (nRandom >= 0 && nRandom < 35)
                    {
                        m_nCurrWeather = (int)WeatherType.Clear;
                        Rain.GetComponent<AudioSource>().Stop();
                        Rain.GetComponent<AudioSource>().loop = false;
                        Wind.GetComponent<AudioSource>().Stop();
                        Wind.GetComponent<AudioSource>().loop = false;
                        m_fWeatherTimer = Random.Range(120.0f, 180.0f);
                    }
                    else if (nRandom >= 35 && nRandom < 55)
                    {
                        m_nCurrWeather = (int)WeatherType.Cold; 
                        Rain.GetComponent<AudioSource>().loop = false;
                        Rain.GetComponent<AudioSource>().Stop();
                        if (Wind.GetComponent<AudioSource>().isPlaying == false)
                        {
                            Wind.GetComponent<AudioSource>().Play();
                        }
                        m_fWeatherTimer = Random.Range(60.0f, 120.0f);
                    }
                    else if (nRandom >= 55 && nRandom < 70)
                    {
                        m_nCurrWeather = (int)WeatherType.Hot;
                        Rain.GetComponent<AudioSource>().Stop();
                        Rain.GetComponent<AudioSource>().loop = false;
                        Wind.GetComponent<AudioSource>().Stop();
                        Wind.GetComponent<AudioSource>().loop = false;
                        m_fWeatherTimer = Random.Range(60.0f, 120.0f);
                    }
                    else if (nRandom >= 70 && nRandom < 100)
                    {
                        m_nCurrWeather = (int)WeatherType.Rain;
                        Wind.GetComponent<AudioSource>().Stop();
                        Wind.GetComponent<AudioSource>().loop = false;
                        if (Rain.GetComponent<AudioSource>().isPlaying == false)
                        {
                            Rain.GetComponent<AudioSource>().Play();
                        }
                        m_fWeatherTimer = Random.Range(90.0f, 150.0f);
                    }
                }

                else if (m_nCurrWeather == (int)WeatherType.Hot)
                {
                    if (nRandom >= 0 && nRandom < 30)
                    {
                        m_nCurrWeather = (int)WeatherType.Clear;
                        Rain.GetComponent<AudioSource>().Stop();
                        Rain.GetComponent<AudioSource>().loop = false;
                        Wind.GetComponent<AudioSource>().Stop();
                        Wind.GetComponent<AudioSource>().loop = false;
                        m_fWeatherTimer = Random.Range(90.0f, 180.0f);
                    }
                    else if (nRandom >= 30 && nRandom < 45)
                    {
                        m_nCurrWeather = (int)WeatherType.Cold;
                        Rain.GetComponent<AudioSource>().loop = false;
                        Rain.GetComponent<AudioSource>().Stop();
                        if (Wind.GetComponent<AudioSource>().isPlaying == false)
                        {
                            Wind.GetComponent<AudioSource>().Play();
                        }
                        m_fWeatherTimer = Random.Range(60.0f, 120.0f);
                    }
                    else if (nRandom >= 45 && nRandom < 70)
                    {
                        m_nCurrWeather = (int)WeatherType.Hot;
                        Rain.GetComponent<AudioSource>().Stop();
                        Rain.GetComponent<AudioSource>().loop = false;
                        Wind.GetComponent<AudioSource>().Stop();
                        Wind.GetComponent<AudioSource>().loop = false;
                        m_fWeatherTimer = Random.Range(60.0f, 120.0f);
                    }
                    else if (nRandom >= 70 && nRandom < 100)
                    {
                        m_nCurrWeather = (int)WeatherType.Rain;
                        Wind.GetComponent<AudioSource>().Stop();
                        Wind.GetComponent<AudioSource>().loop = false;
                        if (Rain.GetComponent<AudioSource>().isPlaying == false)
                        {
                            Rain.GetComponent<AudioSource>().Play();
                        }
                        m_fWeatherTimer = Random.Range(60.0f, 120.0f);
                    }
                }
                else if (m_nCurrWeather == (int)WeatherType.Rain)
                {
                    
                    Wind.GetComponent<AudioSource>().Stop();
                    Wind.GetComponent<AudioSource>().loop = false;
                    if (Rain.GetComponent<AudioSource>().isPlaying == false)
                    {
                        Rain.GetComponent<AudioSource>().Play();
                    }

                    if (nRandom >= 0 && nRandom < 20)
                    {
                        m_nCurrWeather = (int)WeatherType.Clear;

                        Rain.GetComponent<AudioSource>().Stop();
                        Rain.GetComponent<AudioSource>().loop = false;
                        Wind.GetComponent<AudioSource>().Stop();
                        Wind.GetComponent<AudioSource>().loop = false;
                        m_fWeatherTimer = Random.Range(60.0f, 120.0f);
                    }
                    else if (nRandom >= 20 && nRandom < 55)
                    {
                        m_nCurrWeather = (int)WeatherType.Cold;

                        Rain.GetComponent<AudioSource>().loop = false;
                        Rain.GetComponent<AudioSource>().Stop();

                        if (Wind.GetComponent<AudioSource>().isPlaying == false)
                        {
                            Wind.GetComponent<AudioSource>().Play();
                        }
                        m_fWeatherTimer = Random.Range(120.0f, 180.0f);
                    }
                    else if (nRandom >= 55 && nRandom < 85)
                    {
                        m_nCurrWeather = (int)WeatherType.Hot;

                        Rain.GetComponent<AudioSource>().Stop();
                        Rain.GetComponent<AudioSource>().loop = false;
                        Wind.GetComponent<AudioSource>().Stop();
                        Wind.GetComponent<AudioSource>().loop = false;

                        m_fWeatherTimer = Random.Range(60.0f, 120.0f);
                    }
                    else if (nRandom >= 85 && nRandom < 100)
                    {
                        m_nCurrWeather = (int)WeatherType.Rain;

                        Wind.GetComponent<AudioSource>().Stop();
                        Wind.GetComponent<AudioSource>().loop = false;

                        if (Rain.GetComponent<AudioSource>().isPlaying == false)
                        {
                            Rain.GetComponent<AudioSource>().Play();
                        }
                        m_fWeatherTimer = Random.Range(60.0f, 90.0f);
                    }
                }
                m_wbWeather.ApplyWeather(m_nCurrWeather);
            }
        }
    }
}
