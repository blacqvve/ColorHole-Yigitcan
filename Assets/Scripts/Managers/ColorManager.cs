using ExtensionMethods;
using Helpers;
using Structs;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : Singleton<ColorManager>
{
     public List<ColorCombination> levelColors;

     private ColorCombination previousColor;

     [SerializeField]
     private Material boardMat, boundsMat, enemyMat, gradientBg;
     protected override void Awake()
     {
          base.Awake();
          levelColors = new List<ColorCombination>();
          FillColorList();
     }
     void FillColorList()
     {

          levelColors.Add(new ColorCombination(new Color32(255, 184, 0,0), new Color32(244, 140, 6,0), new Color32(208, 0, 0,0), new Color32(202, 243, 36,255)));
          levelColors.Add(new ColorCombination(new Color32(67, 170, 139,0), new Color32(144, 190, 109,0), new Color32(248, 150, 30,0), new Color32(249, 199, 79,255)));
          levelColors.Add(new ColorCombination(new Color32(82, 183, 136,0), new Color32(45, 106, 79,0), new Color32(8, 28, 21,0), new Color32(216, 243, 220,255)));
          levelColors.Add(new ColorCombination(new Color32(234, 172, 139,0), new Color32(181, 101, 118,0), new Color32(53, 80, 112,0), new Color32(229, 107, 111,255)));
          levelColors.Add(new ColorCombination(new Color32(191, 210, 0,0), new Color32(128, 185, 24,0), new Color32(0, 127, 95,0), new Color32(170, 204, 0,255)));
          levelColors.Add(new ColorCombination(new Color32(242, 0, 137,0), new Color32(219, 0, 182,0), new Color32(45, 0, 247,0), new Color32(137, 0, 242,255)));
          levelColors.Add(new ColorCombination(new Color32(249, 87, 56,0), new Color32(244, 211, 94,0), new Color32(8, 61, 119,0), new Color32(238, 150, 75,255)));
          levelColors.Add(new ColorCombination(new Color32(92, 164, 169,0), new Color32(155, 193, 188,0), new Color32(237, 106, 90,0), new Color32(244, 241, 187,255)));
          levelColors.Add(new ColorCombination(new Color32(240, 135, 0,0), new Color32(239, 202, 8,0), new Color32(0, 166, 166,0), new Color32(187, 222, 240,255)));
     }

     public void SetRandomColorForLevel()
     {

          ColorCombination combination = levelColors.GetRandomElementFromList(previousColor);

          boardMat.color = combination.BoardColor;
          boundsMat.color = combination.BoundsColor;
          enemyMat.color = combination.EnemyColors;
          gradientBg.color = combination.BackgroundShadowColor;

          previousColor = combination;

     }
}
