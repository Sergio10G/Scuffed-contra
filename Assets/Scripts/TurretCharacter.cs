using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCharacter : MonoBehaviour, IHittable
{
    private GameObject bullet_pool_;
    private GameObject head_;
    private Transform shooting_point_;
    private GameObject player_;

    private float last_shot_time_;

    private float _health;
    private float _damage;
    private Gun _gun;

    public float shooting_period_ = 1;
    public float bullet_force_ = 50;

    public float health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health == 0)
            {
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

            if (gun != null)
                gun.bullet_dmg_ = _damage;
        }
    }

    public Gun gun
    {
        get { return _gun; }
        set
        {
            _gun = value;

            // OnGunChange stuff
        }
    }

    private void Awake()
    {
        health = 100;
        damage = 12;
        head_ = gameObject.transform.Find("Head").gameObject;
        player_ = GameObject.Find("Player");
        bullet_pool_ = GameObject.Find("BulletPool");
        shooting_point_ = transform.Find("Head").Find("ShootingPoint");
        gun = new Gun(shooting_point_, bullet_pool_, bullet_force_, damage, 1);
    }

    void Start()
    {
        last_shot_time_ = Time.time;
    }

    void Update()
    {
        Vector3 look_direction = new Vector3(player_.transform.position.x - transform.position.x, player_.transform.position.y - transform.position.y - 1, 0.0f);
        head_.transform.forward = look_direction.normalized;
        if (Time.time - last_shot_time_ > shooting_period_)
        {
            gun.Shoot(head_.transform.forward);
            last_shot_time_ = Time.time;
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
