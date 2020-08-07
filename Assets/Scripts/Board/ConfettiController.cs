using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiController : MonoBehaviour
{
     [SerializeField]
     private GameObject confettiPrefab;

     private void OnEnable()
     {
          LevelManager.Instance.onBufferEmpty += SpawnConfetti;
     }

     private void SpawnConfetti(BoardIndex obj)
     {
          if (obj.Equals(BoardIndex.Second))
          {
               GameObject go=Instantiate(confettiPrefab, new Vector3(0, 0, 15), Quaternion.Euler(180f,0,0));
               Destroy(go, 2.5f);
          }
     }
}
