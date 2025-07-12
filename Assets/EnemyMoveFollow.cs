using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveFollow : MonoBehaviour
{
    [SerializeField]
    Transform player;
    NavMeshAgent nmAgent;

    void Start()
    {
        nmAgent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        nmAgent.SetDestination(player.position);
    }
}
