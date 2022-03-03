using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private float start_time_;
    public int shooter_id = -1;
    public float lifespan_ = 1;
    public float damage_;

    private void Awake()
    {

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
}
