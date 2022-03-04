using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HittableCharacter : MonoBehaviour, IHittable
{
    private float _health;
    private float _dmg;
    private Gun _gun;

    public float health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health == 0)
            { 
                // Death stuff
            }
        }
    }

    public float dmg
    {
        get { return _dmg; }
        set
        {
            _dmg = value;
            
            // OnDamageChange stuff
        }
    }

    public Gun gun
    {
        get { return _gun; }
        set
        {
            _gun = value;

            // OnGunChange stuff
        }
    }

    public HittableCharacter(float health, float dmg, Gun gun)
    {
        this.health = health;
        this.dmg = dmg;
        this.gun = gun;
    }

    public HittableCharacter() { }

    public float takeDamage(float dmg)
    {
        if (health > dmg)
        {
            health -= dmg;
            return health;
        }
        health = 0;
        return 0;
    }
}
