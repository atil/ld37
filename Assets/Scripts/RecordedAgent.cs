using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordedAgent : Agent
{
    public byte[] Record;

    private int _index;

    protected override void Update()
    {
        if (_index >= Record.Length)
        {
            return;
        }

        var f1 = BitConverter.ToSingle(Record, _index);
        var f2 = BitConverter.ToSingle(Record, _index + sizeof(float));

        transform.Translate(new Vector2(f1, f2));

        _index += sizeof(float) * 2;
    }
}
