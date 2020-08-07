using DG.Tweening;
using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
     private void OnEnable()
     {
          LevelManager.Instance.onBufferEmpty += MoveGate;
     }

     private void MoveGate(BoardIndex obj)
     {
          if (obj.Equals(BoardIndex.First))
          {
               transform.DOMoveY(-0.35f, 1f);
          }
     }


}
