using Enums;
using ExtensionMethods;
using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
     [SerializeField]
     private List<GameObject> firstBuffer = new List<GameObject>();
     [SerializeField]
     private List<GameObject> secondBuffer = new List<GameObject>();

     [SerializeField]
     private GameObject secondBoardParent;

     GameManager gameManager;

     public Action<BoardIndex> onBufferEmpty;
     public Action<BoardIndex> onObjectRemoved;

     private void Start()
     {
          gameManager = GameManager.Instance;
          print("firstBuffer =" + firstBuffer.Count);
          print("secondBuffer =" + secondBuffer .Count);
          ColorManager.Instance.SetRandomColorForLevel();
     }

     private void OnEnable()
     {
          DestroyBounds.onBoundTrigger += CollectibleCollect;
     }
     private void OnDisable()
     {
          DestroyBounds.onBoundTrigger -= CollectibleCollect;
     }

     private void CollectibleCollect(GameObject obj)
     {
          if (obj.CompareTag(CollectibleConstants.ENEMY_TAG))
          {
               if (gameManager.GameState.Equals(GameState.Playing))
               {
                    gameManager.SetGameState(GameState.GameOver);
                    return;
               }
          }

          switch (gameManager.boardIndex)
          {
               case BoardIndex.First:
                    firstBuffer.Remove(obj);
                    onObjectRemoved?.Invoke(BoardIndex.First);
                    if (firstBuffer.IsEmpty())
                    {
                         onBufferEmpty?.Invoke(BoardIndex.First);
                         gameManager.SetGameState(GameState.NextBoard);
                         secondBoardParent.SetActive(true);

                    }
                    break;
               case BoardIndex.Second:
                    secondBuffer.Remove(obj);
                    onObjectRemoved?.Invoke(BoardIndex.Second);
                    if (secondBuffer.IsEmpty())
                    {
                         onBufferEmpty?.Invoke(BoardIndex.Second);
                         gameManager.BoardComplete();
                    }
                    break;
          }
     }

     public int GetLevelObjectCount(BoardIndex boardIndex)
     {
          switch (boardIndex)
          {
               case BoardIndex.First:
                    return firstBuffer.Count;
               case BoardIndex.Second:
                    return secondBuffer.Count;
               default:
                    return 0;
          }
     }


}
