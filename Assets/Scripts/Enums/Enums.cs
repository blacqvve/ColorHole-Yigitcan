using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enums
{
     public enum GameState
     {
          Start,
          Playing,
          NextBoard,
          NewLevel,
          GameOver
     }
     public enum BoardIndex
     {
          First,
          Second
     }

     public enum CollectibleType
     {
          Safe,
          Enemy
     }
}
