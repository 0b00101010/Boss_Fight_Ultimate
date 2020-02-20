using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBall : Enemy
{
    [SerializeField]
    private GameObject Explode;
    private Vector3 pos;
    private Rigidbody2D rBody;

    private void Awake()
    {
        Coefficient = 0.3f;
        rBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(OffTriger());
    }

    public void SetPos(Vector3 bossPosition, Vector3 cautionPosition)
    {
        pos = (cautionPosition - bossPosition).normalized;
    }

    private IEnumerator OffTriger()
    {
        yield return YieldInstructionCache.WaitFrame;
        gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
    }

    private void FixedUpdate()
    {
        rBody.velocity = new Vector2(pos.x * 10, pos.y * 10);
        gameObject.transform.localScale -= new Vector3(0.02f, 0.02f);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject explode = Instantiate(Explode, gameObject.transform.position, Quaternion.identity);
        gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
        StartCoroutine(GameManager.instance.FadeOut(gameObject.GetComponent<SpriteRenderer>(),0.1f));
        StartCoroutine(GameManager.instance.FadeOut(explode.GetComponent<SpriteRenderer>(), 0.1f));
        Destroy(explode,0.5f);
        Destroy(gameObject,0.5f);
    }
}
