using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBounds : MonoBehaviour
{
     public static Action<GameObject> onBoundTrigger;
     private void OnTriggerEnter(Collider other)
     {
          if (other.CompareTag("Collectible")||other.CompareTag("Enemy"))
          {
               onBoundTrigger?.Invoke(other.gameObject);
               Destroy(other.gameObject);
          }
     }
}
