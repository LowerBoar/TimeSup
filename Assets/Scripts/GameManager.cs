using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player PlayerPrefab;
    public Enemy EnemyPrefab;

    private List<Player> players;

    void Start()
    {
        players = new List<Player>();

        SpawnPlayer();
        SpawnEnemy();
    }

    void Update()
    {
        
    }

    private void SpawnPlayer()
    {
        var player = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
        player.transform.SetParent(transform);
        players.Add(player);
    }

    private void SpawnEnemy()
    {
        var x = Random.Range(-12f, 12f);
        var y = Random.Range(-12f, 12f);

        var enemy = Instantiate(EnemyPrefab, transform.position + new Vector3(x, y, 0), Quaternion.identity);
        enemy.transform.SetParent(transform);
        enemy.SetTarget(players.Last());
    }
}
