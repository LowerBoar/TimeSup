using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player PlayerPrefab;
    public Enemy EnemyPrefab;

    private List<Player> players;
    private List<InputRecorder> recorders;

    void Start()
    {
        players = new List<Player>();
        recorders = new List<InputRecorder>();

        SpawnPlayer();
        SpawnEnemy();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            recorders.Clear();
            var amount = players.Count;
            foreach (var player in players.ToArray()) {
                recorders.Add(player.GetInputRecorder());
                Destroy(player.gameObject);
            }
            players.Clear();

            // TODO Destroy enemies
            for (var i = 0; i < amount; ++i) {  // TODO Make dedicated function for players spawning
                SpawnPlayer(i);
                players.Last().SetupInputRecorder(recorders[i]);
            }
            SpawnPlayer();

            SpawnEnemy();
        }
    }

    // TODO Split to different functions for manual and non-manual controlled players or think of some other way
    private void SpawnPlayer(int recorderNumber = -1)
    {
        var player = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
        player.transform.SetParent(transform);
        players.Add(player);

        player.SetupInputRecorder(recorderNumber >= 0 ? recorders[recorderNumber] : null);  
    }

    private void SpawnEnemy()
    {
        var x = Random.Range(5f, 7f);
        var y = Random.Range(5f, 7f);

        if (Random.Range(1, 10) >= 5) {
            x = -x;
        }

        if (Random.Range(1, 10) >= 5) {
            y = -y;
        }

        var enemy = Instantiate(EnemyPrefab, transform.position + new Vector3(x, y, 0), Quaternion.identity);
        enemy.transform.SetParent(transform);
        enemy.SetTarget(players.Last());
    }
}
