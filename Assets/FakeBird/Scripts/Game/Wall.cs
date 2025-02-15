using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
   [SerializeField] private float smoothSpeed;
   [SerializeField] private SpriteRenderer sr;
   [SerializeField] private int id;
   [SerializeField] private List<Sprite> spriteWall;
   [SerializeField] private float currentHealthPoint;
   [SerializeField] private float amountHealthPoint;
   
   [SerializeField] private float positionXDespawn;

   private void Start()
   {
      sr = GetComponent<SpriteRenderer>();
   }

   public void Init(DataWall data)
   {
      spriteWall = data.Sprites;
      currentHealthPoint = data.healthPoint;
      amountHealthPoint = data.healthPoint;
      id = data.id;
      if (TryGetComponent(out SpriteRenderer sr))
      {
         sr.sprite = spriteWall[0];
      }
   }
   public void TakeDamge(float damage)
   {
      currentHealthPoint -= damage;
      if (currentHealthPoint <= 0)
      {
         GameManager.Instance.Scorce += id * 20;
         PoolingManager.Despawn(gameObject);
      }
      if (currentHealthPoint <= amountHealthPoint/3)
      {
         sr.sprite = spriteWall[2];
      }else if (amountHealthPoint * 2 / 3 >= currentHealthPoint && currentHealthPoint > amountHealthPoint / 3)
      {
         sr.sprite = spriteWall[1];
      }
      else
      {
         sr.sprite = spriteWall[0];
      }
   }

   private void FixedUpdate()
   {
      if (GameManager.Instance.mode == ModeGame.Play)
      {
         transform.Translate(Vector3.left*smoothSpeed*Time.deltaTime);
         if (transform.position.x <= positionXDespawn)
         {
            PoolingManager.Despawn(gameObject);
         }  
      }
   }
}
