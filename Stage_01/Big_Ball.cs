using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big_Ball : Enemy
{
    [SerializeField]
    private GameObject Explode;
    private Vector3 pos;
    private Rigidbody2D rBody;

    private void Awake()
    {
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

    private IEnumerator OffTriger() {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
    }

    private void FixedUpdate()
    {
        rBody.velocity = new Vector2(pos.x * 15,pos.y * 15);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Instantiate(Explode,gameObject.transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}
