using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyController : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private Player _player;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private bool _direction;

    private void Start() {
        _player = FindObjectOfType<Player>();
        _spriteRenderer = _player.GetComponent<SpriteRenderer>();
        _direction = _spriteRenderer.flipX;

        Destroy(gameObject, 3f);

        //print(_player.gameObject.tag);
    }

    // Update is called once per frame
    void Update()
    {
        if (_direction){
            transform.Translate(Vector2.left * Time.deltaTime * _speed );   
        } else {
            transform.Translate(Vector2.right * Time.deltaTime * _speed);
        }
    }
}
