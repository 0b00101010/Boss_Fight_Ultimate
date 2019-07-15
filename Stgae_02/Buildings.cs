using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
    [SerializeField]
    private GameObject[] buildingObject;

    public void Excute()
    {
        StartCoroutine(MoveBuilding());
    }

    public void StopMoveBuilding()
    {
        StopCoroutine(MoveBuilding());
    }

    private IEnumerator MoveBuilding()
    {
        for (int i = 0; i < 2; i++)
        {
            buildingObject[i].gameObject.transform.Translate(new Vector2(-0.5f, 0));

            if (buildingObject[i].gameObject.transform.position.x <= -19.0f)
            {
                buildingObject[i].gameObject.transform.position = new Vector2(19.0f, 2.0f);
            }
        }

        yield return new WaitForSeconds(0.02f);
        StartCoroutine(MoveBuilding());
    }
}
