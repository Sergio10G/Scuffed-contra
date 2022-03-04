using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour, IHittable
{
    private CharacterController controller_;
    private InputController input_;
    private Vector2 movement_;
    private Vector3 velocity_;
    private bool is_jumping_;
    private bool is_firing_;

    private GameObject camera_;

    public float speed_ = 10;
    public float jump_force_ = 3;
    public float gravity_ = 15;
    public float bullet_force_ = 50;
    public GameObject bullet_pool_;

    private float _health;
    private float _damage;
    private Gun _gun;

    public float health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health == 0)
                gameObject.SetActive(false);
        }
    }

    public float damage
    {
        get { return _damage; }
        set
        {
            _damage = value;

            if (gun != null)
                gun.bullet_dmg_ = damage;
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

    private void Awake()
    {
        health = 100;
        damage = 20;
        bullet_pool_ = GameObject.Find("BulletPool");
        camera_ = GameObject.FindGameObjectWithTag("MainCamera");
        gun = new Gun(transform.Find("ShootingPoint"), bullet_pool_, bullet_force_, damage, 0);
        input_ = new InputController();
        
        input_.PlayerKeyboard.Movement.performed += move_performed =>
        {
            movement_ = move_performed.ReadValue<Vector2>();
        };

        input_.PlayerKeyboard.Movement.canceled += move_cancel =>
        {
            movement_ = Vector2.zero;
        };

        input_.PlayerKeyboard.Jump.performed += jump_performed =>
        {
            is_jumping_ = jump_performed.ReadValueAsButton();
        };

        input_.PlayerKeyboard.Jump.canceled += jump_cancel =>
        {
            is_jumping_ = jump_cancel.ReadValueAsButton();
        };

        input_.PlayerKeyboard.Fire.performed += fire_performed =>
        {
            is_firing_ = fire_performed.ReadValueAsButton();
        };
    }

    private void Start()
    {
        controller_ = gameObject.GetComponent<CharacterController>();
        input_.PlayerKeyboard.Enable();
    }

    void Update()
    {
        bool is_grounded = controller_.isGrounded;
        if (is_grounded && velocity_.y < 0)
        {
            velocity_.y = 0f;
        }

        Vector3 move = new Vector3(movement_.x, 0.0f, 0.0f);
        controller_.Move(move * Time.deltaTime * speed_);
        camera_.transform.Translate(new Vector3(12 + gameObject.transform.position.x - camera_.transform.position.x, 0, 0) * Time.deltaTime * speed_);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        if (is_jumping_ && is_grounded)
        {
            velocity_.y += Mathf.Sqrt(jump_force_ * -3.0f * -gravity_);
        }

        velocity_.y += -gravity_ * Time.deltaTime;
        controller_.Move(velocity_ * Time.deltaTime);

        if (is_firing_)
        {
            gun.Shoot(gameObject.transform.forward);
            is_firing_ = false;
        }

    }

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