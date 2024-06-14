using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Interactable
{
    #region Fields

    public float speed = 5;
    public float waitTime = .3f;
    public float turnSpeed = 90;

    public Light spotLight;
    public float viewDistance;
    public LayerMask viewMask;
    float viewAngle;
    

    public Transform pathHolder;
    Transform player;
    public GameObject item;
    Color originalSpotlightColor;
    public StealthGameManager sgm;

    public bool hasItem;

    #endregion

    #region Events

    public static event System.Action OnPlayerSpotted;

    #endregion

    #region UnityMethods

    void Start()
    {
        hasItem = false;
        sgm = GameObject.Find("StealthGameManager").GetComponent<StealthGameManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        canInteract = true;

        originalSpotlightColor = spotLight.color;
        viewAngle = spotLight.spotAngle;

        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }

        StartCoroutine(PatrolPath(waypoints));
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            spotLight.color = Color.red;
            sgm.playerInput.DeactivateInput();
            
            if (OnPlayerSpotted != null)
            {
                OnPlayerSpotted();
            }
            Time.timeScale = 0;
        }
        else
        {
            spotLight.color = originalSpotlightColor;
        }
    }

    #endregion

    #region Methods

    bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if (angleToPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, player.position, viewMask))
                {
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator PatrolPath(Vector3[] waypoints)
    {
        transform.position = waypoints[0];
        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        transform.LookAt(targetWaypoint);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            if (transform.position == targetWaypoint)
            {
                targetWaypointIndex = (targetWaypointIndex +1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                yield return new WaitForSeconds(waitTime);
                yield return StartCoroutine(FaceTarget(targetWaypoint));
            }
            yield return null;
        }
    }

    IEnumerator FaceTarget(Vector3 lookAtTarget)
    {
        Vector3 directionToLook = (lookAtTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(directionToLook.z, directionToLook.x) * Mathf.Rad2Deg;
        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }

    #endregion

    #region InterfaceImplementation

    public override void Interact()
    {
        if (canInteract)
        {
            canInteract = false;
            sgm.playerHasItem = true;
            Debug.Log("Collected: " + item.name, gameObject);
            Destroy(item);
            // Add one to collected info ui
        }

    }

    #endregion

    #region Debug

    void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;

        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * viewDistance);
    }

    #endregion
}
