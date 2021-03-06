using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "ScriptableObjects/Character")]

public class Character : ScriptableObject
{

    public new string name;
    public float speed;
    public int cost;
    public bool isOpen;

    public Material material;
    public Material[] test_material;
    public Mesh test_mesh;
}
