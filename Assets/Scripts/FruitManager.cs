using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [SerializeField] private Fruit[] fruitPrefabs;
    [SerializeField] private Fruit[] spawnableFruits;
    [SerializeField] private Transform fruitsParent;
    [SerializeField] private LineRenderer fruitSpawnLine;
    private Fruit currentFruit;

    [SerializeField] private float fruitSpawnPos;
    private bool canControl;
    private bool isControlling;

    [SerializeField] private bool enableGizmos;

    private void Awake()
    {
        MergeManager.onMergeProcessed += MergeProcessedCallBack;
    }
    void Start()
    {
        canControl = true;
        HideLine();
    }


    void Update()
    {
        if (canControl)
        {
            ManagePlayerInput();
        }
    }

    private void ManagePlayerInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            MouseDownCallBack();
        }

        else if(Input.GetMouseButton(0))
        {
            if(isControlling)
                MouseDragCallBack();
            else
            {
                MouseDownCallBack();
            }    
        }

        else if(Input.GetMouseButtonUp(0) && isControlling)
        {
            MouseUpCallBack();
        }
    }

    private void MouseDownCallBack()
    {
        DisplayLine();

        PlaceLineAtClickedPosition();

        SpawnFruit();

        isControlling = true;
    }

    private void MouseDragCallBack()
    {
        PlaceLineAtClickedPosition();

        currentFruit.MoveTo(new Vector2(GetSpawnPosition().x, fruitSpawnPos));
    }

    private void MouseUpCallBack()
    {
        HideLine();

        currentFruit.EnablePhysics();

        canControl = false;
        StartControlTimer();
        isControlling = false;
    }

    private void SpawnFruit()
    {
        Vector2 spawnPosition = GetSpawnPosition();
        Fruit fruitToInstantiate = spawnableFruits[Random.Range(0, spawnableFruits.Length)];

        currentFruit = Instantiate(fruitToInstantiate, spawnPosition, Quaternion.identity, fruitsParent);
    }

    private Vector2 GetClickedWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private Vector2 GetSpawnPosition()
    {
        Vector2 worldClickedPosition = GetClickedWorldPosition();
        worldClickedPosition.y = fruitSpawnPos;
        return worldClickedPosition;
    }

    private void PlaceLineAtClickedPosition()
    {
        fruitSpawnLine.SetPosition(0, GetSpawnPosition());
        fruitSpawnLine.SetPosition(1, GetSpawnPosition() + Vector2.down * 15f);
    }

    private void HideLine()
    {
        fruitSpawnLine.enabled = false;
    }

    private void DisplayLine()
    {
        fruitSpawnLine.enabled = true;
    }

    private void StartControlTimer()
    {
        Invoke("StopControlTimer", 1);
    }

    private void StopControlTimer()
    {
        canControl = true;
    }

    private void MergeProcessedCallBack(FruitType fruitType, Vector2 spawnPosition)
    {
        for (int i = 0; i < fruitPrefabs.Length; i++)
        {
            if (fruitPrefabs[i].GetFruitType() == fruitType)
            {
                Debug.Log("Merge");
                SpawnMergeFruit(fruitPrefabs[i], spawnPosition);
                break;
            }
        }
    }

    private void SpawnMergeFruit(Fruit fruit, Vector2 spawnPosition)
    {
        Fruit fruitInstance = Instantiate(fruit, spawnPosition, Quaternion.identity, fruitsParent);
        fruitInstance.EnablePhysics();
    }
#region UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!enableGizmos)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(-5, 3, 0), new Vector3(5, 3, 0));
    }
#endregion
}
