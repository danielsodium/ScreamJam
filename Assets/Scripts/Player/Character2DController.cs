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
    public float check_radius = 0.1f;

    public GameObject left_leg;
    public GameObject right_leg;
    public GameObject left_arm;
    public GameObject right_arm;
    Rigidbody2D left_legRB;
    Rigidbody2D right_legRB;

    public Animator anim;

    private float _movement;
    private bool _is_space_down;
    private bool _facing_right = true;
/*    private Vector3 delta;
    private Vector3 previous_position;
    private Transform[] children_transform;
*/
    private void Start()
    {
        left_legRB = left_leg.GetComponent<Rigidbody2D>();
        right_legRB = right_leg.GetComponent<Rigidbody2D>();
        Vector3 transform_vector = transform.localScale;
        _facing_right = (transform_vector.x > 0);
/*        previous_position = body_collider.bounds.center;
        children_transform = GetComponentsInChildren<Transform>();
*/    }
    private void Update()
    {
        _movement = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            _is_space_down = true;
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

    private void FixedUpdate()
    {
        if (_is_space_down && isGrounded())
        {
            jump();
            _is_space_down = false;
        }
        /*delta = body_collider.bounds.center - previous_position;
        this.gameObject.transform.position += delta;
        foreach (Transform i in children_transform)
        {
            i.position -= delta;
        }
        previous_position = body_collider.bounds.center;*/

    }

    
    private bool isGrounded()
    {
        if (Physics2D.OverlapCircle(ground_check_transform.position, check_radius, player_mask))
        {
            return true;
        }
        return false;
    }

    private void flip()
    {
        _facing_right = !_facing_right;
        Vector3 transform_scale = transform.localScale;
        transform_scale.x *= -1;
        transform.localScale = transform_scale;
        left_arm.GetComponent<Balance>().targetRotation *= -1;
        right_arm.GetComponent<Balance>().targetRotation *= -1;
    }

    private void jump()
    {

    }

    IEnumerator walkRight(float seconds)
    {
        right_legRB.AddForce(Vector2.right * (MovementSpeed * 1000) * Time.deltaTime);
        yield return new WaitForSeconds(seconds);
        left_legRB.AddForce(Vector2.right * (MovementSpeed * 1000) * Time.deltaTime);

    }

    IEnumerator walkLeft(float seconds)
    {
        left_legRB.AddForce(Vector2.left * (MovementSpeed * 1000) * Time.deltaTime);
        yield return new WaitForSeconds(seconds);
        right_legRB.AddForce(Vector2.left * (MovementSpeed * 1000) * Time.deltaTime);

    }


}
