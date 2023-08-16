using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    #region Public Variables
    public Transform rayCast;
    public LayerMask rayCastMask;
    public float rayCastLength;
    public float attackDistance; // Minimum distance for attack
    public float moveSpeed;
    public float timer; // Timer for cooldown between attacks
    public Transform leftLimit;
    public Transform rightLimit;
    #endregion

    #region Private Variables
    private RaycastHit2D hit;
    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private Transform target;
    private Animator anim;
    private float distance; // store the distance b/w enemy and player
    private bool attackMode; 
    private bool inRange; // check if Player is in range
    private bool cooling; // Check if Enemy is cooling after attack
    private float intTimer;
    private float dirX;
    private float delay = 0.5f;
    private float delayScene = 5f;
    #endregion

    public int maxHealth = 100;
    int currentHealth;

    public Transform hitBox;
    public LayerMask playerLayers;
    public float attackRange = 0.5f;

    public int enemy_attack = 10;

    public HealthBar healthBar;

    private void Awake()
    {
        SelectTarget(); 
        intTimer = timer; // store the initial valie of timer
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

  
    void Update()
    {

        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, transform.right, rayCastLength, rayCastMask);
            RaycastDebugger();
        }

        // when player found
        if (hit.collider != null)
        {
            EnemyLogic();
        }
        else if (hit.collider == null)
        {
            inRange = false;
        }

        if (!attackMode)
        {
            Move();
        }

        if (!InsideOfLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("BasicAttack"))
        {
            SelectTarget();
        }

        if (inRange == false)
        {
            StopAttack();
        }
    }

    void GameOver()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameOver");
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player" || trig.tag == "Enemy")
        {
            target = trig.transform;
            inRange = true;
            Flip();
        }

        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        //play hurt animation
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

        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        GameOver();
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            anim.SetBool("BasicAttack", false);
        }
    }

    void Move()
    {
        anim.SetBool("Running", true); 

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("BasicAttack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }

    
    }

    void Attack()
    {
        timer = intTimer; // reset timer
        attackMode = true; // to check if enemy can still attack or not


        anim.SetBool("Running", false);
        anim.SetBool("BasicAttack", true);

        StartCoroutine(DelayAttack());

        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(hitBox.position, attackRange, playerLayers);

        foreach (Collider2D player in hitPlayer)
        {
            PlayerMovement playCh = player.GetComponent<PlayerMovement>();
            if (playCh == null)
                return;
            playCh.PlayerTakeDamage(enemy_attack);
        }
    }


    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("BasicAttack", false);
    }

    void RaycastDebugger()
    {
        if (distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.red);
        }
        else if (attackDistance > distance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.green);
        }
    }

    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    private void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();
    }

    private void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        } else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        //attackBlocked = false;
    }

    private IEnumerator DelayScene()
    {
        yield return new WaitForSeconds(delayScene);
    }
}
