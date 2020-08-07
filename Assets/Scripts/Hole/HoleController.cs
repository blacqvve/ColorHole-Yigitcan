using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
     public float distance;

     private void CollectibleContact(Collider obj)
     {
          var rigidbody = obj.gameObject.GetComponent<Rigidbody>();
          var direction = transform.parent.position - obj.gameObject.transform.position;
          rigidbody.AddForce(HoleConstants.HOLE_VACUUM_FORCE * direction, ForceMode.Force);
     

     }
     private void OnTriggerStay(Collider collision)
     {

          if (collision.gameObject.CompareTag(CollectibleConstants.COLLECTIBLE_TAG) || collision.gameObject.CompareTag(CollectibleConstants.ENEMY_TAG))
          {
               CollectibleContact(collision);
               if (Vector3.Distance(transform.parent.position, collision.gameObject.transform.position) < distance)
               {
                    collision.isTrigger = true;
               }
  
          }
     }
     private void OnDrawGizmos()
     {
          Gizmos.color = Color.white;
          Gizmos.DrawWireSphere(transform.parent.position, distance);
     }
}
