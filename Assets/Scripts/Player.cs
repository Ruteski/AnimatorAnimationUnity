using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _jumpForce = 15f;

    private Rigidbody2D _rig;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private bool _isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        _rig = GetComponent<Rigidbody2D>(); 
        _animator = GetComponent<Animator>();   
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            _rig.velocity = new Vector2(-_speed, _rig.velocity.y);
            _animator.SetBool("isWalking", true);
            _spriteRenderer.flipX = true;
        } else {
            _rig.velocity = new Vector2(0, _rig.velocity.y);
            _animator.SetBool("isWalking", false);
        }

        if (Input.GetKey(KeyCode.RightArrow)) {
            _rig.velocity = new Vector2(_speed, _rig.velocity.y);
            _animator.SetBool("isWalking", true);
            _spriteRenderer.flipX = false;
        }

        if (Input.GetKey(KeyCode.UpArrow) && _isGrounded) {
            _rig.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _isGrounded = false;
            _animator.SetTrigger("isJumping");
            _animator.SetBool("isWalking", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 6) {
            _isGrounded = true;
        }
    }
}
