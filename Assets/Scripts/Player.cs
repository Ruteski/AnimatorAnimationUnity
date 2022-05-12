using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 8f;
    [SerializeField] private float _jumpForce = 15f;
    [SerializeField] private float _startTimeAttack = 0.1f;
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _energy;
    [SerializeField] private Transform _backPoint;

    private Rigidbody2D _rig;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private bool _isGrounded = true;
    private float _timeAttack;

    // Start is called before the first frame update
    void Start()
    {
        _rig = GetComponent<Rigidbody2D>(); 
        _animator = GetComponent<Animator>();   
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            GameObject bullet = Instantiate(_energy);

            if (_spriteRenderer.flipX){
                bullet.transform.position = _backPoint.transform.position; 
            } else {
                bullet.transform.position = _target.transform.position;
            }
            AudioController.current.PlayMusic(AudioController.current.sfx);
        }
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

        if (_startTimeAttack <= 0) {
            if (Input.GetKey(KeyCode.Z)) {
                //_animator.SetTrigger("isAttacking");
                _animator.SetBool("isAtk", true);
                _timeAttack = _startTimeAttack;
            }
            
        } else {
            _timeAttack -= Time.deltaTime;
            //_animator.SetTrigger("isAttacking");
            _animator.SetBool("isAtk", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 6) {
            _isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Enemy")) {
            collision.gameObject.GetComponent<Animator>().SetTrigger("hit");
            Destroy(collision.gameObject, 2f);
            _rig.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            AudioController.current.PlayMusic(AudioController.current.anotherSfx);
        }
    }
}
