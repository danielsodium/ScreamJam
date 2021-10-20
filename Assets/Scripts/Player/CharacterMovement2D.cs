
using UnityEngine;


public class CharacterMovement2D : MonoBehaviour
{
    public float MovementSpeed = 1;
    public float JumpForce = 1;
    public LayerMask player_mask;
    public Transform ground_check_transform;
    public float check_radius = 0.3f;

    private Rigidbody2D _rigidbody;
    private BoxCollider2D _box_collider;
    private bool _is_space_down;
    private bool facing_right = true;
    private float _movement;

   private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _box_collider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        _movement = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            _is_space_down = true;
        }
    }

    private void FixedUpdate()
    {
        if (_is_space_down && isGrounded())
        {
            _rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            _is_space_down = false;
        }
        if (_movement > 0 && !facing_right)
        {
            flip();
        }
        if (_movement < 0 && facing_right)
        {
            flip();
        }
        _rigidbody.position += new Vector2(_movement, 0) * Time.deltaTime * MovementSpeed;
        
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
        facing_right = !facing_right;
        Vector3 transform_scale = transform.localScale;
        transform_scale.x *= -1;
        transform.localScale = transform_scale;
    }
}
