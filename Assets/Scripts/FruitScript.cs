using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour
{
    Rigidbody2D body;

    private bool isPhysics;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        body.gravityScale = 0;
        body.velocity = Vector2.zero;
        isPhysics = false;
    }    

    public void OnPhysics()
    {
        isPhysics = true;
        body.gravityScale = 1f;
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            OnPhysics();
            SpawnFruits._canSpawn = true;
        }
    }
}
