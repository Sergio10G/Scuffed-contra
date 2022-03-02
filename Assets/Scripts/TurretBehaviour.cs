using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
    private GameObject head_;
    private GameObject player_;

    private void Awake()
    {
        head_ = gameObject.transform.Find("Head").gameObject;
        player_ = GameObject.Find("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 look_direction = new Vector3(player_.transform.position.x - transform.position.x, player_.transform.position.y - transform.position.y, 0.0f);
        head_.transform.forward = look_direction.normalized;
    }
}
