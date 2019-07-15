using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CF_Test : MonoBehaviour
{
    [SerializeField]
    private Phase[] phases;

    private int phaseCount = 0;

    public void PhaseUp() {
        phases[phaseCount++].Excute();
    }

}
