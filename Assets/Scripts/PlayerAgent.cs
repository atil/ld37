using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAgent : MonoBehaviour
{
    public System.Action<float, float> Displaced;
    public System.Action TriggeredExit;

    private const float Speed = 5f;
    private Vector2 _moveDir;

    private float Cos(float angle)
    {
        return Mathf.Abs(Mathf.Cos(angle));
    }
    private float Sin(float angle)
    {
        return Mathf.Abs(Mathf.Sin(angle));
    }

    public void Init(Vector3 v, bool firstRun)
    {
        transform.position = v;
        if (!firstRun)
        {
            var angle = Vector3.Angle(Vector2.right, v);
            _moveDir = new Vector2(-Mathf.Sign(v.x) * (Cos(angle) < Sin(angle) ? 1f : 0f), 
                -Mathf.Sign(v.y) * (Cos(angle) > Sin(angle) ? 1f : 0f));
        }
    }

    void Update()
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

        Displaced((newPos - oldPos).x, (newPos - oldPos).y);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.layer == LayerMask.NameToLayer("Exit"))
        {
            Debug.Log("Exit!");

            TriggeredExit();
        }
    }
}
