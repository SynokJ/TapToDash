using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player_tr;
    private Vector3 camera_offset;

    void Start()
    {
        camera_offset = transform.position - player_tr.position;
    }

    void LateUpdate()
    {
        Vector3 camera_pos = player_tr.position + camera_offset;
        transform.position = camera_pos;
    }
}
