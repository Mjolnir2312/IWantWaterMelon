using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : MonoBehaviour
{
    [SerializeField] private GameObject fruitsPrefab;
    [SerializeField] private LineRenderer fruitSpawnLine;

    [SerializeField] private float fruitSpawnPos;

    [SerializeField] private bool enableGizmos;
    void Start()
    {
        
    }


    void Update()
    {
        ManagePlayerInput();
    }

    private void ManagePlayerInput()
    {
        if(Input.GetMouseButtonDown(0))
        {
            MouseDownCallBack();
        }
        else if(Input.GetMouseButton(0))
        {
            MouseDragCallBack();
        }
        else if(Input.GetMouseButtonUp(0))
        {
            MouseUpCallBack();
        }
    }

    private void MouseDownCallBack()
    {
        DisplayLine();

        fruitSpawnLine.SetPosition(0, GetSpawnPosition());
        fruitSpawnLine.SetPosition(1, GetSpawnPosition() + Vector2.down * 15f); 
    }

    private void MouseDragCallBack()
    {
        fruitSpawnLine.SetPosition(0, GetSpawnPosition());
        fruitSpawnLine.SetPosition(1, GetSpawnPosition() + Vector2.down * 15f);
    }

    private void MouseUpCallBack()
    {
        HideLine();
    }

    private void SpawnFruit()
    {
        Vector2 spawnPosition = GetSpawnPosition();

        Instantiate(fruitsPrefab, spawnPosition, Quaternion.identity);
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

    private void HideLine()
    {
        fruitSpawnLine.enabled = false;
    }

    private void DisplayLine()
    {
        fruitSpawnLine.enabled = true;
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
