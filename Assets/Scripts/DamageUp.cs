using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerCharacter pc = other.gameObject.GetComponent<PlayerCharacter>();
            pc.damage *= 1.5f;
            gameObject.SetActive(false);
        }
    }
}
