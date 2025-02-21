using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnWall : MonoBehaviour
{
    [SerializeField] private Wall wallPrefab;
    [SerializeField] private float positionXSpawn;
    [SerializeField] private float timeSpawn = 3f;
    private float time;
    
    private float[] newHeight = new float[4];
    private float[] newPosition = new float[4];
    private float heightCam;

    private DataWall _dataWall;

    public DataWall DataWall
    {
        set => _dataWall = value;
    }

    private void Start()
    {
        heightCam = Camera.main.orthographicSize;
        time = timeSpawn;
    }

    private void Update()
    {
        if (GameManager.Instance.mode == ModeGame.Play)
        {
            time += Time.deltaTime;
            if (time >= timeSpawn)
            {
                SpawmWall();
                time = 0;
            }   
        }
    }

    public void SpawmWall()
    {
        newHeight[0] = (int)Random.Range(1, heightCam);
        newPosition[0] = newHeight[0] / 2;

        newHeight[1] = heightCam - newHeight[0];
        newPosition[1] = newHeight[1] / 2 + newHeight[0];

        newHeight[2] = (int)Random.Range(1, heightCam);
        newPosition[2] = -newHeight[2] / 2;

        newHeight[3] = heightCam - newHeight[2];
        newPosition[3] = -newHeight[2] - newHeight[3] / 2;

        for (int i = 0; i < 4; i++)
        {
            if(wallPrefab == null) return;
            Wall wall = PoolingManager.Spawn(wallPrefab, new Vector3(positionXSpawn, newPosition[i], 0), quaternion.identity, transform);
            wall.Init(_dataWall);
            wall.transform.localScale = new Vector3(1, newHeight[i]);
        }
    }
}
