using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordedAgent : MonoBehaviour
{
    public byte[] Record;

    private int _index;

    void Update()
    {
        if (_index >= Record.Length)
        {
            Destroy(gameObject);
            return;
        }

        var f1 = BitConverter.ToSingle(Record, _index);
        var f2 = BitConverter.ToSingle(Record, _index + sizeof(float));

        transform.Translate(new Vector2(f1, f2));

        _index += sizeof(float) * 2;
    }
}
