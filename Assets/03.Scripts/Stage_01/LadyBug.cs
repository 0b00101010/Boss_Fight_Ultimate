using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadyBug : StagePattern
{
    [SerializeField]
    private GameObject[] patterns;
    [SerializeField]
    private GreatMigration greatMigration;
    private Boss_LadyBug boss;

    private void Awake(){
        boss = GameObject.FindWithTag("Boss").GetComponent<Boss_LadyBug>();
    }


    // TODO : Chanage to object pulling
    public override void Excute(int patternNumber){
        if (patternNumber == 2)
            Instantiate(patterns[patternNumber], new Vector2(Random.Range(-8.5f, 8.5f), -2.5f), Quaternion.identity);
        else if (patternNumber == 3)
            greatMigration.Migration();
        else
            Instantiate(patterns[patternNumber], new Vector2(Random.Range(-8.0f, 8.0f), 0.0f), Quaternion.identity);

    }
}
