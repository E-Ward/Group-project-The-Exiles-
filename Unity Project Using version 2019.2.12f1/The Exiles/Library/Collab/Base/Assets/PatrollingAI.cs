using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingAI : MonoBehaviour
{

    public float wanderRadius; // TODO unused
    public float wanderTimer;

    private Transform target;
    private NavMeshAgent agent;
    private float timer;

    public bool isWandering;
    public bool isPatrolling;

    private EnemyNavDestinationReached enemyNavDestinationReached;
    private WanderingAI wanderingAI;

    public int patrolZoneIndex;
    private int waypointIndex;
    public GameObject patrolZones;
    private EnemyPatrolZones enemyPatrolZones;
    private Transform[] patrolZonesTransform;
    private Transform[] waypointsTransform;
    private int patrolZoneCount;

    void OnEnable()
    {
        enemyNavDestinationReached = GetComponent<EnemyNavDestinationReached>();
        wanderingAI = GetComponent<WanderingAI>();
        agent = GetComponent<NavMeshAgent>();
        enemyPatrolZones = GetComponent<EnemyPatrolZones>();
        patrolZoneCount = patrolZones.transform.childCount;
        // patrolZoneIndex = 0;
        waypointIndex = 0;
        waypointsTransform = GetWaypointsInPatrolZone(patrolZoneIndex);
        target = waypointsTransform[waypointIndex];
        timer = wanderTimer;
        isWandering = false;
        isPatrolling = true;
    }

    // TODO: Maybe move to OnEnable()
    void Awake()
    {
        patrolZonesTransform = new Transform[patrolZones.transform.childCount];
        for (int i = 0; i < patrolZonesTransform.Length; i++)
        {
            patrolZonesTransform[i] = patrolZones.transform.GetChild(i);
        }
    }

    void Update()
    {
        // Debug.Log($"(From PatrollingAI.cs: patrol zones in scene: {patrolZonesTransform.Length}");
        GetWaypointsInPatrolZone(0);
        GetWaypointsInPatrolZone(1);
        // Debug.Log($"Target x: {target.position.x}");

        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            // TODO don't need == here
            if (enemyNavDestinationReached.isTouching == false && isPatrolling == true)
            {
                // Wander to waypoint(s)
                Vector3 newPos = target.position;
                agent.SetDestination(newPos);
                // timer = 0;
                // GetNextWaypoint();
            }

            // if (transform.position == target.position)
            if (Vector3.Distance(transform.position, target.position) <= 0.4f)
            {
                timer = 0;
                target = GetNextWaypoint();  
            }
        }
    }

    private Transform[] GetWaypointsInPatrolZone(int index)
    {
        if (index >= patrolZoneCount)
        {
            // TODO Error log
            return null;
        }

        Transform[] points = new Transform[patrolZonesTransform[index].transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = patrolZonesTransform[index].transform.GetChild(i);
        }

        // Debug.Log($"Number of waypoints in patrol zone {index}: {points.Length}");
        return points;
    }

    private Transform GetNextWaypoint()
    {
        Transform[] currentZoneWaypoints;
        // waypointIndex++;

        Debug.Log($"GetNextWaypoint() - P: {patrolZoneIndex} W: {waypointIndex}");

        // Check patrolZoneIndex/waypointIndex
        if (patrolZoneIndex > patrolZoneCount)
        {
            Debug.Log("Last patrol zone reached.");
            Debug.Log("Resetting patrol zone index and waypoint index.");
            patrolZoneIndex = 0;
            waypointIndex = 0;
        }

        // TODO: Major performance improvements needed
        currentZoneWaypoints = GetWaypointsInPatrolZone(patrolZoneIndex);

        if (++waypointIndex >= currentZoneWaypoints.Length)
        {
            Debug.Log("Last waypoint of zone reached. Going to next zone.");
            waypointIndex = 0;
            // patrolZoneIndex++;
            // Check patrolZoneIndex/waypointIndex
            if (++patrolZoneIndex >= patrolZoneCount)
            {
                Debug.Log("Last patrol zone reached.");
                Debug.Log("Resetting patrol zone index and waypoint index.");
                patrolZoneIndex = 0;
                waypointIndex = 0;
            }
            currentZoneWaypoints = GetWaypointsInPatrolZone(patrolZoneIndex); // FIXME potential performance improvement needed
        }

        return currentZoneWaypoints[waypointIndex];
        // Get the transform for the waypointIndex for patrolZone at patrolZoneIndex
        // Set target (Transform) to transform of waypoint
        // target = nextWaypoint;

        /*
        if (waypointIndex >= EnemyWaypoints.points.Length - 1)
        {
            Debug.Log("Last waypoint of zone reached. Going to next zone.");
            waypointIndex = 0;
            EnemyWaypoints.currentPatrolZone = ++patrolZoneIndex;
            if (patrolZoneIndex >= EnemyWaypoints.patrolZones.Length)
            {
                Debug.Log("Resetting patrol zone index.");
                patrolZoneIndex = 0;
                EnemyWaypoints.currentPatrolZone = 0;
            }

            Debug.Log($"Patrol zone is now: {patrolZoneIndex} (EW = {EnemyWaypoints.currentPatrolZone})");
            target = EnemyWaypoints.points[waypointIndex];
            return;
        }

        Debug.Log($"Getting next waypoint (patrol zone {patrolZoneIndex} / EW = {EnemyWaypoints.currentPatrolZone}).");
        target = EnemyWaypoints.points[++waypointIndex];*/
    }
}
