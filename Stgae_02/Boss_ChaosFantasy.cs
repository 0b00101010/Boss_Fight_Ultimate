using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_ChaosFantasy : StageBoss
{

    [SerializeField]
    private GameObject rainbowCircle;

    [SerializeField]
    private GameObject vibrateEye;

    private void Awake()
    {
        Damage = 100;
    }

    public void Vibrate() {
        StartCoroutine(EyeVibrate());
        StartCoroutine(RainBowCircle());
    }

    private IEnumerator EyeVibrate()
    {
        GameObject newEye = Instantiate(vibrateEye,new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)),Quaternion.identity);
        Destroy(newEye, 0.5f);
        StartCoroutine(GameManager.instance.FadeOut(newEye.GetComponent<SpriteRenderer>(), 0.3f));
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(EyeVibrate());
    }

    private IEnumerator RainBowCircle()
    {
        GameObject rainbow = Instantiate(rainbowCircle,gameObject.transform.position,Quaternion.identity);

        rainbow.transform.Rotate(new Vector3(0,0,Random.Range(0f,90f)));
        yield return GameManager.instance.FadeOut(rainbow.GetComponent<SpriteRenderer>(),0.5f);
        Destroy(rainbow);
        yield return new WaitForSeconds(1.0f);

        StartCoroutine(RainBowCircle());
    }
}
