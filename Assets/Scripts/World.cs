using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class World : MonoBehaviour
{
    public GameObject PlayerAgentPrefab;
    public GameObject RecordedAgentPrefab;

    private readonly List<List<byte>> _recordings = new List<List<byte>>();

    void Start()
    {
        var pl = Instantiate(PlayerAgentPrefab);
        _recordings.Add(new List<byte>());

        pl.GetComponent<PlayerAgent>().Displaced += frameData =>
        {
            var f1 = BitConverter.GetBytes(frameData.X);
            var f2 = BitConverter.GetBytes(frameData.Y);
            _recordings.Last().AddRange(f1);
            _recordings.Last().AddRange(f2);
        };
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            var bot = Instantiate(RecordedAgentPrefab);
            bot.GetComponent<RecordedAgent>().Record = _recordings.Last().ToArray();
        }
    }

}
