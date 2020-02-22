using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TouchManager : MonoBehaviour
{
    private Vector2 touchDownPosition;
    private Vector2 touchUpPosition;

    private Vector2 swipeDirection;

    private float minSwipeDistance;

    private bool isTouch;
    private bool isSwipe;

    public Vector2 TouchDownPosition => touchDownPosition;
    public Vector2 TouchUpPosition => touchUpPosition;
    public Vector2 SwipeDirection => swipeDirection;
    
    public bool IsTouch => isTouch;
    public bool IsSwipe => isSwipe;


    private void Start(){
        minSwipeDistance = Screen.width / 5;
    }

    public void ProcessTouch(){
        if(Input.touchCount > 0){
            Touch tempTouch = Input.touches[0];

            if(tempTouch.phase.Equals(TouchPhase.Began)){
                isTouch = true;
                touchDownPosition = Camera.main.ScreenToWorldPoint(tempTouch.position);
            }
            else if(tempTouch.phase.Equals(TouchPhase.Moved)){
                Vector2 currentPosition = Camera.main.ScreenToWorldPoint(tempTouch.position);
                
                if((currentPosition -  touchDownPosition).magnitude > minSwipeDistance){
                    swipeDirection = (currentPosition - touchDownPosition).normalized;
                    isSwipe = true;
                }
            }
            else if(tempTouch.phase.Equals(TouchPhase.Ended)){
                isTouch = false;
                isSwipe = true;
                touchUpPosition = Camera.main.ScreenToWorldPoint(tempTouch.position);
            }
        }
    }

}

