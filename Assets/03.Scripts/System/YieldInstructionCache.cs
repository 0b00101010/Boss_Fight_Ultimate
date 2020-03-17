using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class YieldInstructionCache 
{
    private static Dictionary<float, WaitForSeconds> waitingSeconds = new Dictionary<float, WaitForSeconds>();
    private static Dictionary<float, WaitForSecondsRealtime> waitingRealTime = new Dictionary<float, WaitForSecondsRealtime>();
    private static object waitFrame = null;

    public static object WaitFrame => waitFrame;

    public static WaitForSeconds WaitingSecond(float waitTime){
        if(!waitingSeconds.ContainsKey(waitTime)){
            waitingSeconds.Add(waitTime, new WaitForSeconds(waitTime));
        }
        
        return waitingSeconds[waitTime];
    }

    public static WaitForSecondsRealtime WaitingRealTime(float waitTime){
        if(!waitingRealTime.ContainsKey(waitTime)){
            waitingRealTime.Add(waitTime, new WaitForSecondsRealtime(waitTime));
        }
        
        return waitingRealTime[waitTime];
    }
}
