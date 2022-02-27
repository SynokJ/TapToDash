using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{

    // 22:22 TODO
    public GameObject plane;
    public MapItem flyingItem_pref;

    private Vector2Int GridSize = new Vector2Int(11, 20);
    private MapItem[,] map;
    private Camera mainCamera;
    private MapItem flyingItem;
    private Touch touch;

    private float offset_x;
    private float offset_y;

    void Start()
    {
        map = new MapItem[GridSize.x, GridSize.y];

        offset_x = GridSize.x / 2;
        offset_y = GridSize.y / 2;

        if (GridSize.x % 2 != 0)
            offset_x += 1;

        mainCamera = Camera.main;

        flyingItem = Instantiate(flyingItem_pref);

        InitBackgroundPlane();
    }

    void Update()
    {
        if (Input.touchCount != 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
                StartPlacingItem(flyingItem_pref);
        }

        if (flyingItem != null && Input.touchCount != 0)
        {

            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = mainCamera.ScreenPointToRay(touch.position);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPossition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPossition.x);
                int y = Mathf.RoundToInt(worldPossition.z);

                bool available = true;

                int temp_x = x + Mathf.RoundToInt(offset_x) - flyingItem_pref.Size.x;
                int temp_y = y + Mathf.RoundToInt(offset_y) - flyingItem_pref.Size.y;

                if (temp_x < 0 || temp_x > GridSize.x - flyingItem.Size.x)
                    available = false;

                if (temp_y < 0 || temp_y > GridSize.y - flyingItem.Size.y)
                    available = false;

                if (available && IsPlaceTaken(temp_x, temp_y)) available = false;

                flyingItem.transform.position = new Vector3(x, 0, y);
                flyingItem.SetTransparent(available);

                if (available && touch.phase == TouchPhase.Ended)
                {
                    PlaceFlyingItem(temp_x, temp_y);
                    flyingItem.SetNormal();
                    flyingItem = null;
                }
            }
        }
    }

    private void StartPlacingItem(MapItem item)
    {
        if (flyingItem != null)
            Destroy(flyingItem.gameObject);

        flyingItem = Instantiate(item);
    }

    private void InitBackgroundPlane()
    {
        plane.transform.localScale = new Vector3((float)GridSize.x / 10, 1, (float)GridSize.y / 10 + 0.1f);
        plane.transform.Translate(0, 0, 0.5f);
    }

    private bool IsPlaceTaken(int posX, int posY)
    {
        for (int x = 0; x < flyingItem_pref.Size.x; ++x)
            for (int y = 0; y < flyingItem_pref.Size.y; ++y)
                if (map[posX + x, posY + y] != null) return true;

        return false;
    }

    private void PlaceFlyingItem(int placeX, int placeY)
    {
        for (int x = 0; x < flyingItem_pref.Size.x; ++x)
            for (int y = 0; y < flyingItem_pref.Size.y; ++y)
            {
                map[placeX, placeY] = flyingItem_pref;
            }
    }

    public List<string> GetCustomLevel()
    {

        List<string> res = new List<string>();

        for (int y = 0; y < map.GetLength(1); ++y)
        {
            string line = "";

            for (int x = 0; x < map.GetLength(0); ++x)
            {
                if (map[x, y] != null)
                    line += "#";
                else
                    line += ".";

            }

            res.Add(line);
        }

        return res;
    }
}

/*
                     ".....#.....",
					".....#.....",
					".....#.....",
					".....#.....",
					".....#.....",
					".....#.....",
					".....#.....",
					".....#.....",
					".....#.....",
					".....#.....",
					".....#.....",
					".....#.....",
					".....#.....",
					".....#.....",
					".....#.....",
					".....#.....",
					".....#.....",
					".....#.....",
					".....#.....",
					".....#....."
*/