using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BallMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float forceShoot;
    private Vector2 oldPos;
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        oldPos = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Movement();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            _rigidbody2D.velocity = Vector2.zero;
            transform.position = oldPos;
        }
    }

    private void Movement()
    {
        _rigidbody2D.AddForce(Vector2.right*forceShoot, ForceMode2D.Impulse);
    }
}
