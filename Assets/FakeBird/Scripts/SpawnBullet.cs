using System;
using UnityEngine;

public class SpawnBullet : MonoBehaviour
{
    public Bullet bulletPrefabs;
    [SerializeField] private float timeDelaySpawn = 0.2f;
    private float timeSpawnBullet = 0;
    private int index = 0;
    private DataBullet dataBullet;

    public DataBullet DataBullet
    {
        set => dataBullet = value;
    }
    private void Reset()
    {
        bulletPrefabs = Resources.Load<Bullet>("Bullet");
    }

    private void Start()
    {
        if (bulletPrefabs == null)
        {
            bulletPrefabs = Resources.Load<Bullet>("Bullet");
        }
        Spawn();
    }
    void Update()
    {
        if (GameManager.Instance.mode == ModeGame.Play)
        {
            timeSpawnBullet += Time.deltaTime;
            if (timeSpawnBullet >= timeDelaySpawn)
            {
                Spawn();
                timeSpawnBullet = 0;
            }   
        }
    }
    void Spawn(){
        Bullet bullet = PoolingManager.Spawn(bulletPrefabs, transform.position,Quaternion.Euler(0f, 0f, -90));
        bullet.Init(dataBullet);
    }
}
