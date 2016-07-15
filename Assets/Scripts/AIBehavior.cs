using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIBehavior : MonoBehaviour
{
    public AudioClip m_acAttackAudio;
    public AudioClip m_acCalling;
    //AudioSource m_asOwAudio;
    AudioSource m_asAttackAudio;
    AudioSource m_asCalling;

    NavMeshAgent m_nmaTarget;
    public List<GameObject> m_lMark; // To hold the mark pop above Cannibal's head
    //Animator m_aAnimatior;

    public enum AIType { Spearman, Archer, Scout, HeavyDuty, HoneyBadger, Boar, Wolf };
    public AIType m_aitType;
    public GameObject m_goTarget;
    public GameObject m_goQuestionMark;
    public GameObject m_goExclamationMark;

    public GameObject m_goProjectile;

    public Vector3 m_v3LastSeenPt;
    public Vector3 m_v3AssignedPt;
    public Vector3 m_v3Position;
    public Vector3 m_v3TargetPt;

    public float m_fSlowTimer;
    public float m_fStunTimer;
    public float m_fMovement;
    public float m_fAttackRadius;
    public float m_fDetectRadius;
    public float m_fVisionRadius;
    public float m_fPatrolRadius;
    public float m_fAttackCD;

    public float m_fAttackDamage;
    public float m_fHealth;

    int m_nSearchChance;
    int m_nDirection;

    float m_fOriginalMovement;
    float m_fStandTimer;
    float m_fAttackTimer;

    bool m_bSeenEnemyRecently;

    //For Boar;
    public float m_fChargeCD;
    public float m_fChargeTime;
    float m_fChargeCDTimer;
    float m_fChargeTimer;
    bool m_bIsCharging;
    List<GameObject> m_lChargedTargets;


    // For Scout;
    public float m_fCallingRadius;
    public float m_fCallingCD;
    public float m_fCallingTimer;
    public float m_fStalkerRange;
    bool m_bIsCalling;

    //CutDownUpdate
    public float m_fUpdateTimer;
    //int m_nNavMeshTries;
    Animator m_aAnimator;
    bool m_bAttacking = false;
    bool m_bTrapped = false;

    void Awake()
    {
        m_nmaTarget = GetComponent<NavMeshAgent>();
    }

    // Use this for initialization
    void Start()
    {
        //m_asOwAudio = GetComponent<AudioSource>();
        m_asAttackAudio = gameObject.AddComponent<AudioSource>();
        m_asAttackAudio.clip = m_acAttackAudio;
        m_asAttackAudio.spatialBlend = 1;
        m_asCalling = gameObject.AddComponent<AudioSource>();
        m_asCalling.clip = m_acCalling;
        m_asCalling.spatialBlend = 1;
        m_fStandTimer = 0;
        m_fAttackTimer = 0;
        m_fChargeCDTimer = 0;
        m_fChargeTimer = m_fChargeTime;

        m_bSeenEnemyRecently = false;
        m_v3Position = transform.position;
        m_v3LastSeenPt = transform.position;

        m_nmaTarget.SetDestination(m_v3TargetPt);
        m_nmaTarget.speed = m_fMovement;
        m_fOriginalMovement = m_fMovement;
        m_nSearchChance = 100;

        m_lMark = new List<GameObject>();
        m_lChargedTargets = new List<GameObject>();
        m_aAnimator = GetComponentInChildren<Animator>();

        m_bIsCalling = false;
        //m_nNavMeshTries=0;
        //Update cut down set up
        m_fUpdateTimer = 7;

        /// Keith's Code! This determines the damage that enemies do based on dificulty!
        /// 
        switch (PlayerPrefs.GetInt("Difficulty"))
        {
            case 1:
                m_fAttackDamage *= .5f;
                break;
            case 2:
                m_fAttackDamage *= 1f;
                break;
            case 3:
                m_fAttackDamage *= 2f;
                break;
            case 4:
                m_fAttackDamage *= 4f;
                break;
            default:
                m_fAttackDamage *= 1f;
                break;
        }
        //////////////
    }

    // Update is called once per frame
    void Update()
    {

        if ((int)m_fUpdateTimer<7 && m_fUpdateTimer>4)
        {
            m_fMovement = 30;
        }
        if ((int)m_fUpdateTimer == 4)
            GetComponent<CapsuleCollider>().isTrigger = true;
        m_v3Position = transform.position;
        if (m_fHealth <= 0)
        {
            Death();
            return;
        }
        m_fCallingTimer -= Time.deltaTime;
        m_fAttackTimer -= Time.deltaTime;
        m_fStandTimer -= Time.deltaTime;
        m_fSlowTimer -= Time.deltaTime;
        m_fStunTimer -= Time.deltaTime;
        m_fChargeTimer -= Time.deltaTime;
        m_fChargeCDTimer -= Time.deltaTime;

        m_fUpdateTimer -= Time.deltaTime;

        m_nmaTarget.SetDestination(m_v3TargetPt);
        //Debug.DrawLine(m_v3Position, m_nmaTarget.destination, Color.cyan);
        m_nmaTarget.speed = m_fMovement;

        if (m_fUpdateTimer<=0)
        {
        if (m_aitType == AIType.Boar)
        {
            if (m_goTarget != null && m_fChargeCDTimer <= 0)
            {
                if (m_lMark.Count != 0)
                {
                    Destroy(m_lMark[0]);
                    m_lMark.Clear();
                    GameObject goMark = (GameObject)Instantiate(m_goExclamationMark, new Vector3(m_v3Position.x, m_v3Position.y + 2, m_v3Position.z), new Quaternion(0, 0, 0, 0));
                    m_lMark.Add(goMark);
                }
                else
                {
                    GameObject goMark = (GameObject)Instantiate(m_goExclamationMark, new Vector3(m_v3Position.x, m_v3Position.y + 2, m_v3Position.z), new Quaternion(0, 0, 0, 0));
                    m_lMark.Add(goMark);
                }

                if (m_bIsCharging == true)
                {
                    if (m_fChargeTimer <= 0)
                    {
                        Move();
                        ChargeAoE();
                        if ((m_v3Position - m_v3TargetPt).magnitude <= 0.1f)
                        {
                            m_fAttackTimer = m_fAttackCD;
                            m_v3TargetPt = m_v3Position;
                            m_fMovement = m_fOriginalMovement;
                            m_fChargeCDTimer = m_fChargeCD;
                            m_fChargeTimer = m_fChargeTime;
                            m_bIsCharging = false;
                        }
                    }
                    else
                    {
                        Stop();
                        m_fChargeTimer -= Time.deltaTime;
                    }
                    return;
                }
                else
                {
                    Chase();
                    if (SetCharge() == true)
                    {
                        return;
                    }
                }

            }
        }

        if (m_bSeenEnemyRecently)
        {
            if (m_goTarget != null)
                Chase();
            else
                Search();
        }
        else
        {
            Patrol();
        }

        if (m_fStandTimer > 0)
            Stop();
        else
            Move();

        if (m_goTarget != null)
        {
            m_fStandTimer = 0;
            Move();
        }

        Check();
        m_v3Position = transform.position;

        if (m_goTarget != null)
        {
            m_fStandTimer = 0;
            if (m_aitType == AIType.Scout || m_aitType == AIType.Wolf)
            {
                if (m_fCallingTimer <= 0)
                {
                    m_bIsCalling = Reinforcement();
                }
                if (m_bIsCalling)
                {
                    if ((m_v3Position - m_goTarget.transform.position).magnitude > m_fAttackRadius)
                        Move();
                    else
                    {
                        Stop();
                        if (m_fAttackTimer <= 0)
                            Attack();
                    }
                }
                else
                {
                    if ((m_v3Position - m_goTarget.transform.position).magnitude > m_fStalkerRange)
                        Move();
                    else
                    {
                        Stop();
                    }
                }
            }
            else
            {
                if (m_aitType == AIType.HeavyDuty && (m_v3Position - m_goTarget.transform.position).magnitude > m_fAttackRadius * 3 && m_fChargeCDTimer <= 0 && m_fAttackTimer <= 0)
                {
                    Stop();
                    BoulderThrowing();
                }
                else if ((m_v3Position - m_goTarget.transform.position).magnitude > m_fAttackRadius)
                    Move();
                else
                {
                    Stop();
                    if (m_fAttackTimer <= 0)
                    {
                        Attack();
                    }
                }
            }
        }

        if (m_fAttackTimer < 0.1f)
        {
            m_bAttacking = false;
        }

        if (m_fSlowTimer <= 0.0f)
            m_fMovement = m_fOriginalMovement;

        if (m_fStandTimer > 0)
            Stop();
        if (m_fStunTimer > 0.0f)
        {
            m_bTrapped = true;
            Stop();
        }
        else
        {
            m_bTrapped = false;
        }

        Animate();

        m_fUpdateTimer = 4.0f;
    }
    }

    void Stop()
    {
        m_nmaTarget.enabled = true;
        m_nmaTarget.Stop();
    }
    void Move()
    {
        m_nmaTarget.enabled=true;
        m_nmaTarget.Resume();
    }

    void Patrol()
    {
        if ((m_v3Position - m_v3TargetPt).magnitude <= 0.1f)    // if the AI is at target point
        {
            if (m_fStandTimer <= 0) // Stand time up
            {
                //Generate a random point in range
                Vector2 v2RandPt = new Vector2();
                while ((m_v3Position - m_v3TargetPt).magnitude < m_fPatrolRadius * 0.5 || IsNavMeshBlocked())
                {
                    v2RandPt = Random.insideUnitCircle * m_fPatrolRadius;
                    v2RandPt.x += m_v3AssignedPt.x;
                    v2RandPt.y += m_v3AssignedPt.z;
                    NavMeshHit nmhHit;
                    NavMesh.SamplePosition(new Vector3(v2RandPt.x, m_v3Position.y, v2RandPt.y), out nmhHit, 40f, m_nmaTarget.areaMask);
                    m_v3TargetPt = nmhHit.position;
                }
                m_fStandTimer = (float)(Random.Range(2, 5));
            }
        }
    }

    void Chase()
    {
        m_v3TargetPt = m_goTarget.transform.position;
        if ((m_v3Position - m_goTarget.transform.position).magnitude > m_fVisionRadius) //When Target is out of range
        {
            m_v3LastSeenPt = new Vector3(m_goTarget.transform.position.x, transform.position.y, m_goTarget.transform.position.z);
            m_v3TargetPt = m_v3LastSeenPt;
            m_goTarget = null;
            if (m_lMark.Count != 0)
            {
                Destroy(m_lMark[0]);
                m_lMark.Clear();
                GameObject goMark = (GameObject)Instantiate(m_goQuestionMark, new Vector3(m_v3Position.x, m_v3Position.y + 2, m_v3Position.z), new Quaternion(0, 0, 0, 0));
                m_lMark.Add(goMark);
            }
        }
        if (IsNavMeshBlocked()) //When Target is not longer reachable
        {
            m_v3LastSeenPt = m_v3AssignedPt;
            m_v3TargetPt = m_v3LastSeenPt;
            if (m_lMark.Count != 0)
            {
                Destroy(m_lMark[0]);
                m_lMark.Clear();
            }
            m_goTarget = null;
        }
    }

    void Attack()
    {
        m_bAttacking = true;
        if (m_aitType == AIType.Archer)
        {
            GameObject goArrow = (GameObject)Instantiate(m_goProjectile, new Vector3(m_v3Position.x, m_v3Position.y + 0.5f, m_v3Position.z), new Quaternion(0, 0, 0, 0));
            goArrow.transform.rotation = Quaternion.Euler(new Vector3(45f, 0, -CalculateAngleToTarget(m_v3TargetPt)));
            goArrow.GetComponent<Arrow>().m_goOwner = gameObject;
            goArrow.GetComponent<Arrow>().m_v3TargetPt = new Vector3(m_v3TargetPt.x, m_v3TargetPt.y + 0.5f, m_v3TargetPt.z);
        }

        //Damage Script here
        else
        {
            if (m_goTarget.tag == "Player")
            {
                m_goTarget.GetComponent<PlayerStatManager>().DecreaseHealth((float)m_fAttackDamage);    //  Damage



                ////////KEITH'S CODE
                /// checks if te player's health is less than or equal to zero
                if (m_goTarget.GetComponent<PlayerStatManager>().playerHealth <= 0)
                {
                    /// turns on the cooresponding achievement based on the ai's type
                    switch (m_aitType)
                    {
                        case AIType.Spearman: PlayerPrefs.SetInt("Death 1", 1);
                            break;
                        case AIType.Scout: PlayerPrefs.SetInt("Death 3", 1);
                            break;
                        case AIType.HeavyDuty: PlayerPrefs.SetInt("Death 4", 1);
                            break;
                        case AIType.HoneyBadger: PlayerPrefs.SetInt("Death 7", 1);
                            break;
                        case AIType.Boar: PlayerPrefs.SetInt("Death 6", 1);
                            break;
                        case AIType.Wolf: PlayerPrefs.SetInt("Death 5", 1);
                            break;
                        default:
                            break;
                    }
                    PlayerPrefs.Save();
                }
                //////////END OF KEITH'S CODE


                //Pop out of hiding script here
                if (m_goTarget.GetComponent<PlayerController>().currentlyHidingOrResting)
                {
                    m_goTarget.GetComponent<PlayerController>().anObject.gameObject.GetComponent<HidingObjectBehavior>().Hide(m_goTarget.transform);
                    m_goTarget.GetComponent<PlayerController>().currentlyHidingOrResting = false;
                }
            }
            else if (m_goTarget.tag == "Animal" || m_goTarget.gameObject.tag == "Cannibal")
            {
                m_goTarget.GetComponent<AIBehavior>().m_fHealth -= m_fAttackDamage;
            }
        }
        m_asAttackAudio.Play();
        m_fAttackTimer = m_fAttackCD;
    }

    void Check()
    {
        Collider[] colliderAround = Physics.OverlapSphere(m_v3Position, m_fVisionRadius); // Get all the collider in range.
        bool bFirstRun = true;
        for (int i = 0; i < colliderAround.Length; i++)
        {
            if (colliderAround[i].gameObject.tag == "Player" || colliderAround[i].gameObject.tag == "Animal" || colliderAround[i].gameObject.tag == "Cannibal" ||
                (colliderAround[i].gameObject.tag == "Hiding Objects" && colliderAround[i].GetComponent<HidingObjectBehavior>().currentlyHiding == true))
            {
                if (colliderAround[i].gameObject.tag != gameObject.tag)
                {
                    if (bFirstRun)
                    {
                        if (colliderAround[i].gameObject.tag == "Hiding Objects")
                        {
                            m_goTarget = colliderAround[i].GetComponent<HidingObjectBehavior>().player;
                            if (m_lMark.Count != 0)
                            {
                                Destroy(m_lMark[0]);
                                m_lMark.Clear();
                                GameObject goMark = (GameObject)Instantiate(m_goExclamationMark, new Vector3(m_v3Position.x, m_v3Position.y + 2, m_v3Position.z), new Quaternion(0, 0, 0, 0));
                                m_lMark.Add(goMark);
                            }
                            else
                            {
                                GameObject goMark = (GameObject)Instantiate(m_goExclamationMark, new Vector3(m_v3Position.x, m_v3Position.y + 2, m_v3Position.z), new Quaternion(0, 0, 0, 0));
                                m_lMark.Add(goMark);
                            }
                        }
                        else
                        {
                            m_goTarget = colliderAround[i].gameObject;
                            if (m_goTarget.tag == "Player")
                            {
                                if (m_lMark.Count != 0)
                                {
                                    Destroy(m_lMark[0]);
                                    m_lMark.Clear();
                                    GameObject goMark = (GameObject)Instantiate(m_goExclamationMark, new Vector3(m_v3Position.x, m_v3Position.y + 2, m_v3Position.z), new Quaternion(0, 0, 0, 0));
                                    m_lMark.Add(goMark);
                                }
                                else
                                {
                                    GameObject goMark = (GameObject)Instantiate(m_goExclamationMark, new Vector3(m_v3Position.x, m_v3Position.y + 2, m_v3Position.z), new Quaternion(0, 0, 0, 0));
                                    m_lMark.Add(goMark);
                                }
                            }
                            else
                            {
                                if (m_lMark.Count != 0)
                                {
                                    Destroy(m_lMark[0]);
                                    m_lMark.Clear();
                                }
                            }
                        }
                        m_bSeenEnemyRecently = true;
                        bFirstRun = false;
                    }
                    else
                    {
                        if ((m_goTarget.transform.position - m_v3Position).magnitude < (colliderAround[i].transform.position - m_v3Position).magnitude)
                        {
                            if (colliderAround[i].gameObject.tag == "Hiding Objects")
                            {
                                m_goTarget = colliderAround[i].GetComponent<HidingObjectBehavior>().player;
                                if (m_lMark.Count != 0)
                                {
                                    Destroy(m_lMark[0]);
                                    m_lMark.Clear();
                                    GameObject goMark = (GameObject)Instantiate(m_goExclamationMark, new Vector3(m_v3Position.x, m_v3Position.y + 2, m_v3Position.z), new Quaternion(0, 0, 0, 0));
                                    m_lMark.Add(goMark);
                                }
                                else
                                {
                                    GameObject goMark = (GameObject)Instantiate(m_goExclamationMark, new Vector3(m_v3Position.x, m_v3Position.y + 2, m_v3Position.z), new Quaternion(0, 0, 0, 0));
                                    m_lMark.Add(goMark);
                                }
                            }
                            else
                            {
                                m_goTarget = colliderAround[i].gameObject;
                                if (m_goTarget.tag == "Player")
                                {
                                    if (m_lMark.Count != 0)
                                    {
                                        Destroy(m_lMark[0]);
                                        m_lMark.Clear();
                                        GameObject goMark = (GameObject)Instantiate(m_goExclamationMark, new Vector3(m_v3Position.x, m_v3Position.y + 2, m_v3Position.z), new Quaternion(0, 0, 0, 0));
                                        m_lMark.Add(goMark);
                                    }
                                    else
                                    {
                                        GameObject goMark = (GameObject)Instantiate(m_goExclamationMark, new Vector3(m_v3Position.x, m_v3Position.y + 2, m_v3Position.z), new Quaternion(0, 0, 0, 0));
                                        m_lMark.Add(goMark);
                                    }
                                }
                                else
                                {
                                    if (m_lMark.Count != 0)
                                    {
                                        Destroy(m_lMark[0]);
                                        m_lMark.Clear();
                                    }
                                }
                            }
                            m_bSeenEnemyRecently = true;
                        }
                    }
                }
            }
        }
    }

    void Search()
    {
        if ((m_v3Position - m_v3TargetPt).magnitude <= 0.1f)    // if the AI is at target point
        {
            if (m_nSearchChance > 0)
            {
                if (m_fStandTimer <= 0) // Stand time up
                {
                    //Generate a random point in range
                    Vector2 v2RandPt = new Vector2();
                    while ((m_v3Position - m_v3TargetPt).magnitude < m_fPatrolRadius * 0.5 || IsNavMeshBlocked())
                    {
                        v2RandPt = Random.insideUnitCircle * m_fPatrolRadius;
                        v2RandPt.x += m_v3LastSeenPt.x;
                        v2RandPt.y += m_v3LastSeenPt.z;
                        m_v3TargetPt = new Vector3(v2RandPt.x, m_v3Position.y, v2RandPt.y);
                    }
                    m_fStandTimer = (float)(Random.Range(1, 3));
                    m_nSearchChance -= Random.Range(15, 50);
                }
            }
        }
        if (m_lMark.Count != 0)
        {
            Destroy(m_lMark[0]);
            m_lMark.Clear();
            GameObject goMark = (GameObject)Instantiate(m_goQuestionMark, new Vector3(m_v3Position.x, m_v3Position.y + 2, m_v3Position.z), new Quaternion(0, 0, 0, 0));
            m_lMark.Add(goMark);
        }

        if (m_nSearchChance <= 0)
        {
            m_nSearchChance = 100;
            m_bSeenEnemyRecently = false;
            if (m_lMark.Count != 0)
            {
                Destroy(m_lMark[0]);
                m_lMark.Clear();
            }
        }
    }

    public void PlayOwSound()
    {
        GetComponent<AudioSource>().Play();
    }

    public bool IsNavMeshBlocked()
    {
        m_v3Position = transform.position;
        //To check if target location is reachable
        
        bool bBlocked = false;
        if (m_v3TargetPt.magnitude == float.PositiveInfinity || m_nmaTarget.isOnNavMesh == false)
        {
            bBlocked = true;
            //m_nNavMeshTries++;
            return bBlocked;
        }
        NavMeshPath navPath = new NavMeshPath();
        //NavMeshHit nmhBlock;
        NavMesh.CalculatePath(m_v3Position, m_v3TargetPt, m_nmaTarget.areaMask, navPath);
        if (navPath.status == NavMeshPathStatus.PathPartial || navPath.status == NavMeshPathStatus.PathInvalid)
        {
            bBlocked = true;
            //m_nNavMeshTries++;
        }
        else
        {
            //bBlocked = NavMesh.Raycast(m_v3Position, m_v3TargetPt, out nmhBlock, NavMesh.AllAreas);//m_nmaTarget.areaMask);
            //Debug.DrawLine(m_v3Position, m_v3TargetPt, Color.red);
            
            m_nmaTarget.path = navPath;
            //m_nNavMeshTries = 0;
        }
        return bBlocked;
    }

    //public bool SimplifiedIsNavMeshBlocked()
    //{
    //    //if (m_nNavMeshTries >= 4)
    //    //{
    //    //    m_nNavMeshTries++;
    //    //}
    //    //else if (m_nNavMeshTries == 7)
    //    //{
    //    //    m_nNavMeshTries = 0;
    //    //    Death();
    //    //}
    //    m_v3Position = transform.position;
    //    //To check if target location is reachable
    //    NavMeshHit nmhBlock;
    //    bool bBlocked = false;
    //    bBlocked = NavMesh.Raycast(m_v3Position, m_v3TargetPt, out nmhBlock, NavMesh.AllAreas);//m_nmaTarget.areaMask);
    //    return bBlocked;
    //}
    bool Reinforcement()
    {

        bool bCalled = false;
        Collider[] colliderAround = Physics.OverlapSphere(m_v3Position, m_fCallingRadius); // Get all the collider in range.
        for (int i = 0; i < colliderAround.Length; i++)
        {
            if (tag == colliderAround[i].gameObject.tag && gameObject != colliderAround[i].gameObject)
            {
                if (m_aitType == AIType.Wolf)
                {
                    if (colliderAround[i].GetComponent<AIBehavior>().m_aitType == m_aitType)
                    {
                        colliderAround[i].gameObject.GetComponent<AIBehavior>().m_v3TargetPt = m_goTarget.transform.position;
                        colliderAround[i].gameObject.GetComponent<AIBehavior>().m_fCallingTimer = m_fCallingCD;
                        colliderAround[i].gameObject.GetComponent<AIBehavior>().Move();
                        bCalled = true;
                    }
                }
                else
                {
                    colliderAround[i].gameObject.GetComponent<AIBehavior>().m_v3TargetPt = m_goTarget.transform.position;
                    colliderAround[i].gameObject.GetComponent<AIBehavior>().m_fCallingTimer = m_fCallingCD;
                    colliderAround[i].gameObject.GetComponent<AIBehavior>().Move();
                    bCalled = true;
                }
            }
        }
        m_fCallingTimer = m_fCallingCD;
        return bCalled;
    }

    bool SetCharge()
    {
        Vector3 v3OriginalTargetPt = m_v3TargetPt;
        Vector3 v3Charge = (m_v3TargetPt - m_v3Position).normalized * 20;
        m_v3TargetPt = v3Charge + m_v3Position;
        if (IsNavMeshBlocked())
        {
            m_v3TargetPt = v3OriginalTargetPt;
            return false;
        }
        RaycastHit rhObject;
        if (Physics.Raycast(m_v3Position, m_v3TargetPt, out rhObject))
        {
            if (rhObject.collider.gameObject != m_goTarget && rhObject.collider.tag != "Hiding Object")
            {
                m_v3TargetPt = v3OriginalTargetPt;
                return false;
            }
        }
        m_fMovement = 15;
        m_bIsCharging = true;
        return true;
    }

    void BoulderThrowing()
    {
        GameObject goBoulder = (GameObject)Instantiate(m_goProjectile, new Vector3(m_v3Position.x, m_v3Position.y + 1.0f, m_v3Position.z), new Quaternion(0, 0, 0, 0));
        goBoulder.GetComponent<ThrowingBoulder>().m_v3OriginPt = new Vector3(m_v3Position.x, m_v3Position.y + 1.0f, m_v3Position.z);
        goBoulder.GetComponent<ThrowingBoulder>().m_goOwner = gameObject;
        goBoulder.GetComponent<ThrowingBoulder>().m_v3TargetPt = new Vector3(m_v3TargetPt.x, m_v3TargetPt.y + 0.5f, m_v3TargetPt.z);
        m_fChargeCDTimer = m_fChargeCD;
        m_fAttackTimer = m_fAttackCD;
    }

    void Animate()
    {
        // If we don't have a navmesh agent, just stop trying to animate
        if (m_nmaTarget == null)
        {
            Debug.LogError("No NavmeshAgent");
            return;
        }

        // Gets the angle between us and the target so we can rotate
        float angle = CalculateAngleToTarget(m_nmaTarget.path.corners[m_nmaTarget.path.corners.Length - 1]);

        // Conditions for rotating the character sprite
        if (angle > 135 || angle < -135)
        {
            m_nDirection = 2;  // South
        }
        else if (angle < -45)
        {
            m_nDirection = 4;  // West
        }
        else if (angle > 45)
        {
            m_nDirection = 6;  // East
        }
        else if (angle < 45 || angle > -45)
        {
            m_nDirection = 8;  // North
        }

        m_aAnimator.SetInteger("Direction", m_nDirection);

        if (m_fStandTimer <= 0)
        {
            m_aAnimator.SetFloat("Speed", .02f);
        }
        else
        {
            m_aAnimator.SetFloat("Speed", 0f);
        }

        m_aAnimator.SetBool("Attacking", m_bAttacking);
        m_aAnimator.SetBool("Trapped", m_bTrapped);
    }

    float CalculateAngleToTarget(Vector3 target)
    {
        // The vector that we want to measure an angle from
        Vector3 referenceForward = transform.forward;

        // The vector perpendicular to referenceForward
        // (used to determine if angle is positive or negative)
        Vector3 referenceRight = Vector3.Cross(Vector3.up, referenceForward);

        // The vector of interest
        Vector3 newDirection = target - transform.position;

        // Get the angle in degrees between 0 and 180
        float angle = Vector3.Angle(newDirection, referenceForward);

        // Determine if the degree value should be negative.
        float sign = Mathf.Sign(Vector3.Dot(newDirection, referenceRight));

        float finalAngle = sign * angle;
        return finalAngle;
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        if (m_lMark.Count > 0)
        {
            Destroy(m_lMark[0]);
            m_lMark.Clear();
        }
    }

    void ChargeAoE()
    {
        if(!m_asAttackAudio.isPlaying)
            m_asAttackAudio.Play();
        Collider[] colliderAround = Physics.OverlapSphere(m_v3Position, m_fAttackRadius); // Get all the collider in range.
        for (int i = 0; i < colliderAround.Length; i++)
        {
            if (colliderAround[i].gameObject.tag == "Player" || colliderAround[i].gameObject.tag == "Animal" || colliderAround[i].gameObject.tag == "Cannibal" ||
                (colliderAround[i].gameObject.tag == "Hiding Objects" && colliderAround[i].GetComponent<HidingObjectBehavior>().currentlyHiding == true))
            {
                bool bIsChargedTarget = false;
                for (int j = 0; j < m_lChargedTargets.Count; j++)
                {
                    if (colliderAround[i].gameObject.tag == "Hiding Objects")
                    {
                        if (colliderAround[i].GetComponent<HidingObjectBehavior>().player == m_lChargedTargets[j])
                        {
                            bIsChargedTarget = true;
                        }
                    }
                    else
                    {
                        if (colliderAround[i].gameObject == m_lChargedTargets[j])
                            bIsChargedTarget = true;
                    }
                }

                if (!bIsChargedTarget)
                {
                    if (colliderAround[i].tag == "Player")
                    {
                        colliderAround[i].GetComponent<PlayerStatManager>().DecreaseHealth((float)m_fAttackDamage);    //  Damage

                        ////////KEITH'S CODE
                        /// checks if te player's health is less than or equal to zero
                        if (colliderAround[i].GetComponent<PlayerStatManager>().playerHealth <= 0)
                        {
                            /// turns on the cooresponding achievement based on the ai's type
                            PlayerPrefs.SetInt("Death 6", 1);
                            PlayerPrefs.Save();
                        }
                        //////////END OF KEITH'S CODE  

                        m_lChargedTargets.Add(colliderAround[i].gameObject);
                    }
                    else if (colliderAround[i].gameObject.tag == "Hiding Objects" && colliderAround[i].GetComponent<HidingObjectBehavior>().currentlyHiding == true)
                    {
                        colliderAround[i].GetComponent<HidingObjectBehavior>().player.GetComponent<PlayerStatManager>().DecreaseHealth((float)m_fAttackDamage);    //  Damage

                        ////////KEITH'S CODE
                        /// checks if te player's health is less than or equal to zero
                        if (colliderAround[i].GetComponent<HidingObjectBehavior>().player.GetComponent<PlayerStatManager>().playerHealth <= 0)
                        {
                            /// turns on the cooresponding achievement based on the ai's type
                            PlayerPrefs.SetInt("Death 6", 1);
                            PlayerPrefs.Save();
                        }
                        //////////END OF KEITH'S CODE  

                        //Pop out of hiding script here
                        if (colliderAround[i].GetComponent<HidingObjectBehavior>().player.GetComponent<PlayerController>().currentlyHidingOrResting)
                        {
                            colliderAround[i].GetComponent<HidingObjectBehavior>().player.GetComponent<PlayerController>().anObject.gameObject.GetComponent<HidingObjectBehavior>().Hide(m_goTarget.transform);
                            colliderAround[i].GetComponent<HidingObjectBehavior>().player.GetComponent<PlayerController>().currentlyHidingOrResting = false;
                        }

                        m_lChargedTargets.Add(colliderAround[i].GetComponent<HidingObjectBehavior>().player);
                    }
                    else if (colliderAround[i].tag == "Animal" || colliderAround[i].gameObject.tag == "Cannibal")
                    {
                        colliderAround[i].GetComponent<AIBehavior>().m_fHealth -= m_fAttackDamage;
                        m_lChargedTargets.Add(colliderAround[i].gameObject);
                    }

                }
            }
        }
    }
}
