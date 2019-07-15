using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosFantasy : StagePattern
{
    [SerializeField]
    private GameObject[] patterns;
    [SerializeField]
    private StageBoss stageBoss;

    private void Awake()
    {
        stageBoss = GameObject.FindWithTag("Boss").GetComponent<StageBoss>();
    }

    public override void Excute(int patternNumber)
    {
        if (patternNumber == 0)
            Instantiate(patterns[patternNumber], new Vector2(Random.Range(-8.5f, 8.5f), -2.5f), Quaternion.identity);
        else
            Instantiate(patterns[patternNumber], new Vector2(Random.Range(-8.0f, 8.0f), 0.0f), Quaternion.identity);
    }
}
