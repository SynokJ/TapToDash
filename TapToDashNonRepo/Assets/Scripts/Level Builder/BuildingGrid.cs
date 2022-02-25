using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{

    // 22:22 TODO

    public Vector2Int GridSize = new Vector2Int(10, 10);
    public MapItem flyingItem_pref;

    private MapItem[,] map;
    private Camera mainCamera;
    private MapItem flyingItem;

    private float offset_x;
    private float offset_y;


    void Start()
    {
        map = new MapItem[GridSize.x, GridSize.y];
        offset_x = GridSize.x / 2;
        offset_y = GridSize.y / 2;

        mainCamera = Camera.main;

        flyingItem = Instantiate(flyingItem_pref);
    }

    void Update()
    {
        if (flyingItem != null && Input.touchCount != 0)
        {
            Touch touch = Input.GetTouch(0);

            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = mainCamera.ScreenPointToRay(touch.position);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPossition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPossition.x);
                int y = Mathf.RoundToInt(worldPossition.z);

                bool available = true;

                if (x < 0 - offset_x + flyingItem_pref.Size.x || x > GridSize.x - flyingItem_pref.Size.x - offset_x)
                    available = false;

                if (y < 0 - offset_y + flyingItem_pref.Size.y || y > GridSize.y - flyingItem_pref.Size.y - offset_y)
                    available = false;

                flyingItem.transform.position = new Vector3(x, 0, y);

                if (available && touch.phase == TouchPhase.Ended)
                    flyingItem = Instantiate(flyingItem_pref);
            }

            Debug.Log(touch.position.ToString());
        }
    }
}
