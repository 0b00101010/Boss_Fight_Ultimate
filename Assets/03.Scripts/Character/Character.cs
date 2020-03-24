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
    private int maxEnergy;
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

    [SerializeField]
    public Sprite[] skilEffect ;
    
    protected Rigidbody2D rBody;

    [SerializeField]
    private SpriteRenderer hitBackGround;

    [SerializeField]
    private Sprite deathEffect;
    private Vector2 jumpForceVector;        
    private EnemyDamage enemyDamage;

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
    // TODO : Division class 
    private void Awake(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rBody = gameObject.GetComponent<Rigidbody2D>();

        enemyDamage = GameObject.FindWithTag("StageManager")?.GetComponent<EnemyDamage>() ?? null;
        hitBackGround = GameObject.FindWithTag("HitBackGround")?.GetComponent<SpriteRenderer>() ?? null;

        jumpForceVector.x = 0;
        jumpForceVector.y = jumpForce;
        
        PlayerPrefs.SetInt("LastGameHitCount", 0);

        StartCoroutine(HealthEnergy());
    }

    protected void IDInit(int char_id, int ability_id, int skill_id){
        this.char_ID = char_id;
        this.ability_ID = ability_id;
        this.skill_ID = skill_id;
    }

    protected void RankInit(int rank, int level_tank, int level_dodge){
        this.rank = rank;
        this.level_Tank = level_tank;
        this.level_Dodge = level_dodge;
    }

    protected void StatInit(float char_Speed, int char_Hp, int char_energy,int char_abilityPrice, int jumpForce, ISkill abilitySkill, int maxEnergy = 100){
        this.speed = char_Speed;
        this.hp = char_Hp;
        this.maxHp = char_Hp;
        this.energy = char_energy;
        this.jumpForce = jumpForce;
        this.maxHp = char_Speed;
        this.abilityPrice = char_abilityPrice;
        this.abilitySkill = abilitySkill;
        this.maxEnergy = maxEnergy;
    }

    public void SetLeft(){
        if (isLeft){
            isLeft = false;
        }
        else if (!isLeft){
            isLeft = true;
        }
    }

    public void SetRight(){
        if (isRight){
            isRight = false;
        }
        else if (!isRight){
            isRight = true;
        }
    }


    public void Move(){
        if (isLeft){
            spriteRenderer.flipX = true;
            if(gameObject.transform.position.x - 0.2f > -8.5){
                gameObject.transform.position += Vector3.left * speed * Time.deltaTime;
            }
        }
        else if (isRight){
            spriteRenderer.flipX = false;
            if (gameObject.transform.position.x + 0.2f < 8.5)
                gameObject.transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }


    public virtual void Jump()
    {
        if (isJump && doubleJump){
            return;
        }

        rBody.velocity = jumpForceVector;

        if (isJump){
            doubleJump = true;
        }
        else{
            isJump = true;
        }
        
    }

    private IEnumerator HealthEnergy() {
        if(energy < maxEnergy) {
            if (energy + 5 > maxEnergy){
                energy = maxEnergy;
            }
            else{
                energy += 5;
            }
        }
        yield return YieldInstructionCache.WaitingSecond(1.0f);
        StartCoroutine(HealthEnergy());
    }

    public virtual void SpecialAbility(){
        if (energy >= abilityPrice){
            energy -= abilityPrice;
            spriteRenderer.sprite = charSprites[1];
            abilitySkill.Enter();
            isUseAbility = true;
            if(abilitySkill.Repeat()){
                StartCoroutine(UseAbility());
            }
        }
    }

    private IEnumerator UseAbility(){
        abilitySkill.Excute();

        yield return YieldInstructionCache.WaitingSecond(1.0f);

        if (energy - abilityPrice >= 0)
            energy -= abilityPrice;
        else if (energy - abilityPrice < 0){
            energy = 0;
            isUseAbility = false;
        }

        if (isUseAbility){
            StartCoroutine(UseAbility());
        }
    }

    public virtual void UnSpecialAbility(){
        isUseAbility = false;
        spriteRenderer.sprite = charSprites[0];
        abilitySkill.Exit();
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag("Enemy") && gameObject.transform.CompareTag("Character")){
            Hit(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other){
        if (other.CompareTag("Enemy") && gameObject.transform.CompareTag("Character")){
            Hit(other.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D other){
        if (other.gameObject.CompareTag("Enemy") && gameObject.transform.CompareTag("Character")){
            Hit(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.CompareTag("Ground")){
            isJump = false;
            doubleJump = false;
        }

        if (other.gameObject.CompareTag("Enemy") && gameObject.transform.CompareTag("Character")){
            Hit(other.gameObject);
        }
    }

    private void Hit(GameObject other){
        float damage = enemyDamage.GetDamage(other.gameObject.GetComponent<Enemy>());
        hp -= damage;
        other.transform.tag = "Untagged";
        PlayerPrefs.SetInt("LastGameHitCount", PlayerPrefs.GetInt("LastGameHitCount") + 1);
        StartCoroutine(FadeInOut());
    }

    private IEnumerator FadeInOut() {
        gameObject.transform.tag = "Untagged";
        
        StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(hitBackGround, 0.5f));
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(spriteRenderer, 0.15f));
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeInCoroutine(spriteRenderer,0.15f));
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeOutCoroutine(spriteRenderer, 0.15f));
        yield return StartCoroutine(GameManager.instance.fadeManager.SpriteFadeInCoroutine(spriteRenderer, 0.15f));
        
        gameObject.transform.tag = "Character";
    }


    public Sprite GetSprite(){
        return spriteRenderer.sprite;
    }

    public IEnumerator ShowEffect(Sprite effectSprite, bool effectFixed = true){
        GameObject target = Instantiate(new GameObject(), gameObject.transform.position, Quaternion.identity);
        target.AddComponent<SpriteRenderer>().sprite = effectSprite;
        SpriteRenderer tagetSpriteRenderer = target.GetComponent<SpriteRenderer>();
        tagetSpriteRenderer.sortingOrder = 1;
        if(effectFixed){
            target.transform.SetParent(gameObject.transform);
        }
        target.transform.localScale = Vector3.one;
        GameManager.instance.fadeManager.SpriteFadeOutCoroutine(tagetSpriteRenderer,0.3f);

        for (int i = 0; i < 10; i++) {
            yield return YieldInstructionCache.WaitingSecond(0.03f);
            target.transform.localScale += Vector3.one / 20;
        }
        Destroy(target);
    }

    public void Death(){
        StartCoroutine(ShowEffect(deathEffect));
    }
}