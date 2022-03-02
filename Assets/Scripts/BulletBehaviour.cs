using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private float start_time_;
    public float lifespan_;

    private void Awake()
    {
        lifespan_ = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        start_time_ = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - start_time_ > lifespan_)
        {
            Destroy(gameObject);
        }
    }
}
