using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatMigration : Enemy
{
    [SerializeField]
    private GameObject Node1;
    [SerializeField]
    private GameObject Node2;
    [SerializeField]
    private GameObject Water;

    private GameObject curNode;
    private GameObject notCurNode;
    private Boss_LadyBug thisBoss;

    private bool isMove;

    private Vector3 markPos;

    private void Start(){
        thisBoss = gameObject.GetComponent<Boss_LadyBug>();
        SelectNode();
        Coefficient = 0.3f;
    }

    private IEnumerator MoveTo(){
        Instantiate(Water, thisBoss.transform.position, Quaternion.identity);
        if (isMove){
            if (curNode.Equals(Node1)){
                if (Node1.transform.position.y < thisBoss.transform.position.y){
                    StopMigration();

                }
            }

            if (curNode.Equals(Node2)){
                if (Node2.transform.position.y > thisBoss.transform.position.y){
                    StopMigration();

                }
            }
        }
        yield return YieldInstructionCache.WaitingSecond(0.05f);

        StartCoroutine(MoveTo());
    }

    public void Migration(){
        StartCoroutine(Excute());
    }

    private IEnumerator Excute(){
        thisBoss.Darkened();
        yield return YieldInstructionCache.WaitingSecond(0.75f);
        Node1.transform.position = new Vector2(Random.Range(-8.0f, 8.0f), Node1.transform.position.y);
        Node2.transform.position = new Vector2(Random.Range(-8.0f, 8.0f), Node2.transform.position.y);
        if (isMove)
            StopCoroutine(Excute());

        SelectNode();

        if (curNode == Node1)
            thisBoss.gameObject.transform.position = Node2.gameObject.transform.position;
        else if (curNode == Node2)
            thisBoss.gameObject.transform.position = Node1.gameObject.transform.position;

        markPos = (curNode.transform.position - thisBoss.gameObject.transform.position).normalized;

        thisBoss.GetComponent<Rigidbody2D>().velocity = new Vector2(markPos.x * 14, markPos.y * 14);
        isMove = true;
        float digree;

        digree = Mathf.Atan2(notCurNode.transform.position.y - curNode.gameObject.transform.position.y,
                             notCurNode.transform.position.x - curNode.gameObject.transform.position.x) * 180 / Mathf.PI;

        digree -= 90;

        thisBoss.transform.Rotate(0,0,digree);
        StartCoroutine(MoveTo());

    }

    private void SelectNode(){
        if (curNode == null){
            curNode = Node1;
            notCurNode = Node2;
        }
        else if (curNode == Node1){
            curNode = Node2;
            notCurNode = Node1;
        }
        else if (curNode == Node2){
            curNode = Node1;
            notCurNode = Node2;
        }
    }

    private void OnDrawGizmos(){
        Gizmos.DrawSphere(Node1.transform.position, 0.5f);
        Gizmos.DrawSphere(Node2.transform.position, 0.5f);
        Gizmos.DrawLine(Node1.transform.position, Node2.transform.position);
    }

    private void StopMigration() {
        isMove = false;
        StopAllCoroutines();
        thisBoss.transform.position = new Vector2(0.0f, 3.0f);
        thisBoss.transform.rotation = Quaternion.identity;
        thisBoss.DarkenDown();
        thisBoss.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
