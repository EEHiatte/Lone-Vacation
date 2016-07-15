using UnityEngine;
using System.Collections;
using UnityEngine.UI;

struct PlayerStats
{
    public float thirstDecay;
    //public float visibility;
    public float staminaDecay;
}

struct FireStats
{
    public float decayRate;
    public float regenRate;
}

struct EnemyStats
{
    public float visionRange;
}

public class WeatherBehavior : MonoBehaviour
{
    GameObject player;
    PlayerStats originalPlayerStats;
    GameObject fire;
    FireStats originalFireStats;
    GameObject[] enemies;
    EnemyStats originalEnemyStats;

    GameObject rainEffect;
    GameObject screenTint;

    // Use this for initialization
    void Start()
    {
        RefreshEntities();
        RefreshPlayerStats();
        RefreshFireStats();
        RefreshEnemyStats();
        rainEffect = GameObject.Find("RainEffect");
        screenTint = GameObject.Find("ScreenTint");
        ApplyWeather(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RefreshEntities()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fire = GameObject.Find("Fire Pit");
        enemies = GameObject.FindGameObjectsWithTag("Cannibal");
    }

    public void RefreshPlayerStats()
    {
        originalPlayerStats.thirstDecay = player.GetComponent<PlayerStatManager>().thirstDecayRate;
        originalPlayerStats.staminaDecay = player.GetComponent<PlayerController>().staminaDecreaseRate;

    }

    public void RefreshFireStats()
    {
        originalFireStats.decayRate = fire.GetComponent<FireBehavior>().decayRate;
        originalFireStats.regenRate = fire.GetComponent<FireBehavior>().regenAmount;
    }

    public void RefreshEnemyStats()
    {
        if (enemies.Length == 0)
            return;
        originalEnemyStats.visionRange = enemies[0].GetComponent<AIBehavior>().m_fVisionRadius; // Probably rubbish
    }

    // Function used to hopefully prevent the issue of weathers stacking on each other
    void ChangeInWeather()
    {
        ResetStats();
        RefreshEntities();
        RefreshPlayerStats();
        RefreshFireStats();
        RefreshEnemyStats();
        if(rainEffect != null)
            rainEffect.GetComponent<ToggleActive>().SetInactiveState();
    }

    // Resets all stats to the stored originals
    void ResetStats()
    {
        if (player != null)
        {
            player.GetComponent<PlayerStatManager>().thirstDecayRate = originalPlayerStats.thirstDecay;     // Resets player's thirst decay
            player.GetComponent<PlayerController>().staminaDecreaseRate = originalPlayerStats.staminaDecay; // Resets player's stamina decay
        }

        if (fire != null)
        {
            fire.GetComponent<FireBehavior>().decayRate = originalFireStats.decayRate;                      // Resets fire's decay
            fire.GetComponent<FireBehavior>().regenAmount = originalFireStats.regenRate;                    // Resets fire regeneration amount
        }

        if (enemies != null)
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<AIBehavior>().m_fVisionRadius = originalEnemyStats.visionRange;          // Resets all enemies' vision range
            }
        }
    }

    public void ApplyWeather(int type)
    {
        ChangeInWeather();

        switch (type)
        {
            case 0:
                {
                    // Clear Weather ( No Weather )
                    ResetStats();
                    Color color = new Color(0, 0, 0, 0);
                    if(screenTint != null)
                        screenTint.GetComponent<RawImage>().color = color;
                }
                break;
            case 1:
                {
                    // Cold Weather
                    player.GetComponent<PlayerStatManager>().thirstDecayRate /= 2;
                    player.GetComponent<PlayerController>().staminaDecreaseRate /= 2;
                    fire.GetComponent<FireBehavior>().decayRate *= 2;
                    fire.GetComponent<FireBehavior>().regenAmount /= 2;
                    Color color = new Color(60f / 255f, 60f / 255f, 139f / 255f, 70f / 255f);
                    screenTint.GetComponent<RawImage>().color = color;
                }
                break;
            case 2:
                {
                    // Hot Weather
                    player.GetComponent<PlayerStatManager>().thirstDecayRate *= 2;
                    player.GetComponent<PlayerController>().staminaDecreaseRate *= 2;
                    fire.GetComponent<FireBehavior>().decayRate /= 2;
                    Color color = new Color(255f / 255f, 31f / 255f, 0, 75f / 255f);
                    screenTint.GetComponent<RawImage>().color = color;
                }
                break;
            case 3:
                {
                    // Rain Weather
                    player.GetComponent<PlayerStatManager>().thirstDecayRate /= 2;
                    fire.GetComponent<FireBehavior>().decayRate *= 2;
                    foreach (GameObject enemy in enemies)
                    {
                        enemy.GetComponent<AIBehavior>().m_fVisionRadius /= 2;
                    }
                    rainEffect.GetComponent<ToggleActive>().SetActiveState();
                    Color color = new Color(0, 0, 36f / 255f, 79f / 255f);
                    screenTint.GetComponent<RawImage>().color = color;
                }
                break;
            default:
                break;
        }
    }
}
