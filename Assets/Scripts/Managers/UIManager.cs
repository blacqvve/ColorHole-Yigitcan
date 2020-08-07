using Enums;
using Helpers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
     [Header("UI Variables")]
     [SerializeField]
     private Image firstBoardProgress;
     [SerializeField]
     private Image secondBoardProgress;
     [SerializeField]
     private Text currentLevelText;
     [SerializeField]
     private Text nextLevelText;
     [SerializeField]
     private GameObject startTextPanel;


     private LevelManager levelManager;
     private int firstBoardCollectibleCount;
     private int secondBoardCollectibleCount;

     private void OnEnable()
     {
          LevelManager.Instance.onObjectRemoved += UpdateProgressBars;

     }

     private void Start()
     {
          levelManager = LevelManager.Instance;
          firstBoardCollectibleCount = levelManager.GetLevelObjectCount(BoardIndex.First);
          secondBoardCollectibleCount = levelManager.GetLevelObjectCount(BoardIndex.Second);
          firstBoardProgress.fillAmount = 0f;
          secondBoardProgress.fillAmount = 0f;
          currentLevelText.text = GameManager.Instance.LevelIndex.ToString();
          nextLevelText.text = (GameManager.Instance.LevelIndex + 1).ToString();
          if (GameManager.Instance.GameState.Equals(GameState.Start))
          {
               startTextPanel.SetActive(true);
          }
     }
     private void Update()
     {
          if (startTextPanel.activeSelf&&GameManager.Instance.GameState.Equals(GameState.Start))
          {
               if (Input.touchCount>0||Input.GetMouseButtonDown(0))
               {
                    startTextPanel.SetActive(false);
                    
                    GameManager.Instance.SetGameState(GameState.Playing);
               }
          }
          if (GameManager.Instance.GameState.Equals(GameState.GameOver))
          {
               startTextPanel.SetActive(true);
               startTextPanel.GetComponentInChildren<Text>().text = "Game Over \n Tap To Retry";
               if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
               {
                    startTextPanel.SetActive(false);

                    GameManager.Instance.GameOver();
               }
          }
     }
     private void UpdateProgressBars(BoardIndex boardIndex)
     {
          switch (boardIndex)
          {
               case BoardIndex.First:
                    firstBoardProgress.fillAmount=1f-((float) levelManager.GetLevelObjectCount(boardIndex)/firstBoardCollectibleCount);
                    break;
               case BoardIndex.Second:
                    secondBoardProgress.fillAmount = 1f - ((float)levelManager.GetLevelObjectCount(boardIndex) / secondBoardCollectibleCount);
                    break;
          }
     }

     private void OnDisable()
     {
          LevelManager.Instance.onObjectRemoved -= UpdateProgressBars;
     }
}
