using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Implements the patrolling logic for the enemy AI.
/// 
/// Author: Alpeche Pancha
/// </summary>
public class PatrollingAI : MonoBehaviour
{
    /// <summary>
    /// A GameObject used to group all the patrol zone objects.
    /// </summary>
    public GameObject patrolZones;
    /// <summary>
    /// The index of the current zone being patrolled by the AI.
    /// </summary>
    public int patrolZoneIndex;
    /// <summary>
    /// The amount of time (in seconds) that the AI patrols a zone (standing still in it) for.
    /// </summary>
    public float patrolTimer;
    /// <summary>
    /// A boolean indicating if the enemy AI is currently patrolling (as opposed to wandering freely).
    /// 
    /// TODO: This bool might not be needed if we enable/disable the component.
    /// </summary>
    public bool isPatrolling;

    private EnemyNavDestinationReached enemyNavDestinationReached;
    private EnemyNavPursue enemyNavPursue;
    private WanderingAI wanderingAI;
    private NavMeshAgent agent;
    private Transform target;
    private Transform[] patrolZonesTransform;
    private Transform[] waypointsTransform; // TODO rename to zoneWaypoints?
    private int patrolZoneCount;
    private int waypointIndex;
    private float timer;

    /// <summary>
    /// Called when this component is enabled.
    /// </summary>
    void OnEnable()
    {
        // patrolZoneIndex = 0;
        isPatrolling = true;

        enemyNavDestinationReached = GetComponent<EnemyNavDestinationReached>();
        enemyNavPursue = GetComponent<EnemyNavPursue>();
        wanderingAI = GetComponent<WanderingAI>();
        agent = GetComponent<NavMeshAgent>();

        patrolZoneCount = patrolZones.transform.childCount;
        waypointIndex = 0;
        waypointsTransform = GetWaypointsInPatrolZone(patrolZoneIndex);
        target = waypointsTransform[waypointIndex];
        timer = patrolTimer;
    }

    void Awake()
    {
        patrolZonesTransform = new Transform[patrolZones.transform.childCount];
        for (int i = 0; i < patrolZonesTransform.Length; i++)
        {
            patrolZonesTransform[i] = patrolZones.transform.GetChild(i);
        }
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= patrolTimer)
        {
            if (!enemyNavDestinationReached.isTouching && isPatrolling)
            {
                // Set AI destination to waypoint
                // TODO: This can be potentially optimized, as we probably
                //       don't need to call SetDestination() every frame.
                agent.SetDestination(target.position);
            }

            if (Vector3.Distance(transform.position, target.position) <= 0.4f)
            {
                timer = 0;
                target = GetNextWaypoint();  
            }

            // TODO: WIP Program enemy AI to switch between patrolling and following the player if they’re spotted
            // Currently many errors e_e
            // agent.TryToChaseTarget();
            // enemyNavPursue.TryToChaseTarget();
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

        return points;
    }

    private Transform GetNextWaypoint()
    {
        Debug.Log($"GetNextWaypoint: Zone {patrolZoneIndex}, WP {waypointIndex}");

        // Increment waypointIndex and check if it's higher than number of
        // waypoints in current zone.
        if (++waypointIndex >= waypointsTransform.Length)
        {
            Debug.Log("Last waypoint of zone reached. Going to next zone.");
            waypointIndex = 0;

            // Increment patrolZoneIndex and check if it's higher than number
            // of existing patrol zones.
            if (++patrolZoneIndex >= patrolZoneCount)
            {
                Debug.Log("Last patrol zone reached. Resetting patrol zone and waypoint indices.");
                patrolZoneIndex = 0;
                waypointIndex = 0;
            }

            waypointsTransform = GetWaypointsInPatrolZone(patrolZoneIndex);
        }

        return waypointsTransform[waypointIndex];
    }
}
