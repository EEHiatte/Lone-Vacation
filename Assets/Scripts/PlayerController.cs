using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    #region Variables
    public NavMeshAgent navMeshTarget;
    public GameObject currentPickup;
    //The distance required to stop chasing the current pickup
    public float distToTargetStop;

    public float orignalSpeed = .105f;
    public float currentSpeed;

    public float sprintSpeed = .210f;
    float stamina = 100;
    public float staminaIncreaseRate = 50;
    public float staminaDecreaseRate = 15f;
    float staminaTimer = 0.0f;

    public bool currentlyHidingOrResting;
    public Transform anObject;

    public bool isMoving = false;
    public bool walkToObject = false;

    GameObject Player;

    public GameObject staminaBarObj;
    GameObject staminaBar;

    InventoryManager invManager;
    bool hasBoots = false;

    public GameObject boots;
    public GameObject lantern;
    public Sprite lanternOffImage;
    public Sprite lanternOnImage;
    GameObject lanternButton;
    bool hasLantern = false;

    public bool eating = false;
    public Vector3 eatingSpot;
    float eatingTimer = 0.0f;
    public AudioSource foodMunching;

    public Animator[] animators;
    int direction = 2;
    float movementSpeed = 0.0f;

    //Vector3 lastSpot;
    // Use this for initialization

    public bool inPainObj = false;
    public float painTime = 0.0f;

    public bool slowedByLake = false;
    float lakeSlowAmt = 0.03f;
    public int hidecount = 0;
    Camera mainCamera;
    public LayerMask layerMask;
    public bool navigating = false;

    bool clickEnabled = true;
    //bool performDouble = false;
    bool doubleClicked = false;
    //bool clickComplete = false;
    float clickDelay = 0.1f;

    public bool sprintEnabled = false;

    public bool canMove = true;
    public bool tryingToMove = false;
    PlayerCreateTrap playerCreateTrap;
    //bool pauseForTrap = false;
    bool queueTrap = false;
    public bool onWater = false;
    Rigidbody playerRigidbody;

    public Button toggleSprintButton;    
    #endregion

    void Start()
    {
        orignalSpeed = 0.1f;
        currentlyHidingOrResting = false;
        anObject = null;
        currentPickup = null;

        Player = GameObject.FindGameObjectWithTag("Player");
        invManager = GetComponent<InventoryManager>();
        staminaBarObj = GameObject.Find("StaminaBarObject");
        lanternButton = GameObject.Find("LanternButton");
        lanternButton.GetComponent<ToggleActive>().SetInactiveState();
        navMeshTarget = GetComponent<NavMeshAgent>();

        mainCamera = Camera.main;

        playerCreateTrap = GetComponent<PlayerCreateTrap>();
        playerRigidbody = GetComponent<Rigidbody>();

        toggleSprintButton = GameObject.Find("SprintButton").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        playerRigidbody.velocity = Vector3.zero;
        CheckUpdate();
        if(!canMove)
        {
            if (!Input.anyKey || !playerCreateTrap.building)
            {
                canMove = true;
                navMeshTarget.enabled = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (anObject != null && anObject.tag == "Hiding Object" && anObject.GetComponent<HidingObjectBehavior>().hasFood)
            {
                eating = true;
                eatingSpot = Player.transform.position;
            }
        }

        #region Eating
        if (eating && !foodMunching.isPlaying)
            foodMunching.Play();
        else if (!eating)
            foodMunching.Stop();

        if (inPainObj && painTime < 0)
        {
            Player.GetComponent<PlayerStatManager>().playerHealth -= anObject.GetComponent<HidingObjectBehavior>().damageCount;
            painTime = 2.0f;
        }

        if (eating && eatingTimer < 0)
        {
            if (Player.GetComponent<PlayerStatManager>().playerHunger < 100 && anObject != null)
            {
                Player.GetComponent<PlayerStatManager>().IncreaseHunger(anObject.GetComponent<HidingObjectBehavior>().foodCount);
                eatingTimer = 2.0f;
            }
            else
                eating = false;
        }
        #endregion

        if (anObject != null && currentlyHidingOrResting == true && anObject.tag == "Fire")
        {
            Player.GetComponent<PlayerStatManager>().IncreaseHealth(.05f);
        }

        StaminaBar();

        #region ThoughtBubble
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, 3.0f);
        if (nearbyObjects.Length != 0)
        {
            for (int i = 0; i < nearbyObjects.Length; i++)
            {
                if (nearbyObjects[i].GetComponent<CollectableBehavior>())
                    nearbyObjects[i].GetComponent<CollectableBehavior>().playerIsNear = true;
                else if (nearbyObjects[i].GetComponent<CoveDecorationPiece>())
                    nearbyObjects[i].GetComponent<CoveDecorationPiece>().playerIsNear = true;
                else if (nearbyObjects[i].GetComponent<StockpileBehavior>())
                    nearbyObjects[i].GetComponent<StockpileBehavior>().playerIsNear = true;
                else if (nearbyObjects[i].GetComponent<CultistScript>())
                    nearbyObjects[i].GetComponent<CultistScript>().playerIsNear = true;
                else if (nearbyObjects[i].GetComponent<FireBehavior>())
                    nearbyObjects[i].GetComponent<FireBehavior>().playerIsNear = true;
            }
        }
        #endregion

        #region MouseInput/Navigation
        if (canMove)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (clickEnabled)
                {
                    clickEnabled = false;
                    StartCoroutine("DoubleClick", clickDelay);
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            NavigateToMouse();
        }

        if (navigating)
        {
            if ((navMeshTarget.destination - transform.position).magnitude < 0.1f)
            {
                navigating = false;
                currentPickup = null;
            }

            if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0))
            {
                navigating = false;
            }

            //if (navMeshTarget.enabled && navMeshTarget.destination != null)
            //    Debug.DrawLine(transform.position, navMeshTarget.destination, new Color(255, 0, 0));
        }
        #endregion

        #region KeyboardMovement
        if (canMove)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); ;
            navMeshTarget.updateRotation = false;
            navMeshTarget.speed = currentSpeed * 100;
            if (!navigating)
            {
                if (move != Vector3.zero)
                {
                    eating = false;
                    navMeshTarget.SetDestination(transform.position + move.normalized * currentSpeed * 10);
                }
                //////////////
                //BUG FIX
                //Bug #10/11
                else if (navMeshTarget.hasPath)
                    navMeshTarget.destination = transform.position;
                //END BUG FIX
                //////////////
                movementSpeed = (currentSpeed * move).magnitude;
            }

            if (IsNavMeshBlocked())
            {
                NavMeshHit navHit;
                if (NavMesh.Raycast(transform.position, navMeshTarget.destination, out navHit, NavMesh.AllAreas))
                    navMeshTarget.SetDestination(navHit.position);
                else
                    navMeshTarget.SetDestination(transform.position);
            }
        }
        #endregion

        #region Trap Placement
        if (!playerCreateTrap.building && !currentlyHidingOrResting)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PerformCreateTrap((int)TrapBehavior.TrapType.GopherHole);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                PerformCreateTrap((int)TrapBehavior.TrapType.TripTrap);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                PerformCreateTrap((int)TrapBehavior.TrapType.RockTripTrap);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                PerformCreateTrap((int)TrapBehavior.TrapType.ThornField);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                PerformCreateTrap((int)TrapBehavior.TrapType.TreeTrap);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6) || queueTrap)
            {
                PerformCreateTrap((int)TrapBehavior.TrapType.Pitfall);
            }
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.G) && hasLantern == true)
        {
            LanternToggle();
        }

        #region WalkToObject
        if (canMove)
            NavigateToObject(currentPickup);
        #endregion

        if (!currentlyHidingOrResting)
            Animate();
    }

    void FixedUpdate()
    {
        #region Timers
        staminaTimer -= Time.deltaTime;
        eatingTimer -= Time.deltaTime;
        painTime -= Time.deltaTime;
        #endregion

        #region Sprint
        //BUG FIX
        //BUG #30 (in if statement)
        if (!currentlyHidingOrResting && (Input.GetKey(KeyCode.Space) || sprintEnabled) && stamina > 0 && navMeshTarget.destination != transform.position)
        {
            currentSpeed = sprintSpeed;
            if (staminaTimer <= 0)
            {
                stamina -= staminaDecreaseRate;
                staminaTimer = 0.2f;
                if (stamina <= 0)
                {
                    stamina = 0;
                    staminaTimer = 2.0f;
                }
            }
        }
        //else if (currentSpeed == 0)
        //{
        //    currentSpeed = orignalSpeed;
        else
        {
            currentSpeed = orignalSpeed;
            if (staminaTimer <= 0)
            {
                stamina += staminaIncreaseRate;
                staminaTimer = .25f;
            }
            if (stamina > 100)
                stamina = 100;
        }
        #endregion

        #region Hiding/Resting
        if (currentlyHidingOrResting)
        {
            currentSpeed = 0;
            if (anObject != null && anObject.tag == "Fire" && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
            {
                currentlyHidingOrResting = false;
            }
        }
        else if (currentSpeed == 0)
            currentSpeed = orignalSpeed;

        //if (eatingSpot != Player.transform.position && eating)
        //    eating = false;
        #endregion

        if (slowedByLake)
            currentSpeed -= lakeSlowAmt;
    }

    void StaminaBar()
    {
        if (stamina < 100)
        {
            staminaBarObj.GetComponent<ToggleActive>().SetActiveState();
            if (staminaBar == null)
            {
                staminaBar = GameObject.Find("StaminaBar");
            }
            staminaBar.gameObject.transform.localScale = new Vector3(stamina / 100f, 1f, 1f);
        }
        else if (stamina == 100)
        {
            staminaBarObj.GetComponent<ToggleActive>().SetInactiveState();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Hiding Object")
        {
            anObject = collider.transform;
            anObject.tag = "Hiding Object";
        }

    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Hiding Object")
        {
            anObject = null;
        }
    }

    public void CheckPowerUps(bool bootCondition)
    {
        if (invManager.haveBoots == true && hasBoots == false)
        {
            orignalSpeed = 0.15f;
            sprintSpeed = 0.25f;
            boots.GetComponent<ToggleActive>().SetActiveState();
            hasBoots = true;
        }

        if (invManager.haveLantern == true && hasLantern == false)
        {
            lanternButton.GetComponent<ToggleActive>().SetActiveState();
            hasLantern = true;
        }
    }

    public void LanternToggle()
    {
        ToggleActive lanternState = lantern.GetComponent<ToggleActive>();
        lanternState.ToggleActiveState();
        GameObject lanternButton = GameObject.Find("LanternButton");
        if (lantern.activeSelf)
        {
            lanternButton.GetComponent<Image>().sprite = lanternOnImage;
            lanternButton.GetComponent<AudioSource>().Play();
        }
        else
        {
            lanternButton.GetComponent<Image>().sprite = lanternOffImage;
            lanternButton.GetComponent<AudioSource>().Play();
        }
    }

    void Animate()
    {
        if (!walkToObject && !navigating)
        {
            // Checks how we are moving and changes our direction accordingly and makes sure we face in the direction we are going fastest
            if (Input.GetAxis("Horizontal") > 0 && Mathf.Abs(Input.GetAxis("Horizontal")) >= Mathf.Abs(Input.GetAxis("Vertical")))
            {
                direction = 6;
            }
            else if (Input.GetAxis("Horizontal") < 0 && Mathf.Abs(Input.GetAxis("Horizontal")) >= Mathf.Abs(Input.GetAxis("Vertical")))
            {
                direction = 4;
            }
            else if (Input.GetAxis("Vertical") > 0 && Mathf.Abs(Input.GetAxis("Vertical")) >= Mathf.Abs(Input.GetAxis("Horizontal")))
            {
                direction = 8;
            }
            else if (Input.GetAxis("Vertical") < 0 && Mathf.Abs(Input.GetAxis("Vertical")) >= Mathf.Abs(Input.GetAxis("Horizontal")))
            {
                direction = 2;
            }
        }
        else
        {
            // Gets the angle between us and the target so we can rotate
            float angle = CalculateAngleToTarget(navMeshTarget.destination);

            // Conditions for rotating the character sprite
            if (angle > 135 || angle < -135)
            {
                direction = 2;  // South
            }
            else if (angle < -45)
            {
                direction = 4;  // West
            }
            else if (angle > 45)
            {
                direction = 6;  // East
            }
            else if (angle < 45 || angle > -45)
            {
                direction = 8;  // North
            }
        }

        // Sets for all animators
        foreach (Animator anim in animators)
        {
            if (anim.gameObject.activeSelf)
            {
                // Sets our animator's direction condition
                anim.SetInteger("Direction", direction);
                // Sets our animator's speed condition
                anim.SetFloat("Speed", movementSpeed);
                // Sets our animator's building condition
                anim.SetBool("Building", GetComponent<PlayerCreateTrap>().building);
            }
        }
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

    public bool IsNavMeshBlocked()
    {
        //To check if target location is reachable
        bool bBlocked = false;
        if (navMeshTarget.pathStatus == NavMeshPathStatus.PathInvalid || navMeshTarget.pathStatus == NavMeshPathStatus.PathPartial)
        {
            Debug.Log("Is blocked");
            bBlocked = true;
        }
        else
            bBlocked = false;
        return bBlocked;
    }

    // Returns true if the mouse clicked on something before reaching the target
    bool CheckMouseInput()
    {
        EventSystem eventSystem = EventSystem.current;
        eventSystem.UpdateModules();
        if (eventSystem.alreadySelecting)
        {
            return CheckMouseInput();
        }
        if (!eventSystem.IsPointerOverGameObject() && !Application.isMobilePlatform)
        {
            return false;
        }
        else if (eventSystem.currentSelectedGameObject == null)
        {
            return false;
        }
        else if (eventSystem.currentSelectedGameObject.GetComponent<RawImage>() != null)
        {
            return false;
        }
        else
            return true;
    }

    void NavigateToMouse()
    {
        // BUG #32
        if (GameManager.paused)
        {
            return;
        }
        // END BUG
        // Now we check if where we click is blocked by UI elements
        if (!CheckMouseInput())
        {
            // If we're good, cast to the terrain
            RaycastHit rayHit;
            bool isLocationValid;
            Vector3 mousePos = Input.mousePosition;
            isLocationValid = Physics.Raycast(mainCamera.ScreenPointToRay(mousePos), out rayHit, 500f, layerMask);
            // If we hit a valid location, begin navigating with navmesh
            if (canMove && isLocationValid && rayHit.collider.tag == "Terrain")
            {
                eating = false;
                navigating = true;
                if (navMeshTarget.enabled == false)
                    navMeshTarget.enabled = true;
                navMeshTarget.SetDestination(rayHit.point);
                navMeshTarget.speed = currentSpeed * 100;
                navMeshTarget.updateRotation = false;
                movementSpeed = navMeshTarget.speed;
                currentPickup = null;
            }
        }
    }

    public void NavigateToObject(GameObject target)
    {
        // BUG #32
        if (GameManager.paused && currentPickup != target)
        {
            return;
        }
        // END BUG

        currentPickup = target;
        walkToObject = true;

        if (walkToObject && ((Input.GetAxis("Horizontal") == 0) && (Input.GetAxis("Vertical") == 0)) && currentPickup != null)
        {
            if (currentPickup.tag == "Fire")
            {
                distToTargetStop = currentPickup.GetComponent<FireBehavior>().collectDistance - 1;
            }
            else if (currentPickup.GetComponent<CollectableBehavior>() != null)
            {
                distToTargetStop = currentPickup.GetComponent<CollectableBehavior>().collectDistance - 1;
            }
            else
            {
                distToTargetStop = 2;
            }
            navigating = false;
            if (navMeshTarget.enabled == false)
                navMeshTarget.enabled = true;
            //Animate();

            if ((transform.position - currentPickup.transform.position).magnitude > distToTargetStop && navMeshTarget.destination != currentPickup.transform.position)
            {
                navMeshTarget.SetDestination(currentPickup.transform.position);
                navMeshTarget.updateRotation = false;
                navMeshTarget.speed = currentSpeed * 100;
                movementSpeed = navMeshTarget.speed;
            }
            if ((transform.position - currentPickup.transform.position).magnitude < distToTargetStop)
            {
                currentPickup = null;
                walkToObject = false;
                navMeshTarget.SetDestination(transform.position);
            }
            isMoving = true;
            eating = false;
        }
        else
        {
            currentPickup = null;
            walkToObject = false;
            //if (navigating)
            //navMeshTarget.enabled = false;
            //transform.Translate(currentSpeed * move.normalized);
        }

        if (!walkToObject && ((Input.GetAxis("Horizontal") == 0) && (Input.GetAxis("Vertical") == 0)))
            isMoving = false;
    }

    IEnumerator DoubleClick(float delay)
    {
        //Debug.Log("Starting to listen for double clicks");
        float endTime = Time.time + delay;
        while (Time.time < endTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("Double click!");
                yield return new WaitForSeconds(0.4f);
                clickEnabled = true;
                doubleClicked = true;

            }
            yield return 0;
        }

        if (doubleClicked)
        {
            doubleClicked = false;
            //performDouble = true;
        }

        clickEnabled = true;
        //clickComplete = true;
        yield return 0;
    }

    public void ToggleSprint()
    {
        sprintEnabled = !sprintEnabled;
        /////////////
        //BUG FIX
        //BUG #6
        ColorBlock colorBlock = toggleSprintButton.colors;
        if (sprintEnabled)
            colorBlock.normalColor = new Color(0.5f, 0.5f, 0.5f);
        else
            colorBlock.normalColor = new Color(1.0f, 1.0f, 1.0f);
        toggleSprintButton.colors = colorBlock;
        toggleSprintButton.enabled = false;
        toggleSprintButton.enabled = true;
        //END BUG FIX
        /////////////
    }

    public void PerformCreateTrap(int type)
    {
        if (onWater)
        {
            return;
        }
        if (Player.GetComponent<PlayerCreateTrap>().CreateTrap(type))
        {
            eating = false;
            navMeshTarget.SetDestination(transform.position);
            navMeshTarget.enabled = false;
            navigating = false;
            canMove = false;
        }
    }

    void CheckUpdate()
    {
        Collider[] colliderAround = Physics.OverlapSphere(transform.position, 50); // Get all the collider in range.
        for (int i = 0; i < colliderAround.Length; i++)
        {
            if (colliderAround[i].gameObject.tag == "Animal" || colliderAround[i].gameObject.tag == "Cannibal" )
            {
                colliderAround[i].GetComponent<AIBehavior>().m_fUpdateTimer = 0;
            }
        }
    }
}
