using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [SerializeField] private Fruit[] fruitsPrefabs;
    [SerializeField] private LineRenderer fruitSpawnLine;
    private Fruit currentFruit;

    [SerializeField] private float fruitSpawnPos;
    private bool canControl;
    private bool isControlling;

    [SerializeField] private bool enableGizmos;
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

        currentFruit = Instantiate(fruitsPrefabs[Random.Range(0, fruitsPrefabs.Length)], spawnPosition, Quaternion.identity);
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

#region UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!enableGizmos)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(-5, fruitSpawnPos, 0), new Vector3(5, fruitSpawnPos, 0));
    }
#endregion
}
