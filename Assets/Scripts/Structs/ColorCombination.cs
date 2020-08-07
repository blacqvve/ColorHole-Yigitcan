using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Structs
{
     public struct ColorCombination
     {
          public Color32 BoardColor;
          public Color32 BoundsColor;
          public Color32 EnemyColors;
          public Color32 BackgroundShadowColor;


          public ColorCombination(Color32 boardColor, Color32 boundsColor, Color32 enemyColors,Color32 backgroundShadowColor)
          {
               BoardColor = boardColor;
               BoundsColor = boundsColor;
               EnemyColors = enemyColors;
               BackgroundShadowColor = backgroundShadowColor;
          }
     }
}