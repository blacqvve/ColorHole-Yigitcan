using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ExtensionMethods
{
     public static class Extensions
     {
          public static List<T> Clone<T>(this List<T> listToClone) where T : ICloneable
          {
               return listToClone.Select(item => (T)item.Clone()).ToList();
          }

          public static T GetRandomElementFromList<T>(this List<T> list)
          {
               int random = Random.Range(0, list.Count);
               return list[random];
          }

          public static T GetRandomElementFromList<T>(this List<T> list, T exclude)
          {
               int random = Random.Range(0, list.Count);
               while (Equals(list[random], exclude))
               {
                    random = Random.Range(0, list.Count);
               }

               return list[random];
          }

          public static void ChangeScaleY(this Transform thisTransform, float change)
          {
               var firstScale = thisTransform.transform.localScale;
               firstScale.y = change;
               thisTransform.transform.localScale = firstScale;
          }

          public static List<Transform> GetAllChilds(this Transform thisTransform)
          {
               return thisTransform.Cast<Transform>().ToList();
          }

          public static void ChangePositionWithChild(this Transform thisTransform, string childname)
          {
               var childs = thisTransform.GetAllChilds();
               var changedChild = childs.FirstOrDefault(x => x.name == childname);
               if (changedChild == null)
                    return;

               childs.ForEach(x => x.SetParent(null));
               var tempPos = changedChild.position;
               changedChild.position = thisTransform.position;
               thisTransform.position = tempPos;

               childs.ForEach(x => x.SetParent(thisTransform));
          }
          public static bool  IsEmpty<T>(this List<T> list)
          {
               if (list.Count==0)
               {
                    return true;
               }
               return false;
          }
     } 
}
