using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private PrefabPool enemy_pool_;
    private GameObject player_;
    private GameObject light_;
    private GameObject red_;
    private GameObject green_;

    private bool active_;
    public float enemy_spawn_period_ = 3;
    private float spawn_radius_;
    private float deactivate_radius_;
    private float last_enemy_time;

    private void Awake()
    {
        player_ = GameObject.Find("Player");
        enemy_pool_ = GameObject.Find("EnemyPool").GetComponent<PrefabPool>();
        light_ = transform.Find("Light").gameObject;
        red_ = transform.Find("Red").gameObject;
        green_ = transform.Find("Green").gameObject;
        spawn_radius_ = 35;
        deactivate_radius_ = 8;
        active_ = true;
    }

    void Start()
    {
        last_enemy_time = Time.time;
    }

    void Update()
    {
        if (active_)
        {
            if (Mathf.Abs(player_.transform.position.x - transform.position.x) <= spawn_radius_ && Time.time - last_enemy_time > enemy_spawn_period_)
            {
                GameObject go = enemy_pool_.GetPoolObject();
                if (go != null)
                {
                    EnemyCharacter ec = go.GetComponent<EnemyCharacter>();
                    ec.health = 150;
                    go.transform.position = gameObject.transform.position;
                    go.SetActive(true);
                    last_enemy_time = Time.time;
                }
            }
            if (Mathf.Abs(player_.transform.position.x - transform.position.x) <= deactivate_radius_)
            {
                Deactivate();
            }
        }
        
    }

    public void Activate()
    {
        red_.SetActive(false);
        green_.SetActive(true);
        light_.GetComponent<Light>().color = new Color(0, 255, 0, 0.5f);
        active_ = true;
    }
    public void Deactivate()
    {
        red_.SetActive(true);
        green_.SetActive(false);
        light_.GetComponent<Light>().color = new Color(255, 0, 0, 0.5f);
        active_ = false;
    }
}
