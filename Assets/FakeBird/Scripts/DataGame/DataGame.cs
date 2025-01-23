using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Data", fileName = "Data Game")]
public class DataGame : ScriptableObject
{
    public List<DataWall> DataWalls;
    public List<DataBullet> DataBullet;
}
[Serializable]
public class DataWall
{
    public string name;
    public int id;
    public float healthPoint;
    public List<Sprite> Sprites;
}
[Serializable]
public class DataBullet
{
    public string name;
    public Sprite sprite;
    public float damage;
}