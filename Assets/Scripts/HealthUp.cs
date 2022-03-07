using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerCharacter pc = other.gameObject.GetComponent<PlayerCharacter>();
            pc.max_health_ += 25;
            pc.health += 25;
            gameObject.SetActive(false);
        }
    }
}
