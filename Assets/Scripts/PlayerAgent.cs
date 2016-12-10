using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAgent : Agent
{
    public System.Action<FrameData> Displaced;

    private const float Speed = 5f;
    private Vector2 _moveDir;

    protected override void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _moveDir = Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _moveDir = Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _moveDir = Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _moveDir = Vector2.right;
        }

        var oldPos = transform.position;
        transform.Translate(_moveDir * Speed * Time.deltaTime);
        var newPos = transform.position;

        Displaced(new FrameData()
        {
            X = (newPos - oldPos).x,
            Y = (newPos - oldPos).y
        });
    }
}
