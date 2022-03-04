using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapItem : MonoBehaviour
{
    public Renderer mainRenderer;
    public Vector2Int Size = Vector2Int.one;


    // Draw gizmo 
    public void OnDrawGizmosSelected()
    {
        for (int x = 0; x < Size.x; ++x)
            for (int y = 0; y < Size.y; ++y)
            {
                Gizmos.color = (x + y) % 2 == 0 ? Color.green : Color.red;
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, 0.1f, 1));
            }
    }

    // Check the correctness of placing an object 
    public void SetTransparent(bool available)
    {
        mainRenderer.material.color = available ? Color.green : Color.red; 
    }

    // Set the default color 
    public void SetNormal()
    {
        mainRenderer.material.color = Color.white;
    }
}
