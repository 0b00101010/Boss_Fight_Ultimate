using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, ICharacter
{
    private int jumpForce;
    private float speed;

    #region ID
    private int char_ID;
    private int ability_ID;
    private int skill_ID;
    #endregion ID

    #region Stat
    private float hp;
    private int energy;
    private float maxSpeed;
    private int abilityPrice;
    private float maxHp;
    #endregion Stat

    #region Rank
    private int rank;
    private int level_Tank;
    private int level_Dodge;
    #endregion Rank

    private bool isLeft;
    private bool isRight;

    private bool doubleJump;
    private bool isJump;

    private bool isUseAbility;
    private int EneryHealDeleay = 5;

    private ISkill abilitySkill;

    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite[] charSprites;
    // 0 기본, 1 능력 사용중

    protected Rigidbody2D rBody;

    [SerializeField]
    private GameObject hitBackGround;

    #region Property
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
    public int Char_ID { get => char_ID; set => char_ID = value; }
    public int Rank { get => rank; set => rank = value; }
    public int Level_Tank { get => level_Tank; set => level_Tank = value; }
    public int Level_Dodge { get => level_Dodge; set => level_Dodge = value; }
    public int Ability_ID { get => ability_ID; set => ability_ID = value; }
    public int Skill_ID { get => skill_ID; set => skill_ID = value; }
    #endregion Property

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rBody = gameObject.GetComponent<Rigidbody2D>();
        hitBackGround = GameObject.FindWithTag("HitBackGround");
        StartCoroutine(HealthEnergy());

    }

    protected void IDInit(int char_id, int ability_id, int skill_id)
    {
        Char_ID = char_id;
        Ability_ID = ability_id;
        Skill_ID = skill_id;
    }

    protected void RankInit(int rank, int level_tank, int level_dodge)
    {
        Rank = rank;
        Level_Tank = level_tank;
        Level_Dodge = level_dodge;
    }

    protected void StatInit(float char_Speed, int char_Hp, int char_energy,int char_abilityPrice, int jumpForce, ISkill abilitySkill)
    {
        Speed = char_Speed;
        Hp = char_Hp;
        MaxHp = char_Hp;
        Energy = char_energy;
        JumpForce = jumpForce;
        MaxSpeed = char_Speed;
        AbilityPrice = char_abilityPrice;
        this.abilitySkill = abilitySkill;
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
        if (Energy > abilityPrice)
        {
            Energy -= abilityPrice;
            spriteRenderer.sprite = charSprites[1];
            abilitySkill.Enter();
            IsUseAbility = true;
            if(abilitySkill.Repeat())
                StartCoroutine(UseAbility());
        }
    }

    private IEnumerator UseAbility()
    {

        abilitySkill.Excute();

        yield return new WaitForSeconds(1.0f);

        if (Energy - AbilityPrice >= 0)
            Energy -= AbilityPrice;
        else if (Energy - AbilityPrice < 0)
        {
            Energy = 0;
            IsUseAbility = false;
        }

        if (IsUseAbility)
            StartCoroutine(UseAbility());
    }

    public virtual void UnSpecialAbility() {
        IsUseAbility = false;
        spriteRenderer.sprite = charSprites[0];
        abilitySkill.Exit();
    }

    //private IEnumerator healEnergy() { 
    //    //딜레이가 끝나면 에너지 회복
    //}


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

        SpriteRenderer backgroundRenderer = hitBackGround.GetComponent<SpriteRenderer>();
        backgroundRenderer.color = new Color(255,0,0,0.5f);
        StartCoroutine(GameManager.instance.FadeOut(backgroundRenderer, 0.5f,5));
        yield return StartCoroutine(GameManager.instance.FadeOut(spriteRenderer, 0.15f,1));
        yield return StartCoroutine(GameManager.instance.FadeIn(spriteRenderer,0.15f,1));
        yield return StartCoroutine(GameManager.instance.FadeOut(spriteRenderer, 0.15f, 1));
        yield return StartCoroutine(GameManager.instance.FadeIn(spriteRenderer, 0.15f, 1));
    }


    public Sprite GetSprite()
    {
        return spriteRenderer.sprite;
    }

 


}