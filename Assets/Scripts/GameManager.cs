using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;

    void Start()
    {
        SpawnEnemy();
    }

    void Update()
    {
        
    }

    private void SpawnEnemy()
    {
        var x = Random.Range(-12f, 12f);
        var y = Random.Range(-12f, 12f);

        var enemy = Instantiate(EnemyPrefab, transform.position + new Vector3(x, y, 0), Quaternion.identity);
        enemy.transform.SetParent(transform);
    }
}
