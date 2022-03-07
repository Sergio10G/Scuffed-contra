using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCharacter : MonoBehaviour, IHittable
{
    private GameManager gm_;
    private GameObject objective_;
    private NavMeshAgent agent_;

    public float hit_period_ = 1;
    public bool ready_to_hit_ = true;
    private float last_hit_time_;

    private float _health;
    private float _damage;

    public float health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health == 0)
            {
                gm_.score += 5;
                gameObject.SetActive(false);
            }
        }
    }

    public float damage
    {
        get { return _damage; }
        set
        {
            _damage = value;

            // OnDamageChange stuff
        }
    }

    private void Awake()
    {
        gm_ = GameObject.Find("GameManager").GetComponent<GameManager>();
        health = 150;
        damage = 30;
        objective_ = GameObject.Find("Player");
        agent_ = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        last_hit_time_ = Time.time;
    }
    void Update()
    {
        if (objective_ != null)
            agent_.destination = objective_.transform.position;
        if (Time.time - last_hit_time_ > hit_period_ && !ready_to_hit_)
        {
            ready_to_hit_ = true;
            last_hit_time_ = Time.time;
        }
    }

    public float takeDamage(float dmg)
    {
        if (health > dmg)
        {
            health -= dmg;
            return health;
        }
        health = 0;
        return 0;
    }
}
