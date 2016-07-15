using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIManager : MonoBehaviour
{
    public enum eManagerTypes { Hut, Nest };
    public eManagerTypes m_emtManagerType;
    public enum eNestTypes { Boar, HoneyBadger, Wolf };
    public eNestTypes m_entNestType;

    bool m_bIsCannibalSpawned;
    bool m_bIsAnimalSpawned;

    public GameObject m_gmDayTime;
    public float m_fMorningTime;
    public float m_fNightTime;

    public float m_fAssignRange;

    public GameObject m_goSpearman;
    public GameObject m_goArcher;
    public GameObject m_goScout;
    public GameObject m_goHeavyDuty;

    public GameObject m_goHoneyBadger;
    public GameObject m_goWolf;
    public GameObject m_goBoar;

    List<GameObject> m_lCannibals;
    List<GameObject> m_lAnimals;

    int m_nSpearmans;
    int m_nArchers;
    int m_nScouts;
    int m_nHeavyDuties;

    int m_nHoneyBadgers;
    int m_nBoars;
    int m_nWolves;

    int m_nCurrSpearmans;
    int m_nCurrArchers;
    int m_nCurrScouts;
    int m_nCurrHeavyDuties;

    int m_nCurrHoneyBadgers;
    int m_nCurrBoars;
    int m_nCurrWolves;

    float m_fCallingTimer;

    int m_nSpawnCounts;
    public float m_fSpawnWait;
    // Use this for initialization
    void Start()
    {
        m_bIsCannibalSpawned = false;
        m_bIsAnimalSpawned = true;
        m_gmDayTime = GameObject.Find("GameManager");
        m_lCannibals = new List<GameObject>();
        m_lAnimals = new List<GameObject>();
        m_nSpawnCounts = 0;
        switch (m_entNestType)
        {
            case eNestTypes.Boar:
                {
                    m_fSpawnWait = 0;
                    break;
                }
            case eNestTypes.HoneyBadger:
                {
                    m_fSpawnWait = 0.2f;
                    break;
                }
            case eNestTypes.Wolf:
                {
                    m_fSpawnWait = 0.40f;
                    break;
                }
        }
        switch (PlayerPrefs.GetInt("Difficulty"))
        {
            case 1:
                {
                    m_nSpearmans = 40;
                    m_nArchers = 35;
                    m_nScouts = 15;
                    m_nHeavyDuties = 15;
                    m_nHoneyBadgers = 70;
                    m_nBoars = 40;
                    m_nWolves = 100;
                }
                break;
            case 2:
                {
                    m_nSpearmans = 35;
                    m_nArchers = 35;
                    m_nScouts = 20;
                    m_nHeavyDuties = 15;
                    m_nHoneyBadgers = 70;
                    m_nBoars = 50;
                    m_nWolves = 90;
                }
                break;
            case 3:
                {
                    m_nSpearmans = 35;
                    m_nArchers = 30;
                    m_nScouts = 20;
                    m_nHeavyDuties = 20;
                    m_nHoneyBadgers = 70;
                    m_nBoars = 60;
                    m_nWolves = 80;
                }
                break;
            case 4:
                {
                    m_nSpearmans = 30;
                    m_nArchers = 25;
                    m_nScouts = 25;
                    m_nHeavyDuties = 25;
                    m_nHoneyBadgers = 70;
                    m_nBoars = 70;
                    m_nWolves = 70;
                }
                break;
            default:
                {
                    m_nSpearmans = 35;
                    m_nArchers = 35;
                    m_nScouts = 20;
                    m_nHeavyDuties = 15;
                    m_nHoneyBadgers = 70;
                    m_nBoars = 50;
                    m_nWolves = 90;
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        m_fSpawnWait -= Time.deltaTime;
        if (m_emtManagerType == eManagerTypes.Hut)
        {
            if ((int)m_gmDayTime.GetComponent<GameManager>().currentTime == (int)m_fMorningTime)
            {
                m_nCurrSpearmans = 0;
                m_nCurrArchers = 0;
                m_nCurrScouts = 0;
                m_nCurrHeavyDuties = 0;
                for (int i = 0; i < m_lCannibals.Count; i++)
                {
                    if (m_lCannibals != null)
                    {
                        switch (m_lCannibals[i].GetComponent<AIBehavior>().m_aitType)
                        {
                            case AIBehavior.AIType.Archer:
                                {
                                    m_nCurrArchers++;
                                    break;
                                }
                            case AIBehavior.AIType.HeavyDuty:
                                {
                                    m_nCurrHeavyDuties++;
                                    break;
                                }
                            case AIBehavior.AIType.Scout:
                                {
                                    m_nCurrScouts++;
                                    break;
                                }
                            case AIBehavior.AIType.Spearman:
                                {
                                    m_nCurrSpearmans++;
                                    break;
                                }
                        }
                    }
                    else
                    {
                        m_lCannibals.RemoveAt(i);
                        i--;
                    }
                }
                for(int i =0; i<100; i++)
                {
                    int nIndexA = Random.Range(0, m_lCannibals.Count - 1);
                    int nIndexB = Random.Range(0, m_lCannibals.Count - 1);
                    if (nIndexA != nIndexB)
                    {
                        GameObject goTemp = m_lCannibals[nIndexA];
                        m_lCannibals[nIndexA] = m_lCannibals[nIndexB];
                        m_lCannibals[nIndexB] = goTemp;
                    }
                }
                m_bIsCannibalSpawned = false;
            }

            if ((int)m_gmDayTime.GetComponent<GameManager>().currentTime > (int)m_fMorningTime && (int)m_gmDayTime.GetComponent<GameManager>().currentTime < (int)m_fNightTime &&
                !m_bIsCannibalSpawned)
            {
                if (m_nSpawnCounts < m_nSpearmans - m_nCurrSpearmans && m_fSpawnWait <= 0)
                {
                    GameObject goTemp = (GameObject)Instantiate(m_goSpearman, transform.position, m_goSpearman.transform.rotation);
                    ChangeToLizzard(goTemp);
                    goTemp.GetComponent<AIBehavior>().m_aitType = AIBehavior.AIType.Spearman;

                    Vector2 v2RandPt = Random.insideUnitCircle * m_fAssignRange;
                    v2RandPt.x += goTemp.transform.position.x;
                    v2RandPt.y += goTemp.transform.position.z;
                    NavMeshHit nmhHit;
                    NavMesh.SamplePosition(new Vector3(v2RandPt.x, goTemp.transform.position.y, v2RandPt.y), out nmhHit, 100f, NavMesh.AllAreas);
                    goTemp.GetComponent<AIBehavior>().m_v3TargetPt = nmhHit.position;
                    goTemp.GetComponent<AIBehavior>().m_v3AssignedPt = nmhHit.position;
                    if (!goTemp.GetComponent<AIBehavior>().IsNavMeshBlocked())
                    {
                        m_lCannibals.Add(goTemp);
                        m_nSpawnCounts++;
                        m_fSpawnWait = 0.4f;
                    }
                    else
                    {
                        goTemp.GetComponent<AIBehavior>().Death();
                    }
                }
                else if (m_nSpawnCounts >= m_nSpearmans - m_nCurrSpearmans && m_nSpawnCounts < (m_nSpearmans - m_nCurrSpearmans) + (m_nArchers - m_nCurrArchers) && m_fSpawnWait <= 0)
                {
                    GameObject goTemp = (GameObject)Instantiate(m_goArcher, transform.position, m_goArcher.transform.rotation);
                    ChangeToLizzard(goTemp);
                    goTemp.GetComponent<AIBehavior>().m_aitType = AIBehavior.AIType.Archer;
                    Vector2 v2RandPt = Random.insideUnitCircle * m_fAssignRange;
                    v2RandPt.x += goTemp.transform.position.x;
                    v2RandPt.y += goTemp.transform.position.z;
                    NavMeshHit nmhHit;
                    NavMesh.SamplePosition(new Vector3(v2RandPt.x, goTemp.transform.position.y, v2RandPt.y), out nmhHit, 100f, NavMesh.AllAreas);
                    goTemp.GetComponent<AIBehavior>().m_v3TargetPt = nmhHit.position;
                    goTemp.GetComponent<AIBehavior>().m_v3AssignedPt = nmhHit.position;
                    if (!goTemp.GetComponent<AIBehavior>().IsNavMeshBlocked())
                    {
                        m_lCannibals.Add(goTemp);
                        m_nSpawnCounts++;
                        m_fSpawnWait = 0.4f;
                    }
                    else
                    {
                        goTemp.GetComponent<AIBehavior>().Death();
                    }
                }

                else if (m_nSpawnCounts >= (m_nSpearmans - m_nCurrSpearmans) + (m_nArchers - m_nCurrArchers) &&
                    m_nSpawnCounts < (m_nSpearmans - m_nCurrSpearmans) + (m_nArchers - m_nCurrArchers) + (m_nScouts - m_nCurrScouts) && m_fSpawnWait <= 0)
                {
                    GameObject goTemp = (GameObject)Instantiate(m_goScout, transform.position, m_goScout.transform.rotation);
                    ChangeToLizzard(goTemp);
                    goTemp.GetComponent<AIBehavior>().m_aitType = AIBehavior.AIType.Scout;
                    Vector2 v2RandPt = Random.insideUnitCircle * m_fAssignRange;
                    v2RandPt.x += goTemp.transform.position.x;
                    v2RandPt.y += goTemp.transform.position.z;
                    NavMeshHit nmhHit;
                    NavMesh.SamplePosition(new Vector3(v2RandPt.x, goTemp.transform.position.y, v2RandPt.y), out nmhHit, 100f, NavMesh.AllAreas);
                    goTemp.GetComponent<AIBehavior>().m_v3TargetPt = nmhHit.position;
                    goTemp.GetComponent<AIBehavior>().m_v3AssignedPt = nmhHit.position;
                    if (!goTemp.GetComponent<AIBehavior>().IsNavMeshBlocked())
                    {
                        m_lCannibals.Add(goTemp);
                        m_nSpawnCounts++;
                        m_fSpawnWait = 0.4f;
                    }
                    else
                    {
                        goTemp.GetComponent<AIBehavior>().Death();
                    }
                }

                else if (m_nSpawnCounts >= (m_nSpearmans - m_nCurrSpearmans) + (m_nArchers - m_nCurrArchers) + (m_nScouts - m_nCurrScouts) &&
                    m_nSpawnCounts < (m_nSpearmans - m_nCurrSpearmans) + (m_nArchers - m_nCurrArchers) + (m_nScouts - m_nCurrScouts) + (m_nHeavyDuties - m_nCurrHeavyDuties) &&
                    m_fSpawnWait <= 0)
                {

                    GameObject goTemp = (GameObject)Instantiate(m_goHeavyDuty, transform.position, m_goHeavyDuty.transform.rotation);
                    ChangeToLizzard(goTemp);
                    goTemp.GetComponent<AIBehavior>().m_aitType = AIBehavior.AIType.HeavyDuty;
                    Vector2 v2RandPt = Random.insideUnitCircle * m_fAssignRange;
                    v2RandPt.x += goTemp.transform.position.x;
                    v2RandPt.y += goTemp.transform.position.z;
                    NavMeshHit nmhHit;
                    NavMesh.SamplePosition(new Vector3(v2RandPt.x, goTemp.transform.position.y, v2RandPt.y), out nmhHit, 100f, NavMesh.AllAreas);
                    goTemp.GetComponent<AIBehavior>().m_v3TargetPt = nmhHit.position;
                    goTemp.GetComponent<AIBehavior>().m_v3AssignedPt = nmhHit.position;
                    if (!goTemp.GetComponent<AIBehavior>().IsNavMeshBlocked())
                    {
                        m_lCannibals.Add(goTemp);
                        m_nSpawnCounts++;
                        m_fSpawnWait = 0.4f;
                    }
                    else
                    {
                        goTemp.GetComponent<AIBehavior>().Death();
                    }
                }
                else if (m_lCannibals.Count == m_nSpearmans + m_nArchers + m_nScouts + m_nHeavyDuties)
                {
                    m_nSpawnCounts = 0;
                    m_bIsCannibalSpawned = true;
                }
            }

            if (m_gmDayTime.GetComponent<GameManager>().currentTime >= m_fNightTime)
            {
                m_fCallingTimer -= Time.deltaTime;
                if (m_fCallingTimer <= 0)
                {
                    m_nCurrSpearmans = 0;
                    m_nCurrArchers = 0;
                    m_nCurrScouts = 0;
                    m_nCurrHeavyDuties = 0;
                    for (int i = 0; i < m_lCannibals.Count; i++)
                    {
                        if (m_lCannibals[i] != null)
                        {
                            switch (m_lCannibals[i].GetComponent<AIBehavior>().m_aitType)
                            {
                                case AIBehavior.AIType.Archer:
                                    {
                                        if (m_nCurrArchers <= m_nArchers * 0.6)
                                            m_nCurrArchers++;
                                        else
                                        {
                                            m_lCannibals[i].GetComponent<AIBehavior>().m_v3TargetPt = transform.position;
                                            if ((m_lCannibals[i].transform.position - transform.position).magnitude <= 10.0f)
                                            {
                                                m_lCannibals[i].GetComponent<AIBehavior>().Death();
                                                m_lCannibals.RemoveAt(i);
                                            }
                                        }
                                        break;
                                    }
                                case AIBehavior.AIType.HeavyDuty:
                                    {
                                        if (m_nCurrHeavyDuties <= m_nHeavyDuties * 0.6)
                                            m_nCurrHeavyDuties++;
                                        else
                                        {
                                            m_lCannibals[i].GetComponent<AIBehavior>().m_v3TargetPt = transform.position;
                                            if ((m_lCannibals[i].transform.position - transform.position).magnitude <= 10.0f)
                                            {
                                                m_lCannibals[i].GetComponent<AIBehavior>().Death();
                                                m_lCannibals.RemoveAt(i);
                                            }
                                        }
                                        break;
                                    }
                                case AIBehavior.AIType.Scout:
                                    {
                                        if (m_nCurrScouts <= m_nScouts * 0.6)
                                            m_nCurrScouts++;
                                        else
                                        {
                                            m_lCannibals[i].GetComponent<AIBehavior>().m_v3TargetPt = transform.position;
                                            if ((m_lCannibals[i].transform.position - transform.position).magnitude <= 10.0f)
                                            {
                                                m_lCannibals[i].GetComponent<AIBehavior>().Death();
                                                m_lCannibals.RemoveAt(i);
                                            }
                                        }
                                        break;
                                    }
                                case AIBehavior.AIType.Spearman:
                                    {
                                        if (m_nCurrSpearmans <= m_nSpearmans * 0.6)
                                            m_nCurrSpearmans++;
                                        else
                                        {
                                            m_lCannibals[i].GetComponent<AIBehavior>().m_v3TargetPt = transform.position;
                                            if ((m_lCannibals[i].transform.position - transform.position).magnitude <= 10.0f)
                                            {
                                                m_lCannibals[i].GetComponent<AIBehavior>().Death();
                                                m_lCannibals.RemoveAt(i);
                                            }
                                        }
                                        break;
                                    }
                            }

                        }
                    }
                    m_fCallingTimer = 2;
                }
            }


        }
        else
        {
            if ((int)m_gmDayTime.GetComponent<GameManager>().currentTime == (int)m_fNightTime)
            {
                m_nCurrBoars = 0;
                m_nCurrHoneyBadgers = 0;
                m_nCurrWolves = 0;
                for (int i = 0; i < m_lAnimals.Count; i++)
                {
                    if (m_lAnimals[i] != null)
                    {
                        switch (m_lAnimals[i].GetComponent<AIBehavior>().m_aitType)
                        {
                            case AIBehavior.AIType.Boar:
                                {
                                    m_nCurrBoars++;
                                    break;
                                }
                            case AIBehavior.AIType.HoneyBadger:
                                {
                                    m_nCurrHoneyBadgers++;
                                    break;
                                }
                            case AIBehavior.AIType.Wolf:
                                {
                                    m_nCurrWolves++;
                                    break;
                                }
                        }
                    }
                    else
                    {
                        m_lAnimals.RemoveAt(i);
                        i--;
                    }
                }
                for (int i = 0; i < 100; i++)
                {
                    int nIndexA = Random.Range(0, m_lAnimals.Count - 1);
                    int nIndexB = Random.Range(0, m_lAnimals.Count - 1);
                    if (nIndexA != nIndexB)
                    {
                        GameObject goTemp = m_lAnimals[nIndexA];
                        m_lAnimals[nIndexA] = m_lAnimals[nIndexB];
                        m_lAnimals[nIndexB] = goTemp;
                    }
                }
                m_bIsAnimalSpawned = false;
            }

            if (((int)m_gmDayTime.GetComponent<GameManager>().currentTime < (int)m_fMorningTime || (int)m_gmDayTime.GetComponent<GameManager>().currentTime > (int)m_fNightTime) &&
                !m_bIsAnimalSpawned)
            {
                if (m_entNestType == eNestTypes.Boar && m_nSpawnCounts < m_nBoars - m_nCurrBoars && m_fSpawnWait <= 0)
                {
                    GameObject goTemp = (GameObject)Instantiate(m_goBoar, transform.position, m_goBoar.transform.rotation);
                    goTemp.GetComponent<AIBehavior>().m_aitType = AIBehavior.AIType.Boar;
                    Vector2 v2RandPt = Random.insideUnitCircle * m_fAssignRange;
                    v2RandPt.x += goTemp.transform.position.x;
                    v2RandPt.y += goTemp.transform.position.z;
                    NavMeshHit nmhHit;
                    NavMesh.SamplePosition(new Vector3(v2RandPt.x, goTemp.transform.position.y, v2RandPt.y), out nmhHit, 100f, NavMesh.AllAreas);
                    goTemp.GetComponent<AIBehavior>().m_v3TargetPt = nmhHit.position;
                    goTemp.GetComponent<AIBehavior>().m_v3AssignedPt = nmhHit.position;
                    if (!goTemp.GetComponent<AIBehavior>().IsNavMeshBlocked())
                    {
                        m_lCannibals.Add(goTemp);
                        m_nSpawnCounts++;
                        m_fSpawnWait = 0.6f;
                    }
                    else
                    {
                        goTemp.GetComponent<AIBehavior>().Death();
                    }
                }
                if (m_entNestType == eNestTypes.HoneyBadger && m_nSpawnCounts < m_nHoneyBadgers - m_nCurrHoneyBadgers && m_fSpawnWait <= 0)
                {
                    GameObject goTemp = (GameObject)Instantiate(m_goHoneyBadger, transform.position, m_goHoneyBadger.transform.rotation);
                    goTemp.GetComponent<AIBehavior>().m_aitType = AIBehavior.AIType.HoneyBadger;
                    Vector2 v2RandPt = Random.insideUnitCircle * m_fAssignRange;
                    v2RandPt.x += goTemp.transform.position.x;
                    v2RandPt.y += goTemp.transform.position.z;
                    NavMeshHit nmhHit;
                    NavMesh.SamplePosition(new Vector3(v2RandPt.x, goTemp.transform.position.y, v2RandPt.y), out nmhHit, 100f, NavMesh.AllAreas);
                    goTemp.GetComponent<AIBehavior>().m_v3TargetPt = nmhHit.position;
                    goTemp.GetComponent<AIBehavior>().m_v3AssignedPt = nmhHit.position;
                    if (!goTemp.GetComponent<AIBehavior>().IsNavMeshBlocked())
                    {
                        m_lCannibals.Add(goTemp);
                        m_nSpawnCounts++;
                        m_fSpawnWait = 0.6f;
                    }
                    else
                    {
                        goTemp.GetComponent<AIBehavior>().Death();
                    }
                }
                if (m_entNestType == eNestTypes.Wolf && m_nSpawnCounts < m_nWolves - m_nCurrWolves && m_fSpawnWait <= 0)
                {
                    GameObject goTemp = (GameObject)Instantiate(m_goWolf, transform.position, m_goWolf.transform.rotation);
                    goTemp.GetComponent<AIBehavior>().m_aitType = AIBehavior.AIType.Wolf;
                    Vector2 v2RandPt = Random.insideUnitCircle * m_fAssignRange;
                    v2RandPt.x += goTemp.transform.position.x;
                    v2RandPt.y += goTemp.transform.position.z;
                    NavMeshHit nmhHit;
                    NavMesh.SamplePosition(new Vector3(v2RandPt.x, goTemp.transform.position.y, v2RandPt.y), out nmhHit, 100f, NavMesh.AllAreas);
                    goTemp.GetComponent<AIBehavior>().m_v3TargetPt = nmhHit.position;
                    goTemp.GetComponent<AIBehavior>().m_v3AssignedPt = nmhHit.position;
                    if (!goTemp.GetComponent<AIBehavior>().IsNavMeshBlocked())
                    {
                        m_lCannibals.Add(goTemp);
                        m_nSpawnCounts++;
                        m_fSpawnWait = 0.6f;
                    }
                    else
                    {
                        goTemp.GetComponent<AIBehavior>().Death();
                    }
                }

                if (m_lAnimals.Count == m_nBoars + m_nHoneyBadgers + m_nWolves)
                {
                    m_nSpawnCounts = 0;
                    m_bIsAnimalSpawned = true;
                }
            }


            if (m_gmDayTime.GetComponent<GameManager>().currentTime >= m_fMorningTime && m_gmDayTime.GetComponent<GameManager>().currentTime < m_fNightTime)
            {
                m_fCallingTimer -= Time.deltaTime;
                if (m_fCallingTimer <= 0)
                {
                    m_nCurrBoars = 0;
                    m_nCurrHoneyBadgers = 0;
                    m_nCurrWolves = 0;
                    for (int i = 0; i < m_lAnimals.Count; i++)
                    {
                        if (m_lAnimals[i] != null)
                        {
                            switch (m_lAnimals[i].GetComponent<AIBehavior>().m_aitType)
                            {
                                case AIBehavior.AIType.Boar:
                                    {
                                        if (m_nCurrBoars <= m_nBoars * 0.6)
                                            m_nCurrBoars++;
                                        else
                                        {
                                            m_lAnimals[i].GetComponent<AIBehavior>().m_v3TargetPt = transform.position;
                                            if ((m_lAnimals[i].transform.position - transform.position).magnitude <= 10.0f)
                                            {
                                                m_lAnimals[i].GetComponent<AIBehavior>().Death();
                                                m_lAnimals.RemoveAt(i);
                                            }
                                        }
                                        break;
                                    }
                                case AIBehavior.AIType.HoneyBadger:
                                    {
                                        if (m_nCurrHoneyBadgers <= m_nHoneyBadgers * 0.6)
                                            m_nCurrHoneyBadgers++;
                                        else
                                        {
                                            m_lAnimals[i].GetComponent<AIBehavior>().m_v3TargetPt = transform.position;
                                            if ((m_lAnimals[i].transform.position - transform.position).magnitude <= 10.0f)
                                            {
                                                m_lAnimals[i].GetComponent<AIBehavior>().Death();
                                                m_lAnimals.RemoveAt(i);
                                            }
                                        }
                                        break;
                                    }
                                case AIBehavior.AIType.Wolf:
                                    {
                                        if (m_nCurrWolves <= m_nWolves * 0.6)
                                            m_nCurrWolves++;
                                        else
                                        {
                                            m_lAnimals[i].GetComponent<AIBehavior>().m_v3TargetPt = transform.position;
                                            if ((m_lAnimals[i].transform.position - transform.position).magnitude <= 10.0f)
                                            {
                                                m_lAnimals[i].GetComponent<AIBehavior>().Death();
                                                m_lAnimals.RemoveAt(i);
                                            }
                                        }
                                        break;
                                    }
                            }

                        }
                    }
                    m_fCallingTimer = 2;
                }
            }
        }

        if(m_fSpawnWait<=0)
        {
            m_fSpawnWait = 0.20f;
        }
    }

    void ChangeToLizzard(GameObject temp)
    {
        //PlayerPrefs.GetInt()
        if (Application.loadedLevel == 3)
        {
            temp.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.green;
        }
        else if (Application.loadedLevel == 4)
        {
            temp.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.black;
        }
    }
}
