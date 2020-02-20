using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    private int barrierValue;

    private void Start()
    {
        Destroy(gameObject,2.0f);
        
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime);
    }

    public void SetBarrierValue(int value)
    {
        barrierValue = value;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject.FindWithTag("StageManager").GetComponent<EnemyDamage>().DeclineDamage(barrierValue);
            StartCoroutine(GameManager.instance.FadeOut(gameObject.GetComponent<SpriteRenderer>(),0.3f));
        }
    }

}
