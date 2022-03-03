using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour, IHittable
{
    private GameObject head_;
    private GameObject player_;
    private Transform shooting_point_;
    private Gun gun_;

    public GameObject bullet_pool_;

    private float last_shot_time_;

    public float health_ = 100;
    public float fire_time_ = 1;
    public float bullet_force_ = 50;

    private void Awake()
    {
        head_ = gameObject.transform.Find("Head").gameObject;
        player_ = GameObject.Find("Player");
        shooting_point_ = transform.Find("Head").Find("ShootingPoint");
        gun_ = new Gun(shooting_point_, bullet_pool_, bullet_force_, 12, 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        last_shot_time_ = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 look_direction = new Vector3(player_.transform.position.x - transform.position.x, player_.transform.position.y - transform.position.y - 1, 0.0f);
        head_.transform.forward = look_direction.normalized;
        if (Time.time - last_shot_time_ > fire_time_)
        {
            gun_.Shoot(head_.transform.forward);
            last_shot_time_ = Time.time;
        }
    }

    public float takeDamage(float dmg)
    {
        if (health_ > dmg)
        {
            health_ -= dmg;
            Debug.Log("Health: " + health_);
            return health_;
        }
        gameObject.SetActive(false);
        return 0;
    }
}
