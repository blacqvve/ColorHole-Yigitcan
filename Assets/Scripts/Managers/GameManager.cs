using Enums;
using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
     private GameState gameState;

     public GameState GameState
     {
          get { return gameState; }
     }
     private int levelIndex;
     public int LevelIndex
     {
          get { return levelIndex; }
          set { levelIndex = levelIndex++; }
     }
     public BoardIndex boardIndex;

     public void BoardComplete()
     {
          gameState = GameState.NewLevel;
          StartCoroutine(LaodNextLevel());
     }

     protected override void Awake()
     {
          base.Awake();
          levelIndex = 1;
          boardIndex = 0;
          gameState = GameState.Start;
     }

     public void SetGameState(GameState state)
     {
          gameState = state;
     }

     private void Start()
     {
          ColorManager.Instance.SetRandomColorForLevel();
     }

     public void GameOver()
     {

          SceneManager.sceneLoaded += SceneRestarted;
          SceneManager.LoadScene(SceneManager.GetActiveScene().name);

     }

     private void SceneRestarted(Scene arg0, LoadSceneMode arg1)
     {
          boardIndex = 0;
          gameState = GameState.Playing;
          SceneManager.sceneLoaded -= SceneRestarted;
     }

     IEnumerator LaodNextLevel()
     {
          if (gameState.Equals(GameState.NewLevel))
          {
               yield return new WaitForSeconds(2.5f);
               boardIndex = 0;
               SceneManager.sceneLoaded += LevelLoaded;
               AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Level0" + (levelIndex + 1), LoadSceneMode.Single);
               while (!asyncLoad.isDone)
               {
                    yield return null;
               }



          }
     }

     private void LevelLoaded(Scene arg0, LoadSceneMode arg1)
     {
          levelIndex++;
          gameState = GameState.Playing;
          SceneManager.sceneLoaded -= LevelLoaded;

     }
}
