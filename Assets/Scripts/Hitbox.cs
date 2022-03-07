using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private GameObject parent_;
    public int parent_id_;

    private void Awake()
    {
        parent_ = gameObject.transform.parent.gameObject;
        if (parent_.tag == "Player")
            parent_id_ = 0;
        else
            parent_id_ = 1;
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            BulletBehaviour bb = other.gameObject.GetComponent<BulletBehaviour>();
            if (bb.shooter_id != parent_id_)
            {
                IHittable p = parent_.GetComponent<IHittable>();
                p.takeDamage(bb.damage);
                other.gameObject.SetActive(false);
            }
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (parent_.tag == "Player" && other.gameObject.tag == "Enemy")
        {
            EnemyCharacter ec = other.gameObject.GetComponent<EnemyCharacter>();
            if (ec.ready_to_hit_)
            {
                ec.ready_to_hit_ = false;
                IHittable p = parent_.GetComponent<IHittable>();
                p.takeDamage(ec.damage);
            }
        }
    }
}
