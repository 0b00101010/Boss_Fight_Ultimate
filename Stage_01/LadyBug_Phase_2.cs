using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadyBug_Phase_2 : Phase
{
    [SerializeField]
    private GameObject rainDrop;

    public override void Excute()
    {
        StartCoroutine(RainDrop());
    }

    private IEnumerator RainDrop() {
        float repeatTime;
        repeatTime = Random.Range(0.01f,0.1f);
        GameObject rain = Instantiate(rainDrop,new Vector2(Random.Range(-8.0f,8.0f),6.0f),Quaternion.identity);
        yield return new WaitForSeconds(repeatTime);
        StartCoroutine(RainDrop());
    }

}
