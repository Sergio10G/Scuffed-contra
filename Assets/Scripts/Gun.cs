using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun
{
    public Transform shooting_point_;
    public GameObject bullet_pool_;

    public float bullet_speed_;
    public float bullet_dmg_;
    public int parent_id_;

    public Gun(Transform shooting_point, GameObject bullet_pool, float bullet_speed, float bullet_dmg, int parent_id)
    {
        shooting_point_ = shooting_point;
        bullet_pool_ = bullet_pool;
        bullet_speed_ = bullet_speed;
        bullet_dmg_ = bullet_dmg;
        parent_id_ = parent_id;
    }

    public void Shoot(Vector3 direction) 
    {
        GameObject bullet = bullet_pool_.GetComponent<PrefabPool>().GetPoolObject();
        bullet.SetActive(true);
        bullet.transform.position = shooting_point_.position;
        bullet.GetComponent<BulletBehaviour>().UpdateTime();
        bullet.GetComponent<BulletBehaviour>().shooter_id = parent_id_;
        bullet.GetComponent<BulletBehaviour>().damage_ = bullet_dmg_;
        Rigidbody bullet_rb = bullet.GetComponent<Rigidbody>();
        bullet_rb.velocity = Vector3.zero;
        bullet_rb.angularVelocity = Vector3.zero;
        bullet_rb.AddForce(direction * bullet_speed_, ForceMode.Impulse);
    }
}
