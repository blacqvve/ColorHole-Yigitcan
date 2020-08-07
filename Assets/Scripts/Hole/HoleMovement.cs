using DG.Tweening;
using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class HoleMovement : MonoBehaviour
{
     [SerializeField]
     private float holeSpeed;

     bool moveToXComplete = false;

     bool isMoving = false;

     public Vector3 positiveClamp = Vector3.zero;
     public Vector3 negativeClamp = Vector3.zero;

     private void Update()
     {
          if (GameManager.Instance.GameState.Equals(GameState.Playing))
          {
               isMoving = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved;
               if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
               {

                    isMoving = Input.GetMouseButton(0);
               }


               if (isMoving)
               {
                    MoveHole();
               }
          }
          if (GameManager.Instance.GameState.Equals(GameState.NextBoard))
          {
               MoveToNextBoard();
          }
     }

     private void MoveToNextBoard()
     {

          Transform cameraTransform = Camera.main.transform;
          Vector3 holeNewPos;
          if (!moveToXComplete)
          {
               holeNewPos = new Vector3(0,transform.position.x,transform.position.z);
               transform.position = Vector3.Lerp(transform.position, holeNewPos, HoleConstants.BOARD_CHANGE_SPEED * Time.deltaTime);
               if (Vector3.Distance(transform.position, holeNewPos) < HoleConstants.LERP_END_THRESHOLD)
               {
                    transform.position = holeNewPos;
                    moveToXComplete = true;
               }
          }

          if (moveToXComplete)
          {
               holeNewPos = new Vector3(
                         transform.position.x,
                         transform.position.y,
                         8f
                         );
               Vector3 cameraNewPos = new Vector3(
                    cameraTransform.position.x,
                    cameraTransform.position.y,
                    8f
                    );

               if (Vector3.Distance(transform.position, holeNewPos) > HoleConstants.LERP_END_THRESHOLD)
               {
                    cameraTransform.position = Vector3.Lerp(cameraTransform.position, cameraNewPos, HoleConstants.BOARD_CHANGE_CAMERA_SPEED * Time.deltaTime);
                    transform.position = Vector3.Lerp(transform.position, holeNewPos, HoleConstants.BOARD_CHANGE_SPEED * Time.deltaTime);

               }
               else
               {
                    cameraTransform.transform.position = cameraNewPos;
                    transform.position = holeNewPos;
                    positiveClamp = new Vector3(1.2f, 0, 12.2f);
                    negativeClamp = new Vector3(-1.2f, 0, 7.8f);
                    GameManager.Instance.SetGameState(GameState.Playing);
                    GameManager.Instance.boardIndex = BoardIndex.Second;
               }
          }


     }

     private void MoveHole()
     {

          float x = Input.GetAxis("Mouse X");
          float y = Input.GetAxis("Mouse Y");
          if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
          {
               Touch touch = Input.touches[0];
               x = touch.deltaPosition.x;
               y = touch.deltaPosition.y;
               print("x = " + x + " y=" + y);
          }
          Vector3 tempVector = new Vector3(x, 0, y);
          Vector3 movePos = new Vector3(Mathf.Clamp(
              transform.position.x+tempVector.normalized.x *Time.deltaTime, negativeClamp.x, positiveClamp.x),
              0f,
               Mathf.Clamp(transform.position.z+tempVector.normalized.z*Time.deltaTime, negativeClamp.z, positiveClamp.z)
               );
          transform.position = Vector3.MoveTowards(transform.position,movePos,1);

     }
}
