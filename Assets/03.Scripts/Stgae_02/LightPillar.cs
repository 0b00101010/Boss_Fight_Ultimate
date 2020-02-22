using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPillar : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private GameObject lightPillar;

    private void Start()
    {
      
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(Excute());

    }

    private IEnumerator Excute()
    {
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(spriteRenderer, 0.25f));
        GameObject target = Instantiate(lightPillar, gameObject.transform.position, Quaternion.identity);
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(target.GetComponent<SpriteRenderer>(),0.1f));
        Destroy(target);
        Destroy(gameObject);
    }
}
