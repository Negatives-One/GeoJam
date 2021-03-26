using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyBullet;

    public int enemiesPerSpawn = 1;
    public float spawnRate = 5;

    [SerializeField]
    private float[] topArea = new float[2];
    [SerializeField]
    private float[] botArea = new float[2];

    [SerializeField]
    private GameObject[] PathCima = new GameObject[1];
    [SerializeField]
    private GameObject[] PathBaixo = new GameObject[1];

    private IEnumerator currentCoroutine;

    public GameObject Player;

    public Vector3 SpawnpointCima;
    public Vector3 SpawnpointBaixo;

    private bool running = false;

    void Start()
    {
        //currentCoroutine = TimerToSpawn();
        StartCoroutine(TimerToSpawn());
        running = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!running)
        {
            SpawnEnemy();
            StartCoroutine(TimerToSpawn());
            running = true;
        }
    }

    private void SpawnEnemy()
    {
        int cimaOuBaixo = Random.Range(0, 2);
        if(cimaOuBaixo == 0)
        {
            GameObject enemy = Instantiate(enemyPrefab, SpawnpointCima, Quaternion.identity, transform);
            enemy.GetComponent<Enemy>().pathParent = PathCima[Random.Range(0, PathCima.Length)];
            enemy.GetComponent<Enemy>().Bullet = enemyBullet;
            enemy.GetComponent<Enemy>().player = Player;
        }
        else if(cimaOuBaixo == 1)
        {
            GameObject enemy = Instantiate(enemyPrefab, SpawnpointBaixo, Quaternion.identity, transform);
            enemy.GetComponent<Enemy>().pathParent = PathBaixo[Random.Range(0, PathCima.Length)];
            enemy.GetComponent<Enemy>().Bullet = enemyBullet;
            enemy.GetComponent<Enemy>().player = Player;
        }
    }

    private IEnumerator TimerToSpawn()
    {
        yield return new WaitForSeconds(1.7f);
        Debug.Log(1);
        running = false;
    }
}
