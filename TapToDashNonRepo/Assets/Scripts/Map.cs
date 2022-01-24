using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Map", menuName = "ScriptableObjects/Map")]
public class Map : ScriptableObject
{
    public Sprite img;
    public int musicIndex;
    public new string name;
}