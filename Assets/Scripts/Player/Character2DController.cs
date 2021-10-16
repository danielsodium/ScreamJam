using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character2DController : MonoBehaviour
{
    [SerializeField] private float MovementSpeed = 1;
    [SerializeField] private float JumpForce = 1;
    [SerializeField] private float step_wait = 0.5f;
    public LayerMask player_mask;
    public Transform ground_check_transform;
    public float check_radius = 0.3f;

    public GameObject left_leg;
    public GameObject right_leg;
    public GameObject left_arm;
    public GameObject right_arm;
    Rigidbody2D left_legRB;
    Rigidbody2D right_legRB;
    Rigidbody2D left_armRB;
    Rigidbody2D right_armRB;
     
    public bool grounded;


    public Animator anim;

    private float _movement;
    private bool _is_space_down;
    private bool _facing_right = true;


    private void Start()
    {
        left_legRB = left_leg.GetComponent<Rigidbody2D>();
        right_legRB = right_leg.GetComponent<Rigidbody2D>();
        left_armRB = left_arm.GetComponent<Rigidbody2D>();
        right_armRB = right_arm.GetComponent<Rigidbody2D>();
        Vector3 transform_vector = transform.localScale;
        _facing_right = (transform_vector.x > 0);

    }
    private void Update()
    {
        _movement = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            _is_space_down = true;
        }
    }
    void OnCollisionEnter(Collision theCollision){
        if(theCollision.gameObject.name == "floor")
        {
            print("GROUNDED");
            grounded = true;
        }
    }
    
    //consider when character is jumping .. it will exit collision.
    void OnCollisionExit(Collision theCollision){
        if(theCollision.gameObject.name == "floor")
        {
            grounded = false;
        }
    }

    private void FixedUpdate()
    {
        if (_is_space_down && grounded)
        {
            StartCoroutine(jump(step_wait));
            _is_space_down = false;
        }
        if (_movement == 0)
        {
            anim.Play("player_idle");
        }
        else
        {
            if (_movement > 0)
            {
                StartCoroutine(walkRight(step_wait));
                if (!_facing_right)
                {
                    flip();
                }
                anim.Play("player_walkright");
            }
            else
            {
                StartCoroutine(walkLeft(step_wait));
                if (_facing_right)
                {
                    flip();
                }
                anim.Play("player_walkleft");
            }
        }

    }

    private void flip()
    {
        // Yeah i tried scaling and rotations but nothing works lol
        _facing_right = !_facing_right;
        /*
        Vector3 transform_scale = transform.localScale;
        transform_scale.x *= -1;
        transform.localScale = transform_scale;*/
    }

    IEnumerator jump(float seconds)
    {
        right_legRB.AddForce(Vector2.up * (JumpForce * 10000) * Time.deltaTime);
        yield return new WaitForSeconds(seconds);
        left_legRB.AddForce(Vector2.up * (JumpForce * 10000) * Time.deltaTime);
    }

    IEnumerator walkRight(float seconds)
    {
        right_armRB.AddForce(Vector2.right * (MovementSpeed * 1000) * Time.deltaTime);
        yield return new WaitForSeconds(seconds);
        left_armRB.AddForce(Vector2.right * (MovementSpeed * 1000) * Time.deltaTime);

    }

    IEnumerator walkLeft(float seconds)
    {
        left_legRB.AddForce(Vector2.left * (MovementSpeed * 1000) * Time.deltaTime);
        yield return new WaitForSeconds(seconds);
        right_legRB.AddForce(Vector2.left * (MovementSpeed * 1000) * Time.deltaTime);

    }


}
