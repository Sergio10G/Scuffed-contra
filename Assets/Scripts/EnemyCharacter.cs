using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCharacter : MonoBehaviour
{

    private GameObject objective_;
    private NavMeshAgent agent_;

    private void Awake()
    {
        objective_ = GameObject.Find("Player");
        agent_ = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        agent_.destination = objective_.transform.position;
    }
}
