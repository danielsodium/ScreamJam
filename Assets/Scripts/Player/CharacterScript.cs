using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public float MovementSpeed = 1;
    public float JumpForce = 1;
    private float facing_right = 1;
    public Animator anim;
    private Rigidbody2D _rigidbody;
   private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
      Move();
    }
    private void Move() {
      var movement = Input.GetAxis("Horizontal");
      if (movement != 0) anim.Play("zombie_walk");
      if (facing_right != Mathf.Sign(movement)) flip();
      if (movement > 0) facing_right = 1;
      else if (movement < 0) facing_right = -1;
      float moveBy = movement * MovementSpeed; 
      _rigidbody.velocity = new Vector2(moveBy, _rigidbody.velocity.y); 
      if(Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
      {
          _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
      }
    }
     private void flip()
    {
        facing_right = -facing_right;
        Vector3 transform_scale = transform.localScale;
        transform_scale.x *= -1;
        transform.localScale = transform_scale;
    }
}
