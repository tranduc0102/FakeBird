using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBackgroud : MonoBehaviour
{
   [SerializeField] private float smoothSpeed;
   [SerializeField] private float posStart;
   [SerializeField] private float posReturn;

   private void Start()
   {
      posStart = transform.position.x;
   }

   private void FixedUpdate()
   {
      if(GameManager.Instance.mode == ModeGame.EndGame) return;
      transform.Translate(smoothSpeed*Vector2.left);
      if (transform.position.x <= posReturn)
      {
         transform.position = new Vector3(posStart, transform.position.y, transform.position.z);
      }
   }
}
