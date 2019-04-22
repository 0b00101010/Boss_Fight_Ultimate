using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, ICharacter
{
    private float speed;
    private int jumpForce;
    private float hp;
    private int energy;
    private float maxSpeed;
    private float maxHp;
    private bool doubleJump;
    private bool isJump;
    
    private bool isLeft;
    private bool isRight;

    private bool isUseAbility;
    private int abilityPrice;
    private Character_Passives.PassiveNumber passiveNumber;
    protected Rigidbody2D rBody;
    private int EneryHealDeleay = 5;

    [SerializeField]
    private GameObject hitBackGround;

    private IObserver passiveObserver;

    public float Speed { get => speed; set => speed = value; }
    public float Hp { get => hp; set => hp = value; }
    protected bool IsLeft { get => isLeft; set => isLeft = value; }
    protected bool IsRight { get => isRight; set => isRight = value; }
    public int Energy { get => energy; set => energy = value; }
    public int AbilityPrice { get => abilityPrice; set => abilityPrice = value; }
    public bool IsUseAbility { get => isUseAbility; set => isUseAbility = value; }
    public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
    public int JumpForce { get => jumpForce; set => jumpForce = value; }
    public float MaxHp { get => maxHp; set => maxHp = value; }

    private void Awake()
    {
        passiveObserver = new PassiveObserver();
        rBody = gameObject.GetComponent<Rigidbody2D>();
        hitBackGround = GameObject.FindWithTag("HitBackGround");
        StartCoroutine(HealthEnergy());

    }

    protected void Init(float char_Speed, int char_Hp, int char_energy,int char_abilityPrice, int jumpForce)
    {
        Speed = char_Speed;
        Hp = char_Hp;
        MaxHp = char_Hp;
        Energy = char_energy;
        JumpForce = jumpForce;
        MaxSpeed = char_Speed;
        AbilityPrice = char_abilityPrice;
    }


    public void SetLeft()
    {
        if (IsLeft)
            IsLeft = false;

        else if (!IsLeft)
            IsLeft = true;
    }

    public void SetRight()
    {
        if (IsRight)
            IsRight = false;

        else if (!IsRight)
            IsRight = true;
    }


    public void Move()
    {
        if (IsLeft)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            if(gameObject.transform.position.x - 0.2f > -8.5)
                gameObject.transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (IsRight)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            if (gameObject.transform.position.x + 0.2f < 8.5)
                gameObject.transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }


    public virtual void Jump()
    {
        if (isJump && doubleJump)
            return;


        Vector2 force = new Vector2(0, JumpForce);
        rBody.velocity = force;
        if (isJump)
            doubleJump = true;
        else
            isJump = true;
    }

    private IEnumerator HealthEnergy() {
        if(Energy < 100) {
            if (Energy + 10 > 100)
                Energy = 100;
            else
                Energy += 5;
        }
        yield return new WaitForSeconds(1.0f);
        StartCoroutine(HealthEnergy());
    }

    public virtual void SpecialAbility(){
        IsUseAbility = true;
        StartCoroutine(UseAbility());
    }

    private IEnumerator UseAbility()
    {
        if (Energy - AbilityPrice >= 0)
            Energy -= AbilityPrice;
        else if (Energy - AbilityPrice < 0)
        {
            Energy = 0;
            IsUseAbility = false;
        }
        yield return new WaitForSeconds(1.0f);

        if (IsUseAbility)
            StartCoroutine(UseAbility());
    }

    public virtual void UnSpecialAbility() {
        IsUseAbility = false;
    }

    //private IEnumerator healEnergy() { 
    //    //딜레이가 끝나면 에너지 회복
    //}

    public void PassiveExcute() {
        if (passiveNumber.Equals(null))
            return;

        passiveObserver.Renewal((int)passiveNumber);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy");
            Hit(other.gameObject);

        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJump = false;
            doubleJump = false;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy");
            Hit(other.gameObject);

        }
    }

    private void Hit(GameObject other)
    {
        float damage = GameObject.FindWithTag("StageManager").GetComponent<EnemyDamage>().GetDamage(other.gameObject.GetComponent<Enemy>());
        hp -= damage;
        GameManager.instance.LastGameHitCount++;
        StartCoroutine(FadeInOut());
    }

    private IEnumerator FadeInOut() {

        SpriteRenderer spriteRenderer = hitBackGround.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(255,0,0,0.5f);
        StartCoroutine(GameManager.instance.FadeOut(spriteRenderer,0.5f,5));
        yield return StartCoroutine(GameManager.instance.FadeOut(gameObject.GetComponent<SpriteRenderer>(),0.15f,1));
        yield return StartCoroutine(GameManager.instance.FadeIn(gameObject.GetComponent<SpriteRenderer>(),0.15f,1));
        yield return StartCoroutine(GameManager.instance.FadeOut(gameObject.GetComponent<SpriteRenderer>(), 0.15f, 1));
        yield return StartCoroutine(GameManager.instance.FadeIn(gameObject.GetComponent<SpriteRenderer>(), 0.15f, 1));
    }



}