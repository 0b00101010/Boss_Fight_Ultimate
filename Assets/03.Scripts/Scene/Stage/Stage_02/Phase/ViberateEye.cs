using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViberateEye : MonoBehaviour
{
    [SerializeField]
    private GameObject frontEye;

    [SerializeField]
    private GameObject backEye;

    private Vector2 newPosition;

    public void Execute(){
        StartCoroutine(ExecuteCoroutine());
    }

    private IEnumerator ExecuteCoroutine(){
        while(true){
            frontEye.transform.position = Vector2.zero;

            newPosition.x = Random.Range(-0.3f,0.7f);
            newPosition.y = Random.Range(-0.3f,0.7f);

            frontEye.transform.Translate(newPosition);
            yield return YieldInstructionCache.WaitingSecond(0.1f);
        }
    }
}
