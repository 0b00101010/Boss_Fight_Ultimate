using UnityEngine;

public abstract class BossPattern : MonoBehaviour{

    private Vector2 newPosition = Vector2.zero;    
    public abstract void Execute();
   
    public void NewPositionX(){
        newPosition = gameObject.transform.position;
        newPosition.x = Random.Range(-8.0f, 8.0f);
        gameObject.transform.position = newPosition;
    }

    public void NewPositionY(){
        newPosition = gameObject.transform.position;
        newPosition.y = Random.Range(-2.0f, 3.0f);
        gameObject.transform.position = newPosition;
    }
}