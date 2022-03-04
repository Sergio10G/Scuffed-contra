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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
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
}
