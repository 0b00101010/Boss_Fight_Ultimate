using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,4.5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            other.gameObject.GetComponent<Character>().Speed
            = other.gameObject.GetComponent<Character>().MaxSpeed / 100 * 50;
            StartCoroutine(Count(other.gameObject.GetComponent<Character>()));
        }

    }

    private IEnumerator Count(Character character)
    {
        yield return new WaitForSeconds(3.0f);
        character.Speed = character.MaxSpeed;
    }
}
