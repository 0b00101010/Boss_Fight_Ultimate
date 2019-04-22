using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wild_Growth : MonoBehaviour
{
    [SerializeField]
    private GameObject[] sprouts;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(Excute());

    }

    private IEnumerator Excute()
    {
        yield return new WaitForSeconds(0.25f);
        yield return StartCoroutine(GameManager.instance.FadeOut(spriteRenderer, 0.5f));

        spriteRenderer.enabled = false;
        Instantiate(sprouts[Random.Range(0,sprouts.Length)],new Vector2(gameObject.transform.position.x, -6.0f),Quaternion.identity);

        Destroy(gameObject, 1.0f);
    }
}
