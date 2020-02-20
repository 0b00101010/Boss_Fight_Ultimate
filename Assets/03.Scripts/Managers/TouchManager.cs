using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TouchManager : MonoBehaviour
{
    private Vector2 touchedPos;
    private Touch tempTouch;
    private Vector2 swipeDirection;
    private bool isTouch;
    private bool isSwiped;
    private float minSwipeDist;
    private Action<Vector2> actionOnSwipeDetected;
    public bool IsTouch { get => isTouch; set => isTouch = value; }
    public bool IsSwiped { get => isSwiped; set => isSwiped = value; }
    public Vector2 SwipeDirection { get => swipeDirection; set => swipeDirection = value; }

    // Update is called once per frame
    public void Awake()
    {
        minSwipeDist = Mathf.Max(Screen.width, Screen.height) / 30f;
    }

    public void ProcessMobileInput() {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                tempTouch = Input.GetTouch(i);
                if (tempTouch.phase == TouchPhase.Began) {
                    //touchedPos = Camera.main.ScreenToWorldPoint(tempTouch.position);
                    touchedPos = new Vector2(tempTouch.position.x, tempTouch.position.y);
                    isTouch = true;
                    isSwiped = false;
                }
                else if (tempTouch.phase == TouchPhase.Moved)
                {
                    Vector2 currentTouchPos = new Vector2(tempTouch.position.x, tempTouch.position.y);
                    bool swipeDetected = CheckSwipe(touchedPos, currentTouchPos);
                    SwipeDirection = (currentTouchPos - touchedPos).normalized;
                    if (swipeDetected)
                    {
                        onSwipeDetected(SwipeDirection);
                    }
                } else if (tempTouch.phase == TouchPhase.Ended)
                {
                    //Debug.Log("Swipe End!");
                    swipeDirection = new Vector2(0.0f,0.0f);
                    isTouch = false;
                }


            }
        }
    }

    private bool CheckSwipe(Vector2 downPos, Vector2 currentPos)
    {

        if (isSwiped)
            return false;

        Vector2 currentSwipe = currentPos - downPos;
        if (currentSwipe.magnitude >= minSwipeDist)
        {
           //Debug.Log("SwipeDetected : " + currentSwipe);
            return true;
        }
        return false;

    }

    public void SetOnSwipeDectected(Action<Vector2> SwipeDetected)
    {
        actionOnSwipeDetected = SwipeDetected;
    }

    private void onSwipeDetected(Vector2 Direction)
    {
        isSwiped = true;
        actionOnSwipeDetected(Direction);
    }

    public Vector2 GetPosition()
    {
        return touchedPos;
    }
}

