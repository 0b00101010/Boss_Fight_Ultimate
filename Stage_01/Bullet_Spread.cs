using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Spread : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer crosshairRenderer;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private GameObject[] Bullets;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(Excute());
    }

    private IEnumerator Excute()
    {
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < 5; i++)
        {

            crosshairRenderer.color = new Color(crosshairRenderer.color.r, crosshairRenderer.color.g, crosshairRenderer.color.b, crosshairRenderer.color.a - 0.2f);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - 0.2f);
            crosshairRenderer.gameObject.transform.localScale -= new Vector3(0.12f,0.12f,0.12f);

            yield return new WaitForSeconds(0.1f);
        }

        spriteRenderer.enabled = false;
        crosshairRenderer.enabled = false;

        for(int i = 0; i < Bullets.Length; i++) {
            Instantiate(Bullets[i],gameObject.transform.position,Quaternion.identity);
            yield return new WaitForSeconds(0.01f);
        }

        Destroy(gameObject,1.0f);
    }
}
