using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class YieldInstructionCache 
{
    private static Dictionary<float, WaitForSeconds> waitingSeconds = new Dictionary<float, WaitForSeconds>();
    private static object waitFrame = null;

    public static object WaitFrame => waitFrame;

    public static WaitForSeconds WaitingSecond(float waitTime){
        if(!waitingSeconds.ContainsKey(waitTime)){
            waitingSeconds.Add(waitTime, new WaitForSeconds(waitTime));
        }
        
        return waitingSeconds[waitTime];
    }
}
