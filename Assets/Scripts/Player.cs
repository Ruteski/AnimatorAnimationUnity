using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;

    private Rigidbody2D _rig;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

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
    }
}
