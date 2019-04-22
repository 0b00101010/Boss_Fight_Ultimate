using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterimageBlock : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BlockDestroy());
        gameObject.transform.SetParent(GameObject.Find("Blocks").transform);
    }

    private IEnumerator BlockDestroy()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        yield return GameManager.instance.FadeOut(spriteRenderer,0.6f,12);
        Destroy(gameObject);
    }

}
