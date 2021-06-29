using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsGrid : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(10,10);

    public GameObject _TowerPanel;
    public Button _UpgradeRange, _UpgradeSpeed, _Sell;
    public Text RangeText, _RangeCost, _RangeLevel,_SpeedText, _SpeedCost, _SpeedLevel;
    private Tower[,] grid;
    [HideInInspector]public Tower flyingTower;
    private TowerSettings TowerSettings;
    private Camera mainCamera;
    public GameManager GameManager;
    public GameObject[] Road;
    private void Awake()
    {
        grid = new Tower[GridSize.x, GridSize.y];

        mainCamera = Camera.main;
    }

    public void ButtonText(Tower x)
    {
        
    }

    public void StartPlacingTower(Tower towerPrefab)
    {
        if (flyingTower != null)
        {
            Destroy(flyingTower.gameObject);
        }

        flyingTower = Instantiate(towerPrefab);
    }
    
    private void Update()
    {
        if (flyingTower != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);

                bool availible = true;

                if (x < 0 || x > GridSize.x - flyingTower.Size.x) availible = false;
                if (y < 0 || y > GridSize.y - flyingTower.Size.y) availible = false;

                if (availible && (IsRoad(x,y) || IsPlaceTaken(x, y))) availible = false;


                flyingTower.transform.position = new Vector3(x,0,y);
                flyingTower.SetTtransparent(availible);

                if (availible && Input.GetMouseButtonDown(0) && (GameManager.PlayerMoney >= flyingTower.GetComponent<TowerSettings>().Price))
                {
                    GameManager.PlayerMoney = GameManager.PlayerMoney - flyingTower.GetComponent<TowerSettings>().Price;
                    PlaceFlyingTower(x,y);
                    GameManager.UIupdate();
                }

                if (!availible && Input.GetMouseButtonDown(0))
                {
                    Destroy(flyingTower.gameObject);
                }
            }
        }
    }

    private bool IsRoad(int PlaceX,int PlaceY)
    {
        for (int i = 0; i < Road.Length; i++)
        {
            if (Road[i].transform.position.x == PlaceX && Road[i].transform.position.z == PlaceY)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < flyingTower.Size.x; x++)
        {
            for (int y = 0; y < flyingTower.Size.y; y++)
            {
                if (grid[placeX + x, placeY + y] != null)
                {
                    return true;
                }
            }
        }
        return false;
    }
    
    private void PlaceFlyingTower(int placeX, int placeY)
    {
        for (int x = 0; x < flyingTower.Size.x; x++)
        {
            for (int y = 0; y < flyingTower.Size.y; y++)
            {
                grid[placeX + x, placeY + y] = flyingTower;
            }
        }
        flyingTower.GetComponent<TowerManager>().Param(_UpgradeRange, _UpgradeSpeed, _Sell, _TowerPanel, GameManager);
        flyingTower.GetComponent<TowerManager>().TextParam(RangeText, _RangeCost, _RangeLevel,_SpeedText, _SpeedCost, _SpeedLevel);
        flyingTower.SetNormal();
        flyingTower = null;
    }

    private void OnDrawGizmos()
    {
        if (flyingTower != null)
        {
            for (int x = 0; x < GridSize.x; x++)
            {
                for (int y = 0; y < GridSize.y; y++)
                {
                    if (!IsRoad(x,y) && !IsPlaceTaken(x,y))
                    {
                        if ((x + y) % 2 == 0)
                        {
                            Gizmos.color = new Color(0.97f, 1f, 0f, 0.28f);
                        }
                        else
                        {
                            Gizmos.color = new Color(1f, 0.89f, 0.01f, 0.57f);
                        }
                    }
                    else
                    {
                        Gizmos.color= new Color(1f, 0f, 0f, 0f);
                    }
                    Gizmos.DrawCube(new Vector3(x,0,y), new Vector3(1,.1f,1)); 
                }
            }
        }
    }
}
