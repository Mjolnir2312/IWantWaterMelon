using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFruits : MonoBehaviour
{
    public FruitScript[] objs;

    [SerializeField] private Transform _spawnPoint;

    private Vector3 spawnPoint;
    public static bool _canSpawn;
    private void Awake()
    {
        FruitScript newObj = Instantiate(objs[Random.Range(0, 4)], _spawnPoint.transform.position, Quaternion.identity);
        _canSpawn = false;
    }

    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;


        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        spawnPoint = new Vector3(worldPosition.x, _spawnPoint.position.y, 0);

        transform.position = spawnPoint;

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(SpawnDelay());
        }
    }

    void SpawnFruit()
    {
        FruitScript newObj = Instantiate(objs[Random.Range(0, 4)], transform.position, Quaternion.identity);
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(0.75f);
        SpawnFruit();
    }
}
