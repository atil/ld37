using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class World : MonoBehaviour
{
    public GameObject PlayerAgentPrefab;
    public GameObject RecordedAgentPrefab;
    public GameObject ExitPrefab;
    public Ui Ui;

    public List<GameObject> Walls;

    private readonly List<List<byte>> _recordings = new List<List<byte>>();
    private readonly List<Vector3> _startPoints = new List<Vector3>();

    private GameObject _exitGo;
    private PlayerAgent _player;

    private int _score;
    private int _highScore;

    void Start()
    {
        _player = Instantiate(PlayerAgentPrefab).GetComponent<PlayerAgent>();
        _exitGo = Instantiate(ExitPrefab);

        _recordings.Add(new List<byte>());

        _player.Displaced += (x, y) =>
        {
            var f1 = BitConverter.GetBytes(x);
            var f2 = BitConverter.GetBytes(y);
            _recordings.Last().AddRange(f1);
            _recordings.Last().AddRange(f2);

        };

        _player.TriggeredExit += () =>
        {
            var bot = Instantiate(RecordedAgentPrefab, _startPoints.Last(), Quaternion.identity);
            bot.GetComponent<RecordedAgent>().Record = _recordings.Last().ToArray();
            _recordings.Add(new List<byte>());

            _score++;

            if (_score > _highScore)
            {
                _highScore = _score;
            }

            Ui.UpdateScore(_score, _highScore);

            SpawnEnterExit(true);
        };

        _player.TriggeredBound += GameOver;

        _player.TriggeredRecord += GameOver;

        Ui.RestartTrigger += () =>
        {
            _player.enabled = true;

            SpawnEnterExit(true);
        };

        SpawnEnterExit(true);
    }

    private void GameOver()
    {
        _player.enabled = false;
        Ui.GameOver(_score, _highScore);
        _recordings.Clear();
        _recordings.Add(new List<byte>());
        _score = 0;
    }

    private void SpawnEnterExit(bool givePlayerPush)
    {
        // Spawn random enter and exit points within walls
        var w1 = Walls[Random.Range(0, Walls.Count - 1)];
        GameObject w2;
        do
        {
            w2 = Walls[Random.Range(0, Walls.Count - 1)];
        } while (w1 == w2);

        var randPnt1 = RandomPointIn(w1.GetComponent<BoxCollider2D>().bounds);
        var randPnt2 = RandomPointIn(w2.GetComponent<BoxCollider2D>().bounds);

        var playerPos = new Vector3(randPnt1.x, randPnt1.y, -2);
        _player.Init(playerPos, givePlayerPush);
        _startPoints.Add(playerPos);

        _exitGo.transform.position = new Vector3(randPnt2.x, randPnt2.y, -2); ;

    }

    private Vector2 RandomPointIn(Bounds bnds)
    {
        return (Vector2)bnds.center +
               new Vector2(Random.Range(-1f, 1f) * bnds.extents.x, Random.Range(-1f, 1f) * bnds.extents.y);
    }

}
