using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatMigration : BossPattern
{

    [SerializeField]
    private SpriteRenderer chargedSpriteRenderer;

    [SerializeField]
    private GameObject firstNode;

    [SerializeField]
    private GameObject secondNode;

    [SerializeField]
    private GameObject migrationWaterObject;

    private MigrationWater[] migrationWaters;

    private Vector2 defaultPosition;
    private Color defaultColor; 

    private GameObject targetNode;
    private GameObject startNode;

    private Rigidbody2D rigidBody;

    private void Awake(){
        defaultPosition = gameObject.transform.position;
        defaultColor = chargedSpriteRenderer.color;
        defaultColor.a = 0;

        migrationWaters = migrationWaterObject.GetComponentsInChildren<MigrationWater>(true);
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Start(){
        targetNode = firstNode;
        startNode = secondNode;
    }

    public override void Execute(){
        StartCoroutine(ExecuteCoroutine());
    }

    private IEnumerator ExecuteCoroutine(){
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeInCoroutine(chargedSpriteRenderer,0.25f));

        NodePositionRandomSetting();
        ChangeSelectNode();
        gameObject.transform.position = startNode.transform.position;
        LookAtTargetNode();

        rigidBody.velocity = GetMigrationDirection() * 15;
        StartCoroutine(IsMove());
    }

    private IEnumerator IsMove(){
        if(targetNode.Equals(firstNode)){
            while(gameObject.transform.position.y < targetNode.transform.position.y){
                CreateWater();                
                yield return YieldInstructionCache.WaitingSecond(0.05f);
            }
        }
        else{
            while(gameObject.transform.position.y > targetNode.transform.position.y){
                CreateWater();
                yield return YieldInstructionCache.WaitingSecond(0.05f);
            }
        }

        Reset();
    }

    private void CreateWater(){
        MigrationWater tempWater = GetUsePossibleWater();
        tempWater.gameObject.transform.position = gameObject.transform.position;
        tempWater.Execute();
    }

    private MigrationWater GetUsePossibleWater(){
        for(int i = 0; i < migrationWaters.Length; i++){
            if(migrationWaters[i].gameObject.activeInHierarchy.Equals(false)){
                return migrationWaters[i];
            }
        }

        return null;
    }

    private Vector2 GetMigrationDirection(){
        return (targetNode.transform.position - startNode.transform.position).normalized;
    }

    private void NodePositionRandomSetting(){
        Vector2 randomPosition = Vector2.zero;
        
        randomPosition.y = firstNode.transform.position.y;
        randomPosition.x = Random.Range(-8.0f, 8.0f);

        firstNode.transform.position = randomPosition;

        randomPosition.y = secondNode.transform.position.y;
        randomPosition.x = Random.Range(-8.0f, 8.0f);
        
        secondNode.transform.position = randomPosition;
    }

    private void LookAtTargetNode(){
        float angleDegree;
        angleDegree = Mathf.Atan2(targetNode.transform.position.y - startNode.transform.position.y
        , targetNode.transform.position.x - startNode.transform.position.x) * 180 / Mathf.PI;
        angleDegree += 90;
        gameObject.transform.Rotate(0,0,angleDegree);
    }

    private void ChangeSelectNode(){
        if(targetNode.Equals(firstNode)){
            targetNode = secondNode;
            startNode = firstNode;
        }
        else{
            targetNode = firstNode;
            startNode = secondNode;
        }
    }

    private void Reset(){
        chargedSpriteRenderer.color = defaultColor;
        gameObject.transform.position = defaultPosition; 
        gameObject.transform.rotation = Quaternion.identity;
        rigidBody.velocity = Vector2.zero;
    }
}
