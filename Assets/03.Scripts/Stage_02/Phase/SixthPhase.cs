using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SixthPhase : Phase
{   
    private BuildingObject buildingObject;

    public override void Execute(){
        buildingObject.BulidingOff();
    } 
}
