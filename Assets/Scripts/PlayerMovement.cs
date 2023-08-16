using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    

    private float dirX;
    private float delay = 0.3f;
   
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private LayerMask jumpableGround;

    private bool doubleJump;

    public Transform hitBox;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public int playerMaxHealth = 1000;
    int currentHealth;

    public HealthBar healthBar;

    public int basic_attack_dmg = 10;
    public int basic_attack_dmg_2 = 20;

    [SerializeField] private AudioSource jumpAudio;
    [SerializeField] private AudioSource attackAudio;
    [SerializeField] private AudioSource attack2Audio;



    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        currentHealth = playerMaxHealth;
        healthBar.SetMaxHealth(playerMaxHealth);
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (isGrounded() && !Input.GetButton("Jump"))
        {
            anim.SetBool("Jumping", true);
            doubleJump = false;
            
        } else
        {
            anim.SetBool("Jumping", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded() || doubleJump)
            {
                anim.SetBool("Jumping", true);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                doubleJump = !doubleJump;
                jumpAudio.Play();
                
            } else
            {
                anim.SetBool("Jumping", false);
            }
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            anim.SetBool("Jumping", true);
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            
        } else
        {
            anim.SetBool("Jumping", false);
        }

        if (!PauseMenu.isPaused)
        {
            UpdateAnimationState();
        }
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    void GameOver()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameOver");
    }

    private void UpdateAnimationState()
    {

        if (dirX > 0f)
        {
            anim.SetBool("Running", true);
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            anim.SetBool("Running", true);
            sprite.flipX = true;
        }
        else
        {
            anim.SetBool("Running", false);
        }

        if (Input.GetButtonDown("BasicAttack"))
        {
            Attack();
        } else if (Input.GetButtonDown("BasicAttack2"))
        {
            Attack2();
        }

    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void Attack()
    {
        anim.SetTrigger("BasicAttack");
        attackAudio.Play();
        
        StartCoroutine(DelayAttack());

        // detect enemy
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitBox.position, attackRange, enemyLayers);

        // Damage Them
        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy enemyCh = enemy.GetComponent<Enemy>();
            if (enemyCh == null)
                return;
            enemyCh.TakeDamage(basic_attack_dmg);
        }
    }

    private void Attack2()
    {
        anim.SetTrigger("BasicAttack2");
        attack2Audio.Play();

        StartCoroutine(DelayAttack());


        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitBox.position, attackRange, enemyLayers);


        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy enemyCh = enemy.GetComponent<Enemy>();
            if (enemyCh == null)
                return;
            enemyCh.TakeDamage(basic_attack_dmg_2);
        }
    }

    public void PlayerTakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Time.timeScale = 0f;
            Die();
        }
    }

    void Die()
    {
        anim.SetBool("isDead", true);

        this.enabled = false;
        GameOver();
    }

    private void OnDrawGizmosSelected()
    {
        if (hitBox == null)
            return;

        Gizmos.DrawWireSphere(hitBox.position, attackRange);
    }

    

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        //attackBlocked = false;
    }
}
