using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MergeManager : MonoBehaviour
{
    public static Action<FruitType, Vector2> onMergeProcessed; 

    private Fruit lastSender;

    void Start()
    {
        Fruit.onCollisionWithFruit += CollisionBetweenFruitsCallback;
    }


    void Update()
    {
        
    }

    private void CollisionBetweenFruitsCallback(Fruit sender, Fruit otherFruit)
    {
        if (lastSender != null)
            return;

        lastSender = sender;

        ProcessMerge(sender, otherFruit);

        //Debug.Log("Collision detected by = " + sender.name);
    }

    private void ProcessMerge(Fruit sender, Fruit otherFruit)
    {
        FruitType mergeFruitType = sender.GetFruitType();
        mergeFruitType += 1;

        Vector2 fruitSpawnPos = (sender.transform.position + otherFruit.transform.position) / 2;


        Destroy(sender.gameObject);
        Destroy(otherFruit.gameObject);

        StartCoroutine(ResetLastSenderCoroutine());

        onMergeProcessed?.Invoke(mergeFruitType, fruitSpawnPos);
    }

    IEnumerator ResetLastSenderCoroutine()
    {
        yield return new WaitForEndOfFrame();

        lastSender = null;
    }


}
