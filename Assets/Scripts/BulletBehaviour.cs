using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Vector3 initial_scale_;
    private float start_time_;
    public int shooter_id = -1;
    public float lifespan_ = 1;
    
    
    private float _damage;

    public float damage
    {
        get { return _damage; }
        set
        {
            _damage = value;

            transform.localScale = initial_scale_ * (damage / 12);
        }
    }

    private void Awake()
    {
        initial_scale_ = transform.localScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - start_time_ > lifespan_)
        {
            gameObject.SetActive(false);
        }
    }

    public void UpdateTime()
    {
        start_time_ = Time.time;
    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
