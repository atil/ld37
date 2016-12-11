using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAgent : MonoBehaviour
{
    public System.Action<float, float> Displaced;
    public System.Action TriggeredExit;
    public System.Action TriggeredBound;
    public System.Action TriggeredRecord;

    private const float Speed = 5f;
    private Vector2 _moveDir;
    private LayerMask _exitLayer;
    private LayerMask _boundLayer;
    private LayerMask _recordLayer;

    private float Cos(float angle)
    {
        return Mathf.Abs(Mathf.Cos(angle));
    }
    private float Sin(float angle)
    {
        return Mathf.Abs(Mathf.Sin(angle));
    }

    public void Init(Vector3 v, bool giveInitialPush)
    {
        _exitLayer = LayerMask.NameToLayer("Exit");
        _boundLayer = LayerMask.NameToLayer("Bound");
        _recordLayer = LayerMask.NameToLayer("Record");

        transform.position = v;

        if (giveInitialPush)
        {
            var angle = Vector3.Angle(Vector2.right, (Vector2)v) * Mathf.Deg2Rad;
            _moveDir = new Vector2(-Mathf.Sign(v.x) * (Cos(angle) > Sin(angle) ? 1f : 0f), 
                -Mathf.Sign(v.y) * (Cos(angle) < Sin(angle) ? 1f : 0f));
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
        var deltaMove = transform.position - oldPos;

        Displaced(deltaMove.x, deltaMove.y);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.layer == _exitLayer)
        {
            TriggeredExit();
        }

        if (coll.gameObject.layer == _boundLayer)
        {
            TriggeredBound();
        }

        if (coll.gameObject.layer == _recordLayer)
        {
            TriggeredRecord();
        }

    }
}
