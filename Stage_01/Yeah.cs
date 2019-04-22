using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yeah : MonoBehaviour
{
    [SerializeField]
    private GameObject Wild_Growth;
    [SerializeField]
    private GameObject Bullet_Spread;
    [SerializeField]
    private GameObject Big_Bullet;
    [SerializeField]
    private GreatMigration greatMigration;

    private void Awake()
    {
        greatMigration = GameObject.FindWithTag("Boss").GetComponent<GreatMigration>();
    }

    private void Start()
    {
        StartCoroutine(Pattern_Wild_Growth());
        StartCoroutine(Pattern_Bullet_Spread());
        StartCoroutine(Pattern_Big_Bullet());
        StartCoroutine(Pattern_GreatMigration());


    }

    private IEnumerator Pattern_Wild_Growth() {

        Instantiate(Wild_Growth,new Vector2(Random.Range(-8.0f,8.0f),0.0f),Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(1,4));
        StartCoroutine(Pattern_Wild_Growth());
    }

    private IEnumerator Pattern_Bullet_Spread()
    {
        Instantiate(Bullet_Spread, new Vector2(Random.Range(-8.0f, 8.0f), 0.0f), Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(1, 4));
        StartCoroutine(Pattern_Bullet_Spread());
    }


    private IEnumerator Pattern_Big_Bullet()
    {
        Instantiate(Big_Bullet, new Vector2(Random.Range(-8.0f, 8.0f), -2.5f), Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(3, 7));
        StartCoroutine(Pattern_Big_Bullet());
    }

    private IEnumerator Pattern_GreatMigration() {
        greatMigration.Migration();
        yield return new WaitForSeconds(Random.Range(3, 5));
        StartCoroutine(Pattern_GreatMigration());
    }


}
