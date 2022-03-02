using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller_;
    private InputController input_;
    private Vector2 movement_;
    private Vector3 velocity_;
    private bool is_jumping_;
    private bool is_firing_;
    private bool is_loaded_;
    private GameObject camera_;
    private Transform shooting_point_;

    public float bullet_force_ = 50;
    public float speed_;
    public float jump_force_;
    public float gravity_;
    public GameObject bullet_;

    private void Awake()
    {
        camera_ = GameObject.FindGameObjectWithTag("MainCamera");
        input_ = new InputController();
        shooting_point_ = GameObject.Find("ShootingPoint").transform;

        input_.PlayerKeyboard.Movement.performed += move_performed =>
        {
            movement_ = move_performed.ReadValue<Vector2>();
            Debug.Log("Move: " + movement_);
        };

        input_.PlayerKeyboard.Movement.canceled += move_cancel =>
        {
            movement_ = Vector2.zero;
        };

        input_.PlayerKeyboard.Jump.performed += jump_performed =>
        {
            is_jumping_ = jump_performed.ReadValueAsButton();
            Debug.Log("Jump: " + is_jumping_);
        };

        input_.PlayerKeyboard.Jump.canceled += jump_cancel =>
        {
            is_jumping_ = jump_cancel.ReadValueAsButton();
            Debug.Log("Jump: " + is_jumping_);
        };

        input_.PlayerKeyboard.Fire.performed += fire_performed =>
        {
            is_firing_ = fire_performed.ReadValueAsButton();
            is_loaded_ = true;
        };

        input_.PlayerKeyboard.Fire.canceled += fire_cancel =>
        {
            is_firing_ = false;
        };
    }

    void Start()
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
        camera_.transform.Translate(move * Time.deltaTime * speed_);

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
        //camera_.transform.Translate(velocity_ * Time.deltaTime);

        if (is_firing_)
        { 
            if (is_loaded_)
            {
                GameObject bullet = Instantiate(bullet_, shooting_point_.position, Quaternion.identity);
                bullet.AddComponent<BulletBehaviour>();
                Rigidbody bullet_rb = bullet.GetComponent<Rigidbody>();
                bullet_rb.AddForce(gameObject.transform.forward * bullet_force_, ForceMode.Impulse);
            }
            is_loaded_ = false;
        }

    }
}
